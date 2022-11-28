import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Lagre } from './lagre/lagre';
import { Liste } from './liste/liste';
import { Endre } from './endre/endre';
import { LoggInn } from './loggInn/loggInn';
import { LoggUt } from './loggInn/loggUt';

const appRoots: Routes = [
  { path: 'liste', component: Liste },
  { path: 'lagre', component: Lagre },
  { path: 'endre/:id', component: Endre, },
  { path: 'loggInn', component: LoggInn },
  { path: 'loggUt', component: LoggUt },
  { path: '', redirectTo: '/loggInn', pathMatch: 'full' }
]

@NgModule({
  imports: [
    RouterModule.forRoot(appRoots)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
