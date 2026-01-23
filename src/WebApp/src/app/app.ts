import { Component, signal } from '@angular/core';
import { ActivatedRoute, RouterOutlet } from '@angular/router';

/**
 * Root component of the application
 */
@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('RedArborApp');

  constructor(activeRoute: ActivatedRoute) {

    activeRoute.url.subscribe(urlSegment => {
      if (urlSegment.length > 0) {
        this.title.set(`RedArborApp - ${urlSegment[0].path}`);
      } else {
        this.title.set('RedArborApp');
      }
    });
  }
}
