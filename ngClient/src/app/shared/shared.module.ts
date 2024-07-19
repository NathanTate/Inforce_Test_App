import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { PaginationComponent } from './components/pagination/pagination.component';
import { HoldableDirective } from "./directives/holdTime.directive";
import { HasRoleDirective } from './directives/has-role.directive';
import { CopyDirective } from './directives/copy.directive';

@NgModule({
  declarations: [
    PaginationComponent,
    HoldableDirective,
    HasRoleDirective,
    CopyDirective
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    ReactiveFormsModule
  ],
  exports: [
    CommonModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    PaginationComponent,
    HoldableDirective,
    HasRoleDirective,
    CopyDirective
  ]
}) 
export class SharedModule {
}