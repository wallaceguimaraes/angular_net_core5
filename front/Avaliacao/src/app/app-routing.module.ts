import { DashboardComponent } from './dashboard/dashboard.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { RelatoriosComponent } from './relatorios/relatorios.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PerfilComponent } from './perfil/perfil.component';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  { path: 'relatorios', component: RelatoriosComponent},
  { path: 'usuarios', component: UsuariosComponent},
  { path: 'dashboard', component: DashboardComponent},
  { path: 'perfil', component: PerfilComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
