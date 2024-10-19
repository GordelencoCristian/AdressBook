import { IAppSettings } from "../interfaces/appsettings.interface";

export class AppSettings implements IAppSettings {
  APP_BASE_URL: string;
  LOGIN: string | null = null;
  PASSWORD: string | null = null;

	constructor(config?: IAppSettings) {
		if (config) {
			this.APP_BASE_URL = config.APP_BASE_URL;
		} else {
      this.APP_BASE_URL = "";
		}
	}

}
