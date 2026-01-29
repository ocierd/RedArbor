import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MaterialModule } from '../../shared/material/material-module';
import { ThemeService, ThemeType } from '../../shared/services/theme-service';
import { FormsModule } from '@angular/forms';


/**
 * Represents the main layout of the application.
 * Includes the router outlet for child routes
 */
@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss',
  imports: [RouterOutlet, MaterialModule, FormsModule]
})
export class MainLayout {

  /**
   * Inject the ThemeService to manage application themes
   */
  private themeService: ThemeService = inject(ThemeService);

  /**
   * Available themes
   */
  protected themes: ThemeType[] = this.themeService.themes;

  /**
   * Current theme signal
   */
  protected currentTheme = this.themeService.currentTheme;


  /**
   * Switch the application theme
   * @param theme Changed theme
   */
  switchTheme(theme: ThemeType): void {
    this.themeService.setCurrentTheme(theme);
  }


}
