import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { delay, timeInterval, timeout } from 'rxjs';
import { AnyCatcher } from 'rxjs/internal/AnyCatcher';
import { FileUploadService } from 'src/services/file-upload.service';
import { ReturnModel } from '../Models/ReturnModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  // Variable to store shortLink from api response
  shortLink: string = "";
  returnModel: ReturnModel;
  loading: boolean = false; // Flag variable
  completed: boolean = false;
  uploaded: boolean = false;
  file: any = null; // Variable to store file
  success: boolean = true; // Variable to store file
  @ViewChild('fileInput') fileInput: ElementRef<HTMLInputElement> = {} as ElementRef

  // Inject service 
  constructor(private fileUploadService: FileUploadService) {
    this.returnModel = new ReturnModel();
   }

  ngOnInit(): void {}

  // On file Select
  onChange(event: any) {
      this.file = event.target.files[0];
      this.uploaded = true;
      this.completed = false;
      this.success = true;
      document.getElementById('btn-upload')?.removeAttribute('disabled')
  }

  // OnClick of button Upload
  onUpload() {
      this.loading = true;
      this.fileUploadService.upload(this.file).subscribe({
          next: (result: ReturnModel) => 
          {
            if (!result.success){
              this.SetError(result.message);
            }
            else{
              this.SetSuccess();

              result.downloadLinks.forEach(url => {
                window.open(url)
              });
            }  

              setTimeout(() => {
                this.ResetPage();
              }, 2000)
          },
          error: () => 
          {
            this.SetError("Ops, tivemos um problema inesperado. Por favor tente novamente mais tarde.");
          }
        }
      )
  }

  private SetError(errorMessage:string){
    this.loading = false;
    this.uploaded = false;
    this.success = false;
    this.returnModel.message = errorMessage;
  }

  private SetSuccess(){
    this.completed = true;
    this.loading = false;
    this.uploaded = false;
    this.success = true;
  }

  private ResetPage(){
    this.file = null;
    this.uploaded = false;
    this.completed = false;
    this.loading = false;
    this.success = true;
    this.fileInput.nativeElement.value = "";
    }
}