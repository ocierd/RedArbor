import { NgModule } from "@angular/core";


import { MatFormFieldModule } from "@angular/material/form-field";
import { MatOptionModule } from "@angular/material/core";
import { MatInputModule } from "@angular/material/input";

/**
 * Material Design components module
 * Centralized module to import and export Angular Material components
 */
const materialModules = [
    MatFormFieldModule,
    MatOptionModule,
    MatInputModule
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