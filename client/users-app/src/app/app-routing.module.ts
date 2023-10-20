import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TableComponent } from './table/table.component';
import { ViewComponent } from './view/view.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
  { path: '', component: TableComponent, pathMatch: 'full' },
  { path: ':id/view', component: ViewComponent },
  { path: ':id/edit', component: EditComponent },
  { path: 'create', component: EditComponent },
  { path: '***', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
