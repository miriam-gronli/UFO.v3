import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Bruker } from "../Bruker";

@Component({
  templateUrl: "loggInn.html"
})

export class LoggUt {

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
  }

  loggUt() {
    this.router.navigate(['/loggInn']);
  }
}
