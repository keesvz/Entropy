Estimate 'reverse entropy' of a set of moving 2D-particles inside a 2D box. 
Kees van Zon.
Published under MIT license.

The system's entropy is (roughly speaking) the degree to which the particles are spreadout in the box.
This toy tool determines a simple 'reverse entropy' - the degree to which the particles are clustered together - defined as
* X: sum(n) (number of particles in row n)^2)
* Y: sum(n) (number of particles in column n)^2)
* XY: sum(n) (number of particles in row n-1 + number of particles in row n) * 
             (number of particles in column n-1 + number of particles in column n)

Usage:
* Set grid size G; splits box into GxG squares used for reverse entropy calculations.
* Set number of particles in box.
* Set maximum initial horizontal and vertical particle speed Vmax. The initial speed of each particle is random
between [-Vmax, Vmax].
* Set a seed for the pseudo-random number generator.
* Check 'Display motion'.
* Hit Start to commence simulation.

While running, the number of iterations and the maximum X, Y ans XY reverse entropy are listed.
While stopped, the particle configuration can be saved, reset, or loaded Moreover, particle configurations with maximum
reverse entropy encountered during the simulation can be loaded.
