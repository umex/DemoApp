import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  model:any = {}
  currentUser$ = this.accountService.currentUser$;
  siteLanguage = "En"

  languageList = [
    { code: 'en', label: 'En' },
    { code: 'sl', label: 'Slo' },
  ];

  constructor(public translate: TranslateService,private accountService:AccountService, private router: Router, private toastr:ToastrService) {
    // Register translation languages
    translate.addLangs(['en', 'sl']);
    // Set default language
    translate.setDefaultLang('en');

    console.log("browser lang:", this.translate.getBrowserLang())

  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$
  }

  login(){
    this.accountService.login(this.model).subscribe(response =>{
      this.router.navigateByUrl('/books');
    },error =>{
      console.log(error)
      //this.toastr.error(error.error)
    });
  }

  logout(){
    this.accountService.logout();
  }
  //Switch language
  translateLanguageTo(lang: string) {
    this.translate.use(lang);
  }

  changeSiteLanguage(localeCode: string): void {
    const selectedLanguage = this.languageList.find((language) => language.code === localeCode)?.label.toString();
    if (selectedLanguage) {
      this.siteLanguage = selectedLanguage;
      this.translate.use(localeCode);
    }
    const currentLanguage = this.translate.currentLang;
    console.log('currentLanguage', currentLanguage);
  }

}
