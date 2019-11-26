uniform sampler2D texture;
uniform float alpha;

void main() {

    vec4 pixel = texture2D(texture, gl_TexCoord[0].xy);
    float gray = (pixel.r + pixel.g + pixel.b) / 3;

    vec4(gray, gray, gray, alpha) * gl_Color;
    
    if (pixel.w < 1) gl_FragColor = vec4(0,0,0,0); 
    else  gl_FragColor = vec4(gray, gray, gray, alpha) * gl_Color;
    
}