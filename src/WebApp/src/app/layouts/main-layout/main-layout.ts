import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';


/**
 * Represents the main layout of the application.
 * Includes the router outlet for child routes
 */
@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss',
  imports:[RouterOutlet]
})
export class MainLayout {

}
