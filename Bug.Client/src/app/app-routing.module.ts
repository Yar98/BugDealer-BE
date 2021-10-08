import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';
import { TestComponent } from './test/test.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./modules/authencation/authencation.module').then(mod => mod.AuthencationModule)
  },
  { path: 'test', component: TestComponent },
  { path: '404', component: PageNotFoundComponent },
  { path: '**', redirectTo: '/' + '404' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'enabled',
    preloadingStrategy: PreloadAllModules
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
