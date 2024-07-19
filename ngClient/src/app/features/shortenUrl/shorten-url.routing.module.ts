import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShortenUrlComponent } from './components/shorten-url/shorten-url.component';
import { ShortenUrlDetailsComponent } from './components/shorten-url-details/shorten-url-details.component';
import { authGuard } from '../../core/guards/auth.guard';

const routes: Routes = [
  {path: '', component: ShortenUrlComponent},
  {path: ':id', component: ShortenUrlDetailsComponent, canActivate: [authGuard]},
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class ShortenUrlRoutingModule { }
