<template>
  <div id="app" class="container-fluid">
    <nav class="navbar fixed-top navbar-expand-lg navbar-dark bg-dark">
      <a class="navbar-brand mb-1" href="#">News</a>
      <button
        class="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav mr-auto">
          <li class="nav-item active">
            <router-link to="/" class="nav-link">
              Home
              <span class="sr-only">(current)</span>
            </router-link>
          </li>
        </ul>
      </div>
    </nav>
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'app',
};

</script>

<style lang="scss" scoped>

body {
  // background: url('../src/assets/background.jpg') no-repeat;
  // background-size:cover;
}
#app {
  padding: 0 0 0 0;
}

nav a {
  width: 50px;
  height: 40px;
}
</style>
