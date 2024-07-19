export interface ShortenUrlResponse {
  items: ShortenUrl[];
  page: number;
  pageSize: number;
  totalCount: number;
}

export interface ShortenUrl {
  id: number;
  longUrl: string;
  shortUrl: string;
  code: string;
  createdAt: Date;
  applicationUserId: number;
}