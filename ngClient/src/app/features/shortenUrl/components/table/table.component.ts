import { Component, EventEmitter, Input, OnInit, Output, Renderer2 } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { faEye, faSort, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ShortenUrlService } from '../../../../core/services/shorten-url.service';
import { PaginationParams } from '../../../../core/models/PaginationParams';
import { ShortenUrlResponse } from '../../../../core/models/ShortenUrlResponse';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-datatable',
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent implements OnInit{
  params = new PaginationParams()
  shortenUrlsResponse!: ShortenUrlResponse;
  length: number = 0;
  actions: any[] = [];
  totalCount: number = 0;
  pageSize: number = 10;
  currentPage: number = 1;
  filter = new EventEmitter<string>();
  progressMap = new Map<number, number>();

  iconSort = faSort;
  iconEye = faEye;
  iconTrash = faTrash;

  searchForm: FormGroup = this.fb.nonNullable.group({
    searchTerm: '',
  });

  constructor(private fb: FormBuilder, private shortenUrlService: ShortenUrlService, public authService: AuthService) {}

  ngOnInit(): void {
    this.getShortenUrls();
    this.shortenUrlService.refreshData.subscribe({
      next: () => this.getShortenUrls()
    })
  }

  getShortenUrls() {
    this.shortenUrlService.getShortenUrls(this.params).subscribe({
      next: (response) => {
        this.shortenUrlsResponse = response;
        console.log(response)
        for (let item of response.items) {
          this.progressMap.set(item.id, 0);
          this.progressMap.set(100000, 0)
        }
      }
    })
  }

  deleteAll(e: number, id: number) {
    this.progressMap.set(id, e / 10)
    const progress = this.progressMap.get(id);
    if (progress != undefined && progress > 100) {
      this.shortenUrlService.deleteAllShortenUrls().subscribe({
        next: () => {
          this.getShortenUrls();
        }
      })
    }
  }

  deleteRow(e: number, id: number) {
    this.progressMap.set(id, e / 10);
    const progress = this.progressMap.get(id);
    if (progress !== undefined && progress > 100) {
      this.shortenUrlService.deleteShortenUrl(id).subscribe({
        next: () => {
          this.getShortenUrls();
        }
      })
    }
  }

  onSearch() {
    const searchTerm = this.searchForm.value.searchTerm ?? '';
    this.filter.emit(searchTerm);
  }

  onPageChange(page: number) {
    if (this.params.page != page) {
      this.params.page = page;
      this.getShortenUrls();
    }
  }

  get columns(): string[] {
    return [ 'id', 'long Url', 'short Url', 'code', 'createdAt', 'userId']
  }
}
