import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookDetailComponent } from './books/book-detail/book-detail.component';
import { BookEditComponent } from './books/book-edit/book-edit.component';
import { BookListComponent } from './books/book-list/book-list.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guard/auth.guard';
import { PreventUnsavedGuard } from './_guard/prevent-unsaved.guard';

const routes: Routes = [
  {path:'', component:HomeComponent},
  {path:'book/:id', component:BookDetailComponent},
  {path:'book/edit/:id', component:BookEditComponent, pathMatch:'full', canDeactivate:[PreventUnsavedGuard]},
  {path:'errors', component:TestErrorsComponent},
  {path:'not-found', component:NotFoundComponent},
  {path:'server-error', component:ServerErrorComponent},
  {path:'books', component:BookListComponent, canActivate: [AuthGuard]},
  {path:'**', component:HomeComponent, pathMatch:'full'}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
