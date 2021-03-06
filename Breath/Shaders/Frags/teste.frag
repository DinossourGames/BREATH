﻿
#version 130
uniform sampler2D texture; // The main input texture (the screen.)
uniform sampler2D palette; // The palette texture.
uniform float shift; // The shift amount on the palette texture.
uniform float offset; // The offset for the random noise generation.
uniform float screenScale; // The current scale of the screen. (1x, 2x, etc)
uniform vec2 screenSize; // The size of the core game screen (320 x 240)
uniform float noiseAlpha; // The amount of alpha the noise should have.

// A weird way to generate a random number with a vec2 seed.
float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

void main() {
    // The size of the game window.
    vec2 screenSizeScaled = screenScale * screenSize;
    // The pixel coordinate being operated on.
    vec2 pixpos = gl_TexCoord[0].xy;

    // Get dither pixel
    vec2 overlayCoord =  gl_FragCoord.xy / 10; //floor(gl_FragCoord.xy / screenScale);
    // Get 1 or 0 based on the pixel location.
    float overlayPixelColor = mod(overlayCoord.x + overlayCoord.y, 2);
    // Dither is black and white every other pixel.
    vec4 overlayPixel = vec4(overlayPixelColor, overlayPixelColor, overlayPixelColor, 1);

   
    
//    // Scale the frag position to match the screen scale
//    vec2 scaledpos = floor(pixpos * screenSizeScaled);
//    // Adjust the position based on the scale of the screen.
//    scaledpos -= mod(scaledpos, screenScale);
//    // Convert back to 0 - 1 coordinate space.
//    scaledpos /= screenSizeScaled;
//
//    // Get base color.
    vec4 pixcol = texture2D(texture, pixpos);
//
//    // Mix dither texture.
//    pixcol = mix(pixcol, overlayPixel, 0.1);
//
//    // Determine the brightness of the pixel in a dumb way.
//    float gray = (pixcol.r + pixcol.g + pixcol.b) / 3;
//
//    // Round it to the nearest 0.25.
//    gray = round(gray / 0.25) * 0.25;
//
//    // Add some noise.
//    gray += (rand(scaledpos + offset) * 2 - 1) * noiseAlpha;
//
//    // Map the palette to the pixel based on the brightness and shift.
//    pixcol = texture2D(palette, vec2(gray, shift));
//
//    // Multiply through the gl_Color for final output.
    gl_FragColor = pixcol  * overlayPixel * gl_Color * vec4(.8,.2,.3,1);
}