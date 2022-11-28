import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Observasjon } from "../Observasjon";

@Component({
  templateUrl: "liste.html"
})
export class Liste {
  alleObservasjoner: Array<Observasjon>;
  laster: boolean;

  constructor(private http: HttpClient,private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleObservasjoner();
  }

  hentAlleObservasjoner() {
    this.http.get<Observasjon[]>("api/observasjon/")
      .subscribe(observasjonene => {
        this.alleObservasjoner = observasjonene;
        this.laster = false;
      },
       error => console.log(error)
      );
  };

  sletteObservasjon(id: number) {
    this.http.delete("api/observasjon/" + id)
      .subscribe(retur => {
        this.hentAlleObservasjoner();
        this.router.navigate(['/liste']);
      },
       error => console.log(error)
      );
  };
  // CHANGED to call logout backend and redirect
  loggUt() {
    this.http.post("api/observasjon/loggut", {})
      .subscribe(success => {
        this.router.navigate(['/']);
      });
  };
}
