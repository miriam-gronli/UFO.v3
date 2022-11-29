import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Observasjon } from "../Observasjon";

@Component({
  templateUrl: "lagre.html"
})
export class Lagre {  //All kode i denne filen, bortsett fra noen av inputvalideringene, er hentet fra "Kunde-SPA-Routing" modul videoen fra Canvas
  skjema: FormGroup;
  
  validering = {   //Inputvalideringen i denne filen er hentet fra modulvideoen "Inputvalidering" på canvas, og er ellers selvutviklet eller fra internett
    id: [""],
    navn: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\:_@#/,'()-. ]{2,30}")])
    ],
    postkode: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{4}")])
    ],
    beskrivelse: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\:_!@#/,'()-. ]{2,5000}")])
    ],
    dato: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\:/,-. ]{2,20}")])
    ],
    tid: [
      null, Validators.compose([Validators.required, Validators.pattern("([01]?[0-9]|2[0-3]):[0-5][0-9]")])
    ]
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.skjema = fb.group(this.validering);
  }

  vedSubmit() {
      this.lagreObservasjon();
  }

  lagreObservasjon() {
    const lagretObservasjon = new Observasjon();

    lagretObservasjon.navn = this.skjema.value.navn;
    lagretObservasjon.postkode = this.skjema.value.postkode;
    lagretObservasjon.beskrivelse = this.skjema.value.beskrivelse;
    lagretObservasjon.dato = this.skjema.value.dato;
    lagretObservasjon.tid = this.skjema.value.tid;

    this.http.post("api/observasjon", lagretObservasjon)
      .subscribe(retur => {
        this.router.navigate(['/liste']);
      },
       error => console.log(error)
      );
  };
}
