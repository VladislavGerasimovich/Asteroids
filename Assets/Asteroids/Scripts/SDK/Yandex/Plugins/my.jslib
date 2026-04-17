mergeInto(LibraryManager.library, {

  	Hello: function () {
    	window.alert("Hello, world!");
    	console.log("Hello, world!");
  	},

  	ShowAdv: function () {
  	    ysdk.adv.showFullscreenAdv({
            callbacks: {
                onOpen: function() {
                  // Действие после открытия рекламы.
                },
                onClose: function(wasShown) {
                  // Действие после закрытия рекламы.
                },
                onError: function(error) {
                  // Действие в случае ошибки.
                },
            }
        })
  	},

  });