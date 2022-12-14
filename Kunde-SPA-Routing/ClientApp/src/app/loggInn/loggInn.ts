import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Bruker } from "../Bruker";

//Kilde:https://github.com/karol-121/Webapplikasjoner-Oppgave/blob/main/Webapplication-Admin/Webapplication-Admin/ClientApp/src/app/logg-inn/logg-inn.ts , basert på kode hentet herfra og bygget på videre

@Component({
  templateUrl: "loggInn.html"
})

export class LoggInn {  
  skjema_loggInn: FormGroup;
  alertContent: string;
   
  formProfile = {  //Inputvalideringen i denne filen er hentet fra modulvideoen "Inputvalidering" på canvas, og er ellers selvutviklet eller fra internett
    brukernavn: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-Z0-9\-_]{3,15}")])],
    passord: [null, Validators.compose([Validators.required, Validators.pattern("[0-9A-Za-z]{4,64}")])]
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.skjema_loggInn = fb.group(this.formProfile);
    this.alertContent = null;
  }

  dissmissAlert() {
    this.alertContent = null;
  }


  autenticate() {
    const bruker = new Bruker();
    bruker.brukernavn = this.skjema_loggInn.value.brukernavn;
    bruker.passord = this.skjema_loggInn.value.passord;

        
    this.http.post("api/observasjon/logginn", bruker)
      .subscribe(success => {
         this.router.navigate(['/liste']);
      }, error => {

      if (error.status === 401) {
        this.alertContent = "Kunne ikke autentisere, sjekk brukernavn og passord."
      }
     });
  }
}
