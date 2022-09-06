import { Directive, Output, Input, EventEmitter, HostBinding, HostListener } from '@angular/core';

@Directive({
  selector: '[appDragDrop]'
})
export class DragDropDirective {

  // tslint:disable-next-line: no-output-on-prefix
  @Output() onFileDropped = new EventEmitter<any>();

  @HostBinding('style.background-color') private background = '#0077B5';
  @HostBinding('style.opacity') private opacity = '1';

  // Dragover listener
  @HostListener('dragover', ['$event']) onDragOver(evt:any) {
    evt.preventDefault();
    evt.stopPropagation();
    this.background = '#2E5E8F';
    this.opacity = '0.8';
  }

  // Dragleave listener
  @HostListener('dragleave', ['$event']) public onDragLeave(evt:any) {
    evt.preventDefault();
    evt.stopPropagation();
    this.background = '#0077B5';
    this.opacity = '1';
  }

  // Drop listener
  @HostListener('drop', ['$event']) public ondrop(evt:any) {
    evt.preventDefault();
    evt.stopPropagation();
    this.background = '#0077B5';
    this.opacity = '1';
    const files = evt.dataTransfer.files;
    if (files.length > 0) {
      this.onFileDropped.emit(files);
    }
  }

}
