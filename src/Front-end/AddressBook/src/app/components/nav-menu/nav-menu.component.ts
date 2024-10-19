import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { globalModules } from '../../app.config';
import { SelectItem } from '../../utils/interfaces/select-item.interface';
import { RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [...globalModules,
    RouterModule,
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    FormsModule],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.scss',
})

export class NavMenuComponent {
  isCollapsed: boolean;

  public languages: SelectItem[] = [
    { label: 'English', value: 'en' },
    { label: 'Romanian', value: 'ro' }
  ];

  constructor(private translate: TranslateService) {
    this.isCollapsed = true;

    this.translate.addLangs(['ro', 'en']);
    this.translate.setDefaultLang('en');
    this.translate.use(this.translate.getBrowserLang() || 'en');
  }

  useLanguage(language: string): void {
    this.translate.use(language);
  }

  collapseNavMenu() {
    if (!this.isCollapsed) {
      this.isCollapsed = !this.isCollapsed;
    }
  }
}
