import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { BookEditComponent } from '../books/book-edit/book-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedGuard implements CanDeactivate<unknown> {
  canDeactivate(
    component: BookEditComponent): boolean {
      if(component.formEdit.dirty){
        return confirm("you have unsaved changes");
      }
    return true;
  }

}
