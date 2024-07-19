import { NgModule } from "@angular/core";
import { TableComponent } from "./components/table/table.component";
import { SharedModule } from "../../shared/shared.module";
import { ShortenUrlRoutingModule } from "./shorten-url.routing.module";
import { ShortenUrlComponent } from "./components/shorten-url/shorten-url.component";
import { ShortenUrlFormComponent } from "./components/shorten-url-form/shorten-url-form.component";
import { ShortenUrlDetailsComponent } from "./components/shorten-url-details/shorten-url-details.component";

@NgModule({
  declarations: [
    TableComponent,
    ShortenUrlComponent,
    ShortenUrlFormComponent,
    ShortenUrlDetailsComponent
  ],
  imports: [
    SharedModule,
    ShortenUrlRoutingModule
  ]
})
export class ShortenUrlModule {

}