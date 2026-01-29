import { effect, Injectable, signal, Signal, WritableSignal } from '@angular/core';


export type ThemeType = 'default-theme' | 'light-theme' | 'dark-theme';

/**
 * Service to manage application themes
 */
@Injectable({
  providedIn: 'root',
})
export class ThemeService {


  /**
   * Available themes
   */
  public readonly themes: ThemeType[] = ['default-theme', 'light-theme', 'dark-theme'];

  /**
   * Current theme signal
   */
  public readonly currentTheme: WritableSignal<ThemeType> = signal(this.themes[0]);

  
  constructor() { 
    effect(() => {
      document.body.className = this.currentTheme();
      console.log('Changed to ', this.currentTheme());
      
    });
  }

  /**
   * Set the current theme
   * @param theme Theme to be set as current
   */
  setCurrentTheme(theme: ThemeType): void {
    this.currentTheme.set(theme);
  }

}
