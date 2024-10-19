import { Injectable } from '@angular/core';
import { AbstractService } from './abstract.service';
import { HttpClient } from '@angular/common/http';
import { AppSettingsService } from './app-settings.service';
import { Observable } from 'rxjs/internal/Observable';
import { Login } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends AbstractService {
  private readonly urlRoute: string = 'Login';

  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
		super(appSettingsService);
	}

  login(data: Login): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/${this.urlRoute}`, data, { responseType: 'text' as 'json' });
	}
}
