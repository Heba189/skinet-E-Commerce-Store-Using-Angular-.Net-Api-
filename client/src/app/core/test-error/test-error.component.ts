import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/app/environments/environment-development';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent {
baseurl=environment.apiUrl;
validationErrors : string[] = [];
constructor(private http : HttpClient) {}
get404Error(){
  this.http.get(this.baseurl + 'products/42').subscribe({
    next : response => console.log(response),
    error : error => console.log(error)
  })
}
get400Error(){
  this.http.get(this.baseurl + 'buggy/badrequest').subscribe({
    next : response => console.log(response),
    error : error => console.log(error)
  })
}
get500Error(){
  debugger;
  this.http.get(this.baseurl + 'buggy/servererror').subscribe({
    next : response => console.log(response),
    error : error => console.log(error)
  })
}
get400ValidationError(){
  debugger;
  this.http.get(this.baseurl + 'products/fortytwo').subscribe({
    next : response => console.log(response),
    error : error => {
      console.log(error);
      this.validationErrors = error.errors;
    }
  })
}
}
