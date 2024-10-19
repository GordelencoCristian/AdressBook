import { Injectable } from '@angular/core';
import { AppSettingsService } from './app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class AbstractService {
	private readonly apiVersion = 'api';
	protected appUrl: string | undefined;

  constructor(protected service: AppSettingsService) {
		this.initConfig();
	}

  initConfig(): void {
      this.appUrl = this.service.settings.APP_BASE_URL;
  }

  createBaseUrl(SERVER_URL: string): string {
		return `${SERVER_URL}/${this.apiVersion}`;
	}

  protected get baseUrl(): string
  {
    return this.createBaseUrl(this.service.settings.APP_BASE_URL);
  }
}
