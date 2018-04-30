// AUTHOR: Jiaxin Dong
// FILENAME: p1.cs
// DATE: 4/29/2018
// VERSION: 2 (see git for changes)
//
// Description:
// This main primarily makes a call to the driver, where the driver
// manages running the encrypt word code.
//
// This primariy has no state other than always creating a new driver class
// which then has the driver run its code.
//
// Assumptions:
// It is assumed that no user input is necessary for a demonstration on how
// the EncryptWord object is used.
// It is assumed that this Main class will do nothing other than call the
// driver, and that the driver will do most of the work.

using System;

namespace cpsc5051 {
    class p1 {
        static void Main(string[] args) {
            driver d = new driver();
            d.run();
        }
    }
}
