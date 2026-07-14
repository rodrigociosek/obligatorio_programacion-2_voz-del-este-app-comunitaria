# Casos de uso — Voz del Este

Actor principal: **Usuario** de la aplicación móvil/escritorio Voz del Este.

## CU-01 Iniciar sesión

**Precondiciones**: el usuario tiene una cuenta registrada.

**Flujo principal**:
1. El usuario abre la app y ve la pantalla de Login.
2. Ingresa alias y contraseña (la contraseña se muestra oculta).
3. Presiona "Ingresar".
4. El sistema valida las credenciales contra la base local.
5. Si son correctas, el usuario accede al menú principal (`AppShell`).

**Flujos alternativos**:
- 4a. Alias o contraseña incorrectos → se muestra "Usuario o clave incorrecta" y el usuario permanece en Login.
- En dispositivo móvil, el usuario puede presionar "Ingresar con huella" en lugar de escribir credenciales; si la huella es reconocida, ingresa directamente.

**Poscondiciones**: sesión iniciada, se navega a `AppShell`.

---

## CU-02 Registrarse

**Precondiciones**: el usuario no tiene cuenta o quiere crear una nueva.

**Flujo principal**:
1. Desde Login, el usuario presiona "Registrarse".
2. Completa alias, contraseña (oculta), nombre completo, dirección, teléfono, email y, opcionalmente, una foto de perfil.
3. Presiona "Registrarse".
4. El sistema valida que ningún campo obligatorio esté vacío (alias, contraseña, nombre completo, teléfono, email).
5. El sistema valida que el email tenga formato válido.
6. El sistema valida que el alias y el email no estén ya registrados.
7. Si todo es válido, se crea el usuario y se muestra "Usuario creado correctamente".

**Flujos alternativos**:
- 4a. Falta un campo obligatorio → se indica cuál específicamente (ej. "Falta el alias", "Falta el email").
- 5a. Email con formato inválido → "El email no es válido".
- 6a. Alias ya registrado → "El alias ya está registrado".
- 6b. Email ya registrado → "El email ya está registrado".
- 7a. Error inesperado al guardar → "Hubo un error".

**Poscondiciones**: nuevo usuario persistido en SQLite local.

---

## CU-03 Navegar el menú principal

**Precondiciones**: sesión iniciada.

**Flujo principal**:
1. El usuario ve accesos a Clima, Cotizaciones, Noticias, Películas y Patrocinadores.
2. Puede abrir el menú de usuario (ver información de cuenta, ajustes, o cerrar sesión).
3. Selecciona una sección y navega a ella.

**Flujos alternativos**:
- El usuario cierra sesión → vuelve a la pantalla de Login.
- El usuario abre Ajustes → puede modificar sus preferencias y aplicarlas.

---

## CU-04 Consultar clima

**Precondiciones**: sesión iniciada, conexión a internet disponible (para datos nuevos).

**Flujo principal**:
1. El usuario entra a la sección Clima.
2. El sistema busca el último registro guardado; si no existe o pasaron más de 3 minutos, consulta la API de OpenWeatherMap para Maldonado, UY.
3. Se muestra el clima actual (temperatura, estado, humedad, viento) y el pronóstico.
4. El fondo de la pantalla cambia según el estado del clima (despejado, nublado, lluvia).

**Flujos alternativos**:
- 2a. Falla la consulta a la API → se lanza una excepción con el detalle del error HTTP.
- Si hay un registro reciente (menos de 3 minutos), se usa el dato guardado sin llamar a la API.

---

## CU-05 Consultar cotizaciones

**Precondiciones**: sesión iniciada, conexión a internet disponible (para datos nuevos).

**Flujo principal**:
1. El usuario entra a la sección Cotizaciones.
2. El sistema busca el último registro guardado; si no existe o pasaron más de 20 minutos, consulta CurrencyLayer (EUR, USD, ARS, BRL respecto al peso uruguayo).
3. Se muestran las cotizaciones actuales.

**Flujos alternativos**:
- 2a. Falla la consulta a la API → se lanza una excepción con el detalle del error HTTP.
- Si hay un registro con menos de 20 minutos de antigüedad, se reutiliza sin llamar a la API.

---

## CU-06 Buscar noticias

**Precondiciones**: sesión iniciada, conexión a internet disponible.

**Flujo principal**:
1. El usuario entra a la sección Noticias.
2. Ingresa una palabra clave y presiona "Buscar".
3. El sistema consulta NewsData.io filtrando por Uruguay/español y la palabra clave.
4. Se muestra la lista de noticias encontradas y un aviso de éxito.
5. El usuario toca una noticia y se abre un modal con el detalle.

**Flujos alternativos**:
- 3a. Error de red o de la API → se muestra el mensaje de error.
- 3b. Sin resultados → la lista queda vacía.

---

## CU-07 Buscar películas

**Precondiciones**: sesión iniciada, conexión a internet disponible.

**Flujo principal**:
1. El usuario entra a la sección Películas.
2. Puede escribir un título, seleccionar uno o más géneros, o ambos.
3. Presiona "Buscar".
4. El sistema consulta TheMovieDB por nombre y/o género(s) seleccionados.
5. Se muestra la lista de películas (póster, título, año, valoración).
6. El usuario toca una película y se abre un modal con el detalle.

**Flujos alternativos**:
- 2a. No se escribió texto ni se seleccionó ningún género → "Debe seleccionar una categoria o ingresar un nombre", no se realiza la búsqueda.
- 4a. La API no devuelve resultados → se muestra un aviso "No se ha encontrado ninguna pelicula".

---

## CU-08 Gestionar patrocinadores

**Precondiciones**: sesión iniciada.

### CU-08a Ver listado
1. El usuario entra a Patrocinadores.
2. Se listan todos los patrocinadores guardados localmente (nombre, logo).

### CU-08b Agregar patrocinador
1. El usuario presiona "Agregar Patrocinador".
2. Completa el nombre y selecciona la ubicación tocando el mapa (Leaflet embebido).
3. Presiona guardar.
4. El sistema valida que el nombre no esté vacío → si falta, "Falta el nombre del patrocinador" y no continúa.
5. El sistema valida que se haya seleccionado una ubicación (latitud/longitud distintas de 0) → si falta, "Falta la ubicación del patrocinador".
6. Se guarda el patrocinador y vuelve al listado.

### CU-08c Editar patrocinador
1. El usuario presiona "Editar" sobre un patrocinador.
2. Se cargan sus datos y su ubicación en el mapa.
3. El usuario modifica nombre y/o ubicación y confirma.
4. Mismas validaciones que en Agregar (nombre y ubicación obligatorios).
5. Se actualiza el registro y vuelve al listado.

### CU-08d Ver ubicación
1. El usuario presiona "Ver ubicación" sobre un patrocinador.
2. Se abre un modal con el mapa centrado en su ubicación guardada.

### CU-08e Eliminar patrocinador
1. El usuario presiona "Eliminar" sobre un patrocinador.
2. El sistema pide confirmación ("¿Estás seguro de que querés continuar?").
3. Si confirma, se elimina el registro y se muestra "Se ha eliminado {nombre} con exito".
4. Si cancela, no se realiza ningún cambio.

**Flujos alternativos**:
- 08e-2a. El patrocinador ya no existe al momento de eliminar → "No se encontro el patrocinador".
