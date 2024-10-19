import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService } from './abstract.service';
import { AppSettingsService } from './app-settings.service';
import { Observable } from 'rxjs';
import { Contact } from '../interfaces/contact.interface';
import { Response } from '../interfaces/response.interface';

@Injectable({
  providedIn: 'root'
})
export class ContactService extends AbstractService {
  private readonly urlRoute: string = 'Contacts';

  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
		super(appSettingsService);
	}

  get(id: number): Observable<Response<Contact>> {
		return this.http.get<Response<Contact>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	addOrUpdate(data: any): Observable<Response<number>> {
		return this.http.post<Response<number>>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	delete(id: number): Observable<Response<any>> {
		return this.http.delete<Response<any>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	list(data: any): Observable<Response<Contact[]>> {
		return this.http.get<Response<Contact[]>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}
}
