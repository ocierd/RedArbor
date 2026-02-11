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
import { MatListModule } from "@angular/material/list";
import { MatDialogModule } from "@angular/material/dialog";
import { DialogModule } from "@angular/cdk/dialog";
import { OverlayModule } from "@angular/cdk/overlay";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatTableModule } from "@angular/material/table";

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
    MatListModule,
    MatDialogModule,
    DialogModule,
    MatTooltipModule,
    OverlayModule,
    MatTableModule
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