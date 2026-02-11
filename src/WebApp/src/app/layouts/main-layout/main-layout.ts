import { Component, effect, inject, Signal, ViewChild } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MaterialModule } from '../../shared/material/material-module';
import { ThemeService, ThemeType } from '../../shared/services/theme-service';
import { FormsModule } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { AuthService, ExpirationTokenState } from '@services/redarbor/auth-service';
import { OverlayContainer } from '@angular/cdk/overlay';


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
   * Inject the ExpirationTokenState to manage token expiration state
   */
  protected expiration:Signal<number>;

  /**
   * Inject the Router to manage navigation
   */
  private router: Router = inject(Router);
  

  /**
   * Available themes
   */
  protected themes: ThemeType[] = this.themeService.themes;

  /**
   * Current theme signal
   */
  protected currentTheme = this.themeService.currentTheme;


  list = [
    { isActive: true, name: 'Dashboard', route: '/redarbor/home', icon: 'home' },
    { isActive: false, name: 'Products', route: '/redarbor/products', icon: 'inventory' },
    // {isActive:false, name:'Orders', route:'/redarbor/orders', icon:'shopping_cart'},
    // {isActive:false, name:'Reports', route:'/redarbor/reports', icon:'bar_chart'},
    // {isActive:false, name:'Settings', route:'/redarbor/settings', icon:'settings'},
  ];

  @ViewChild('sidenav') sidenav!: MatSidenav;
  reason = '';

  constructor(private authService: AuthService,
    private overlayContainer: OverlayContainer
  ) {
    console.log('Constructor of MAIN Layout');
    
    this.expiration = inject(ExpirationTokenState).expiration;

    effect(() => {
      const exp = this.expiration();
      if (exp <= 0) {
        this.closeSession();
      }
      
    });
  }

  close(reason: string) {
    this.reason = reason;
    this.sidenav.close();
  }

  /**
   * Switch the application theme
   * @param theme Changed theme
   */
  switchTheme(theme: ThemeType): void {
    this.themeService.setCurrentTheme(theme);
    const overlayContainerElement = this.overlayContainer.getContainerElement();
    overlayContainerElement.classList.remove(...this.themeService.themes);
    overlayContainerElement.classList.add(theme);
  }

  

  /**
   * Navigate to specified route
   * @param route Route to navigate
   */
  goToLink(route: string): void {
    this.router.navigate([route]);
  }


  closeSession(): void {

    this.authService.closeSession();
    this.router.navigate(['/auth']);
  }

}
