import { Injectable } from '@angular/core';
import { AppSettings } from '../models/app-settings.model';
import { IAppSettings } from '../interfaces/appsettings.interface';
import jsonData from '../../assets/config/appsettings.json';

@Injectable({
  providedIn: 'root',
})
export class AppSettingsService {
  private appSettings: IAppSettings = new AppSettings();

  constructor() {
    this.getFromJson();
  }

  get settings(): IAppSettings {
    return this.appSettings;
  }

  public getFromJson(){
    this.appSettings.APP_BASE_URL = jsonData.APP_BASE_URL
    this.appSettings.LOGIN = jsonData.LOGIN
    this.appSettings.PASSWORD = jsonData.PASSWORD
  }
}
