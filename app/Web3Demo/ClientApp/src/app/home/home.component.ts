import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public model: ProcessDocumentRequestModel = new ProcessDocumentRequestModel();
  public responseInfo: ProcessDocumentResponseModel | undefined;
  public step: number = 0;

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    this.model = new ProcessDocumentRequestModel();
  }

  public processFileByUrl() {
    this.step = 1;
    this.http.post<ProcessDocumentResponseModel>(this.baseUrl + 'api/v1/documents', this.model).subscribe(result => {
      this.step = 2;
      this.responseInfo = result;
    }, error => console.error(error));
  }
}


interface ProcessDocumentResponseModel {
  FileUrl: string;
  rawNFT: string;
}


export class ProcessDocumentRequestModel {
  public PostUrl: string | undefined;
  public Token: string | undefined;
}
