Estimate 'reverse entropy' of a set of moving 2D-particles inside a 2D box.<br>
Kees van Zon.<br>
Published under MIT license.<br>

The system's entropy is (roughly speaking) a measure of the degree to which the particles are spreadout in the box.
This toy tool determines a simple entropy of a given configuration of particles as the logarithm of the average distance between
the particles.

Usage:
* Set grid size G; splits box into GxG squares used for reverse entropy calculations.
* Set number of particles in box.
* Set maximum initial horizontal and vertical particle speed Vmax. The initial speed of each particle in either direction is random
between [-Vmax, Vmax].
* Set a seed for the pseudo-random number generator.
* Check 'Display motion'.
* Hit Start to commence simulation.

While running, the number of iterations and the minimum entropy encountered are listed.
While stopped, the particle configuration can be saved, reset, or loaded. Moreover, the simulation's particle configuration with
the smallest entropy can be loaded.

Executable last compiled and tested on Windows 10 with .Net 4.5