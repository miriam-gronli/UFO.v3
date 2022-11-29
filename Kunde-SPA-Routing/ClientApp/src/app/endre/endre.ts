import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Observasjon } from "../Observasjon";

@Component({
  templateUrl: "endre.html"
})
export class Endre {   //All kode i denne filen, bortsett fra noen av inputvalideringene, er hentet fra "Kunde-SPA-Routing" modul videoen fra Canvas

  skjema: FormGroup;
  
  validering = {  //Inputvalideringen i denne filen er hentet fra modulvideoen "Inputvalidering" på canvas, og er ellers selvutviklet eller fra internett

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

  constructor(private http: HttpClient, private fb: FormBuilder,
              private route: ActivatedRoute, private router: Router) {
      this.skjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.endreObservasjon(params.id);
    })
  }

  vedSubmit() {
      this.endreEnObservasjon();
  }

  endreObservasjon(id: number) {
    this.http.get<Observasjon>("api/observasjon/" + id)
      .subscribe(
        observasjon => {
          this.skjema.patchValue({ id: observasjon.id });
          this.skjema.patchValue({ navn: observasjon.navn });
          this.skjema.patchValue({ postkode: observasjon.postkode });
          this.skjema.patchValue({ beskrivelse: observasjon.beskrivelse });
          this.skjema.patchValue({ dato: observasjon.dato });
          this.skjema.patchValue({ tid: observasjon.tid });
        },
        error => console.log(error)
      );
  }

  endreEnObservasjon() {
    const endretObservasjon = new Observasjon();
    endretObservasjon.id = this.skjema.value.id;
    endretObservasjon.navn = this.skjema.value.navn;
    endretObservasjon.postkode = this.skjema.value.postkode;
    endretObservasjon.beskrivelse = this.skjema.value.beskrivelse;
    endretObservasjon.dato = this.skjema.value.dato;
    endretObservasjon.tid = this.skjema.value.tid;

    this.http.put("api/observasjon/", endretObservasjon)
      .subscribe(
        retur => {
          this.router.navigate(['/liste']);
        },
        error => console.log(error)
      );
  }
}
