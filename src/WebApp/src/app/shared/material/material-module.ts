import { NgModule } from "@angular/core";


import { MatFormFieldModule } from "@angular/material/form-field";
import { MatOptionModule } from "@angular/material/core";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { MatRadioModule } from "@angular/material/radio";
import { MatIconModule } from "@angular/material/icon";
import { MatMenuModule } from "@angular/material/menu";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatListModule, MatNavList } from "@angular/material/list";

/**
 * Material Design components module
 * Centralized module to import and export Angular Material components
 */
const materialModules = [
    MatFormFieldModule,
    MatOptionModule,
    MatInputModule,
    MatButtonModule,
    MatRadioModule,
    MatIconModule,
    MatMenuModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule
    
];

@NgModule({
    declarations: [],

    imports: [
        ...materialModules
    ],
    exports: [
        ...materialModules
    ],
    

})
export class MaterialModule {


}