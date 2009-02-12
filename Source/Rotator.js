function EngageRotator(timerLength, pausedDelayLength, rotatorId, positionId, useAnimations, duration, fps, positionCounterId) {
    //Article Rotator
    //***************************

    //button logic:
    //previous - go to the previous article, if it was the first article go to the last, resume "slideshow", reset timer
    //pause - stop the slideshow
    //next - go to the next article, if it is the last article go to the first, resume "slideshow", reset timer

    //global variables
    this.timerLength = timerLength;
    this.pausedDelayLength = pausedDelayLength;
    this.forward = true;
    this.timerID = null;
    this.millisecondsPerSecond = 1000;
    this.isPaused = false;
    this.rotatorId = rotatorId;
    this.positionId = positionId;
    this.isPaused = false;
    this.animation = null;
    this.useAnimations = useAnimations;
    this.duration = duration;
    this.fps = fps;
    this.positionCounterId = positionCounterId;

    EngageRotator.prototype.StopTheClock = function()
    {
        clearTimeout(this.timerID);
    }

    EngageRotator.prototype.StartTheTimer = function()
    {
        if (!this.isPaused)
        {
            var self = this;
            var DisplayArticle = function()
            {
                self.DisplayArticle();
                self.StartTheTimer();
            }
            this.timerID = setTimeout(DisplayArticle, this.millisecondsPerSecond * this.timerLength);
        }
    }
    
    EngageRotator.prototype.DisplayArticle = function(newPosition)
    {
        //don't do anything if the animation is playing
        if (this.animation == null || !this.animation.get_isPlaying())
        {
            //loop over each article
            for (i = 0; i < this.articles.length; i++)
            {
                var id = this.articles[i].id;
                var currentArticle = document.getElementById(id);
                
                //find out which one is being displayed
                //don't do anything if they've clicked on the current position
                if (currentArticle.style.display == "block" && newPosition != i)
                {
                    //if there was not a position passed in, get the new position
                    if (newPosition == null)
                    {
                        newPosition = this.GetNewPosition(i);
                    }
                    var nextArticleId = this.articles[newPosition].id;
                    var nextArticle = document.getElementById(nextArticleId);
                    if (this.positions && this.positions[i])
                    {
                        var currentPositionId = this.positions[i].id;
                        var currentPosition = document.getElementById(currentPositionId);
                        
                        if (this.positions[newPosition])
                        {
                            var nextPositionId = this.positions[newPosition].id;
                            var nextPosition = document.getElementById(nextPositionId);

                            //change the position class
                            if (nextPosition)
                            {
                                nextPosition.className = "rotatorPositionSelected";
                            }
                        }
                        if (currentPosition)
                        {
                            currentPosition.className = "rotatorPosition";
                        }
                    }
                    
                    var positionCounter = document.getElementById(this.positionCounterId);
                    if (positionCounter)
                    {
                        positionCounter.innerHTML = newPosition + 1;
                    }
                    
     	            this.animation = this.switchElements(currentArticle, nextArticle);

                    break; //stop looping over articles, since we found (and changed) the currently displayed.
                }
            }
        }
    }
    
    EngageRotator.prototype.switchElements = function(elemToHide, elemToShow)
    {
        if (elemToHide && elemToShow)
        {
            if (this.useAnimations)
            {
                if (this.duration == null)
                {
                    this.duration = 0.5;
                }
                if (this.fps == null)
                {
                    this.fps = 30;
                }
            
                //position the new element on top of the current element
                var bounds = Sys.UI.DomElement.getBounds(elemToHide);
                elemToHide.style.width = bounds.width + 'px';
                bounds = EngageRotator.findPosition(elemToHide);
                elemToHide.style.left = bounds.x + 'px';
                elemToHide.style.top = bounds.y + 'px';
                elemToHide.style.position = 'absolute';
                elemToShow.style.position = '';

                elemToShow.style.display = "block";
                
                var animations = [new AjaxControlToolkit.Animation.FadeOutAnimation(elemToHide, this.duration, this.fps, 0, 1, false), new AjaxControlToolkit.Animation.FadeInAnimation(elemToShow, this.duration, this.fps, 0, 1, false)];
                
                //run the two fade animations at the same time, and then hide the current element after the animation has finished.
                var animation = new AjaxControlToolkit.Animation.ParallelAnimation(elemToShow, this.duration, this.fps, animations);
                animation.add_ended(function() 
                    {
                        elemToHide.style.display = "none";
                    });
                animation.play();
                
                return animation;
            }
            else
            {
                elemToShow.style.display = "block";
                elemToHide.style.display = "none";
            }
        }
    }
    
    EngageRotator.prototype.GetNewPosition = function(curPosition)
    {
        //if moving forward (default)
        if (this.forward)
        {
            var max = this.articles.length - 1;
            if (curPosition == max)
            {
                //set to first position
                return 0;
            }
            else
            {
                //increment position
                return curPosition + 1;
            }
        }
        //if moving backwards (previous button)
        else
        {
            if (curPosition == 0)
            {
                //set to last position
                return this.articles.length - 1;
            } 
            else
            {
                //decrement position
                return curPosition - 1;
            }
        }
    }

    EngageRotator.prototype.Previous = function()
    {
        this.isPaused = false;
        this.forward = false;
        this.DisplayArticle();
        this.StopTheClock();
        this.StartTheTimer();
        this.forward = true;
   }

    EngageRotator.prototype.Next = function()
    {
        this.isPaused = false;
        this.DisplayArticle();
        this.StopTheClock();
        this.StartTheTimer();
    }
    
    EngageRotator.prototype.Show = function(newPosition)
    {
        this.isPaused = false;
        this.DisplayArticle(newPosition);
        this.StopTheClock();
        this.StartTheTimer();
    }
    
    EngageRotator.prototype.Pause = function()
    {
        this.StopTheClock();
        this.isPaused = true;
    }
    
    EngageRotator.prototype.OnMouseOut = function()
    {
        if (!this.isPaused)
        {
            var self = this;
            var DisplayArticle = function()
            {
                self.DisplayArticle();
                self.StartTheTimer();
            }
            this.timerID = setTimeout(DisplayArticle, this.millisecondsPerSecond * this.pausedDelayLength);
        }
    }
        
    EngageRotator.getFirstLevelChildrenElements = function(id)
    {
        //from http://www.techlists.org/archives/web/javascript/2004-12/msg00022.shtml
        var elements = new Array();
        var i = 0;
        var oDiv = document.getElementById(id);
        if(oDiv && oDiv.firstChild) { // check for children
           var oChild = oDiv.firstChild;
           while(oChild) { // run over them
             if(oChild.nodeType==1) { // element
               // oChild is a first level child of oDiv
               elements[i] = oChild;
               i = i + 1;
             }
             oChild = oChild.nextSibling;
           }
        }
        return elements;
    }
    
    EngageRotator.findPosition = function (element) {
        //from http://www.quirksmode.org/js/findpos.html
	    var curleft = curtop = 0;
	    if (element.offsetParent) {
		    curleft = element.offsetLeft;
		    curtop = element.offsetTop;
		    var offsetParentPosition = EngageRotator.getStyle(element.offsetParent, "position");
		    while (element.offsetParent && (!offsetParentPosition || offsetParentPosition == "static")) {
		        element = element.offsetParent;
			    curleft += element.offsetLeft;
			    curtop += element.offsetTop;
			    offsetParentPosition = EngageRotator.getStyle(element.offsetParent, "position");
		    }
	    }
	    return { "x": curleft, "y": curtop};
    }
    
    EngageRotator.getStyle = function (oElm, strCssRule) {
        //from http://www.robertnyman.com/2006/04/24/get-the-rendered-style-of-an-element/
        if (oElm)
        {
	        var strValue = "";
	        if(document.defaultView && document.defaultView.getComputedStyle){
		        strValue = document.defaultView.getComputedStyle(oElm, "").getPropertyValue(strCssRule);
	        }
	        else if(oElm.currentStyle){
		        strCssRule = strCssRule.replace(/\-(\w)/g, function (strMatch, p1){
			        return p1.toUpperCase();
		        });
		        strValue = oElm.currentStyle[strCssRule];
	        }
	        return strValue;
	    }
    }
    
    //this should only be called on page load
    //load up each article div inside the article wrapper div
    this.articles = EngageRotator.getFirstLevelChildrenElements(this.rotatorId);
    this.positions = EngageRotator.getFirstLevelChildrenElements(this.positionId);

    //highlight first position if we're showing positions
    if (this.positions && this.positions[0])
    {
        var firstPositionId = this.positions[0].id;
        var firstPosition = document.getElementById(firstPositionId);
        if (firstPosition)
        {
            firstPosition.className = "rotatorPositionSelected";
        }
    }
    this.StartTheTimer();
}