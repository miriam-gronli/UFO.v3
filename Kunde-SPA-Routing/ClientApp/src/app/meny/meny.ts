import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-meny',
  templateUrl: './meny.html'
})
export class Meny { //All kode i denne filen er hentet fra "Kunde-SPA-Routing" modul videoen på Canvas
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
