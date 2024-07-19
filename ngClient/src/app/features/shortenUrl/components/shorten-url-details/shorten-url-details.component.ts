import { Component, OnDestroy, OnInit } from '@angular/core';
import { ShortenUrlService } from '../../../../core/services/shorten-url.service';
import { ShortenUrl } from '../../../../core/models/ShortenUrlResponse';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-shorten-url-details',
  templateUrl: './shorten-url-details.component.html',
  styleUrl: './shorten-url-details.component.css'
})
export class ShortenUrlDetailsComponent implements OnInit, OnDestroy{
  shortenUrl!: ShortenUrl;
  currentId: number = 0;
  subscription!: Subscription;
  isCopied = false;

  constructor(private shortenUrlService: ShortenUrlService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe({
      next: (params: Params) => {
        this.currentId = +params['id'];
        this.getShortenUrl(this.currentId);
      }
    })
  }

  onCopied() {
    this.isCopied = true;
  }

  getShortenUrl(id: number) {
    this.shortenUrlService.getShortenUrl(id).subscribe({
      next: (response) => {
        this.shortenUrl = response;
      }
    })
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    }
  }

}
