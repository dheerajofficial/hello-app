import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export class Person {
  "FirstName" : string;
  "LastName" : string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  baseUrl: string;
  http: HttpClient;
  model = new Person();
  mesg: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.baseUrl = baseUrl;
    this.http = http;
  }

  onSubmit(form:any) {
    console.log(form.value)
    this.http.post<Person>(this.baseUrl + 'person', form.value).subscribe(result => {
      this.mesg = "Person data saved successfully! check Person info link"
    }, error => console.error(error));
  }
}
