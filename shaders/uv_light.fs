precision mediump float;

varying vec3 normout;
varying vec2 texcoordout;
varying vec3 lightVector;
varying float dist;

uniform sampler2D tex0;
uniform vec3 unib[4];
//uniform float blend ====> unib[0][2]
uniform vec3 unif[16];
//uniform vec3 fogshade ==> unif[4]
//uniform float fogdist ==> unif[5][0]
//uniform float fogalpha => unif[5][1]
//uniform vec3 lightcol => unif[9]
//uniform vec3 lightamb => unif[10]

void main(void) {
  vec4 texc = texture2D(tex0, texcoordout); // ------ basic colour from texture

  float ffact = smoothstep(unif[5][0]/3.0, unif[5][0], dist); // ------ smoothly increase fog between 1/3 and full fogdist

  float intensity = clamp(dot(lightVector, vec3(0.0, 0.0, 1.0)), 0.0, 1.0); // ------ adjustment of colour according to combined normal
  if (texc.a < unib[0][2]) discard; // ------ to allow rendering behind the transparent parts of this object
  texc.rgb = (texc.rgb * unif[9]) * intensity + (texc.rgb * unif[10]); // ------ directional lightcol * intensity + ambient lightcol

  gl_FragColor =  (1.0 - ffact) * texc + ffact * vec4(unif[4], unif[5][1]); // ------ combine using factors
}


