<template>
  <div class="NewsFeed container" id="mainContainer">
    <div class="container text-center mt-5" id="selectCategory">
      <input v-model="filtered" type="search" class="form-control" placeholder="Find by category">
    </div>
    <div class="row">
      <div
        v-for="item in filterNews"
        :key="item.Id"
        class="col-sm-6 col-md-4 col-lg-3"
        id="simpleColumn"
      >
        <div class="card" style="height:100%;" id="simpleCard">
          <div class="imageInside">
            <img :src="item.Image" class="card-img-top" alt id="itemImage">
          </div>
          <div class="card-body" id="simpleCard">
            <h6 class="card-title">{{item.Title}}</h6>
                <p class="card-text mt-4">{{item.Description}}</p>
                <a :href="item.Link" class="btn btn-outline-dark mt-1 mb-1">Read more</a>
          </div>
          <div class="card-footer border-dark">{{item.PublishDate}}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "NewsFeed",

  data() {
    return {
      filtered: '',
      NewsFeed: []
    };
  },

  methods: {
    fetchNewsFeed() {
      this.$http
        .get("http://localhost:5000/api/newsfeed")
        .then(response => response.json())
        .then(result => (this.NewsFeed = result));
    }
  },

  computed: {
    filterNews() {
      const search = this.filtered.toLowerCase().trim();

      if (!search) return this.NewsFeed;

      return this.NewsFeed.filter(
        c => c.Category.toLowerCase().indexOf(search) > -1
      );
    }
  },

  created: function() {
    this.fetchNewsFeed();
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

#simpleCard:hover {
  background: gray;
}

#simpleColumn {
  height: 600px;
  padding: 10px;
  color: bisque;
}

.imageInside {
  background-color: rgba(0, 0, 0, 0.4);
}

#itemImage {
  display: block;
  // margin-left: auto;
  // margin-right: auto;
  width: 100%;
  height: 100%;
}

#selectCategory {
  padding: 20px 0 20px 0;
}

#selectCategory select {
  border: 2px solid gray;
  width: 200px;
  height: 30px;
}

#simpleCard {
  text-align: center;
  background-color: rgba(0, 0, 0, 0.2);
  width: 100%;
}

.card-footer {
  background-color: rgba(0, 0, 0, 0.2);
}

input {
  background: none;
  text-align: center;
  margin-left: auto;
  margin-right: auto;
  border: none;
  border-bottom: 2px solid rgba(0, 0, 0, 0.3);
  width: 300px;
}

input:focus {
  background: none;
  text-align: center;
  margin-left: auto;
  margin-right: auto;
  border: none;
  border-bottom: 2px solid rgba(0, 0, 0, 0.5);
  width: 300px;
  outline: none;
  box-shadow: none;
}

h6 {
  font-weight: bold;
}

p {
  font-size: 12px;
}
</style>
