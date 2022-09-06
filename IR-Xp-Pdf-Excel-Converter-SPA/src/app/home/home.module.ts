import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { SpinnerModule } from '../spinner/spinner.module';
import { DragDropDirective } from './drag-drop.directive';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';

@NgModule({
  imports: [
    CommonModule,
    HomeRoutingModule,
    HttpClientModule,
    SpinnerModule
  ],
  declarations: [HomeComponent,
                DragDropDirective],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class HomeModule { }
