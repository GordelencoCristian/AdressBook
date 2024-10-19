import { provideRouter } from '@angular/router';
import { APP_INITIALIZER, ApplicationConfig, importProvidersFrom, provideZoneChangeDetection} from "@angular/core";

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';

import {TranslateModule, TranslateLoader} from "@ngx-translate/core";
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {HttpClient, HttpHandlerFn, HttpInterceptorFn, HttpRequest, provideHttpClient, withInterceptors} from '@angular/common/http';
import { PageTitleComponent } from './utils/shared/page-title/page-title.component';

import { catchError, Observable, of, tap } from 'rxjs';
import { Login } from './utils/models/login.model';
import { AppSettingsService } from './utils/services/app-settings.service';
import { AuthService } from './utils/services/auth.service';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatNativeDateModule } from '@angular/material/core';

export function initializeAppFactory(
  appSettingsService: AppSettingsService,
  authenticationService: AuthService
): () => Observable<any> {
  return () => {
    const login = appSettingsService.settings.LOGIN ?? "";
    const password = appSettingsService.settings.PASSWORD ?? "";

    if (!authenticationService || !authenticationService.login) {
      throw new Error('AuthService or login method is not defined');
    }

    return authenticationService.login(new Login(login, password)).pipe(
      tap((token: string) => {
        localStorage.setItem('token', token);
      }),
      catchError((error) => {
        return of(null);
      })
    );
  };
}

export const httpLoaderFactory: (http: HttpClient) => TranslateHttpLoader = (http: HttpClient) =>
  new TranslateHttpLoader(http, './i18n/', '.json');

export const authenticationInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next:
  HttpHandlerFn) => {
    const token = localStorage.getItem('token');
    if (token) {
      const modifiedReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`),
      });

      return next(modifiedReq);
    }

    return next(req);
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
     provideRouter(routes),
     provideClientHydration(),
     provideHttpClient(withInterceptors([authenticationInterceptor])),
     {
      provide: APP_INITIALIZER,
      useFactory: initializeAppFactory,
      multi: true,
      deps: [AppSettingsService, AuthService],
    },
    importProvidersFrom(MatNativeDateModule),
    importProvidersFrom([TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpLoaderFactory,
        deps: [HttpClient],
      },
    })]), provideAnimationsAsync()
    ]
};

export const globalModules = [
  TranslateModule,
  PageTitleComponent,
]


