import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ShortenUrlService } from '../../../../core/services/shorten-url.service';

@Component({
  selector: 'app-shorten-url-form',
  templateUrl: './shorten-url-form.component.html',
  styleUrl: './shorten-url-form.component.css'
})
export class ShortenUrlFormComponent implements OnInit{
  shortenForm!: FormGroup;
  shortenUrl: string = '';

  constructor(private fb: FormBuilder, private shortenUrlService: ShortenUrlService) {

  }

  ngOnInit(): void {
    this.shortenForm = this.fb.group({
      longUrl: ['', [Validators.required]]
    })
  }

  onSubmit() {
    if(!this.shortenForm.valid) return;
    this.createShortenUrl();
  }

  createShortenUrl() {
    this.shortenUrlService.createShortenUrl(this.shortenForm.value).subscribe({
      next: (response) => {
        this.shortenUrl = response.shortUrl;
        this.shortenForm.reset();
        this.shortenUrlService.refreshData.next();
      }
    })
  }

  get longUrl() {
    return this.shortenForm.get('longUrl')
  }
}
