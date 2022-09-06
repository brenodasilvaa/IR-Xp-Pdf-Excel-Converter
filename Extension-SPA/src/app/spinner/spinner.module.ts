import { CommonModule } from '@angular/common';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { SpinnerComponent } from './spinner.component';

@NgModule({
  imports: [
    CommonModule
  ],
  exports:[SpinnerComponent],
  declarations: [SpinnerComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class SpinnerModule { }
