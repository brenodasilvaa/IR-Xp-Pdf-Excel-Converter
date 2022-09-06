import { Component, OnInit } from '@angular/core';
import { delay, timeInterval, timeout } from 'rxjs';
import { AnyCatcher } from 'rxjs/internal/AnyCatcher';
import { FileUploadService } from 'src/services/file-upload.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  // Variable to store shortLink from api response
  shortLink: string = "";
  loading: boolean = false; // Flag variable
  completed: boolean = false;
  uploaded: boolean = false;
  file: any = null; // Variable to store file

  // Inject service 
  constructor(private fileUploadService: FileUploadService) { }

  ngOnInit(): void {}

  // On file Select
  onChange(event: any) {
      this.file = event.target.files[0];
      this.uploaded = true;
      this.completed = false;
      document.getElementById('btn-upload')?.removeAttribute('disabled')
  }

  // OnClick of button Upload
  onUpload() {
      this.loading = true;
      setTimeout(() => {
        this.completed = true;
        this.loading = false;
        this.uploaded = false;
        console.log(this.file);
      }, 5000);


      // this.fileUploadService.upload(this.file).subscribe(
      //     (event: any) => {
      //         if (typeof (event) === 'object') {

      //             // Short link via api response
      //             this.shortLink = event.link;

      //             this.loading = false; // Flag variable 
      //         }
      //     }
      // );
  }
}
