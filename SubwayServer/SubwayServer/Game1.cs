//Author: Victoria Mak
//File Name: Game1.cs
//Project Name: SubwayServer
//Creation Date: May 19, 2022
//Modified Date: June 21, 2022
//Description: Play the game, Subway Server, where you are a Sandwich Artist in a Subway shop and have to serve customers their sandwiches. Arrays are used to store the ingredient images, rectangles, locations. Methods are used for commonly
//             used code, such as translating sauce, grabbing and dropping ingredients, and calculating scores. Loops are used for large quantity values such as translating each ingredient piece like lettuce shreds or sauce drops. I also used
//             arrays to store the hovering/non-hovering status of buttons.

using Animation2D;
using Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SubwayServer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Store the random number generator
        Random rng = new Random();

        //Store the number of milliseconds in 1 second
        const int MILLISEC_PER_SEC = 1000;

        //Store the UI layout
        const int BUTTON_SPACER = 20;
        const int SMALL_HORIZ_SPACER = 8;
        const int SUB_BOTTOM_VERT_SPACER = 20;
        const int VERT_SPACER = 15;
        const int HORIZ_SCORE_SPACER = 100;
        const int CUSTOMER_LINE_START = 240;
        const int NOTEPAD_TOP_SPACER = 20;
        const int NOTEPAD_SECTION_SPACER = 10;
        const int NOTEPAD_LEFT_SPACER = 10;
        const int HOVER_DISPLACEMENT = 5;
        const int TOP_SCREEN_SPACE = 80;
        const int SAUCE_CONT_HORIZ_EDGE_SPACER = 15;
        const int SAUCE_CONT_VERT_EDGE_SPACER = 10;
        const int FIRST_LAYER_VERT_SPACER = 25;
        const int CHEESE_VERT_SPACING = 20;
        const int TOASTER_WINDOW_SPACER = 55;
        const int TOASTER_BAR_X_SPACER = 340;
        const int TOASTER_BAR_Y_SPACER = 27;
        const int ORDER_NUM_BTN_SPACER = 5;
        const int ORDER_SELECTION_TOP_SPACER = 80;

        //Store the shop spacing
        const int SHOP_HORIZ_SPACER = 58;
        const int SHOP_VERT_SPACER = 46;

        //Store the number of different lettuce rotation positions
        const int NUM_DIFF_LETTUCE_ROTATIONS = 100;

        //Store the passing score
        const double PASS = 0.6;

        //Store the acceleration due to gravity and from a push in pixels per second squared
        const double GRAVITY = 60;
        const double AIR_RESISTANCE = 30;
        const double PUSH = 30;

        //Store the hovering state
        const int NOT_HOVERING = 0;
        const int HOVERING = 1;

        //Store the locked state for the purchase button
        const int LOCKED = 2;

        //Store right and left
        const int LEFT = 0;
        const int RIGHT = 1;

        //Store the game states
        const int MENU = 0;
        const int PLAY = 1;
        const int PREGAME = 2;
        const int PRE_PREGAME = 3;
        const int INSTRUCTIONS = 4;
        const int PRE_ENDGAME = 5;
        const int ENDGAME = 6;
        const int SHOP = 7;

        //Store the play states
        const int STORE_FRONT = 0;
        const int UNCOOKED_COUNTER = 1;
        const int TOASTER_STATION = 2;
        const int COOKED_COUNTER = 3;
        const int CUST_TABLE = 4;
        const int TAKING_ORDER = 5;
        const int CHECKING_ORDER = 6;

        //Store the customer states
        const int NOT_ORDERED = 0;
        const int WALKING_IN = 1;
        const int WALKING_TO_TABLE = 2;
        const int WAITING_FOR_ORDER = 3;
        const int MOVING_UP = 4;

        //Store the sub steps
        const int UNSTARTED = 0;
        const int BREAD_CHOSEN = 1;
        const int CUTTING_BREAD = 2;
        const int CHOOSING_MEAT = 3;
        const int ADDING_MEAT = 4;
        const int CHOOSING_CHEESE = 5;
        const int ADDING_CHEESE = 6;
        const int SLIDING_1 = 7;
        const int IN_LINE_TOASTER = 8;
        const int ENTERED_TOASTER = 9;
        const int IN_LINE_COOKED = 10;
        const int CHOOSING_VEGGIES = 11;
        const int ADDING_VEGGIES = 12;
        const int CHOOSING_SAUCE = 13;
        const int ADDING_SAUCE = 14;
        const int ADDING_TOP_BUN = 15;
        const int SELECT_ORDER_NUM = 16;
        const int SLIDING_2 = 17;

        //Store the max and min number of customers in a round
        const int MAX_CUSTOMERS = 8;
        const int MIN_CUSTOMERS = 3;

        //Store the maximum and minimum customer entrance delay time in milliseconds
        const float MIN_CUST_ENTRANCE_DELAY = 25000f;
        const float MAX_CUST_ENTRANCE_DELAY = 45000f;

        //Store the types of bread
        const int ITALIAN = 0;
        const int HERBS_CHEESE = 1;
        const int WHOLE_WHEAT = 2;
        const int NUM_BREAD_CHOICES = 3;

        //Store the bread image states
        const int BOT_BUN = 0;
        const int FULL_LOAF = 1;
        const int TOP_BUN = 2;

        //Store the meat types
        const int CHICKEN = 0;
        const int HAM = 1;
        const int SALAMI = 2;
        const int NUM_MEAT_CHOICES = 3;

        //Store the cheese types
        const int CHEDDAR = 0;
        const int OLD_ENGLISH = 1;
        const int SWISS = 2;
        const int NUM_CHEESE_CHOICES = 3;

        //Store the veggie types
        const int LETTUCE = 0;
        const int TOMATO = 1;
        const int OLIVE = 2;
        const int PICKLE = 3;
        const int CUCUMBER = 4;
        const int NUM_VEGGIE_CHOICES = 5;

        //Store the sauce types
        const int CHIPOTLE = 0;
        const int HONEY_MUSTARD = 1;
        const int BBQ = 2;
        const int RANCH = 3;
        const int SWEET_CHILI = 4;
        const int NUM_SAUCE_CHOICES = 5;

        //Store the number of columns of bottles the sauce container can hold
        const int NUM_SAUCE_COL = 2;

        //Store the top and bottom toasters
        const int TOP_TOASTER = 0;
        const int BOT_TOASTER = 1;
        const int NUM_TOASTERS = 2;

        //Store the bar empty and full
        const int EMPTY_BAR = 0;
        const int FULL_BAR = 1;

        //Store the maximum different toppings and sauces a customer can get
        const int MAX_VEGGIES = 2;
        const int MAX_SAUCES = 2;

        //Store the number of bread zones for evaluation
        const int BREAD_ZONES = 12;
        
        //Store the knife images states
        const int UNROTATED = 0;
        const int ROTATED = 1;
        const int KNIFE_CUTTING = 2;

        //Store the 4 scores index number
        const int WAIT_SCORE = 0;
        const int CUT_SCORE = 1;
        const int TOAST_SCORE = 2;
        const int TOPPING_SCORE = 3;
        const int NUM_SCORES = 4;

        //Store the ingredient choice weighting, the correct of ingredients
        const double PLACEMENT_WEIGHTING = 0.5;
        const double INGR_CHOICE_WEIGHTING = 0.5;

        //Store the max point score for 1 topping of each and the max additional points per additional option, bonus wait points per second remaining, and tips multiplier
        const int MAX_BASE_SCORE = 150;
        const int ADDITIONAL_TOPPING_PTS = 30;
        const int BONUS_WAIT_POINTS = 1;
        const double TIPS_MULTIPLIER = 1.25;

        //Store the shop items
        const int DOORBELL = 0;
        const int TIMER_TOP = 1;
        const int TIMER_BOT = 2;
        const int POSTER_1 = 3;
        const int POSTER_2 = 4;
        const int RADIO = 5;

        //Store the toaster window transparency
        const float TOASTER_WINDOW_TRANSPARENCY = 0.8f;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Store the subway main background color, toaster background color, and taking order background color
        Color subwayBgColor = Color.LightGreen;
        Color toasterBgColor = Color.LightGray;
        Color takingOrderBgColor = Color.White;

        //Store the current game state and current and previous play state
        int gameState = MENU;
        int prevPlayState = STORE_FRONT;
        int playState = STORE_FRONT;

        //Store the current day number and the total number of customers in the current round
        int day = 0;
        int totalNumCust = MIN_CUSTOMERS;

        //Store the screen width and height
        int screenWidth;
        int screenHeight;

        //Store the current and previous mouse state
        MouseState mouse;
        MouseState prevMouse;
        
        //Store the current and previous keyboard state
        KeyboardState kb;
        KeyboardState prevKb;

        //Store the fonts
        SpriteFont titleFont;
        SpriteFont writingFont;
        SpriteFont remainPiecesFont;
        SpriteFont scoreFont;
        SpriteFont descFont;
        
        //Store the sound effects
        SoundEffect selectSnd;
        SoundEffect plopSnd;
        SoundEffect purchaseSnd;
        SoundEffect openingJingle;
        SoundEffect closingJingle;
        SoundEffect doorbellSnd;
        SoundEffect timerAlarmSnd;
        SoundEffect ovenStartSnd;
        SoundEffect sauceSqzSnd;
        SoundEffect lettuceSnd;
        SoundEffect successSnd;
        SoundEffect failSnd;
        SoundEffect writingSnd;
        SoundEffect paperFlipSnd;
        SoundEffect stepSnd;
        SoundEffect slideSnd;
        SoundEffect btnClickSnd;

        //Store the sound effect instances
        SoundEffectInstance lettuceOrSauceSnd;
        SoundEffectInstance writing;
        SoundEffectInstance[] timerAlarms = new SoundEffectInstance[NUM_TOASTERS];

        //Store the background music
        Song menuMusic;
        Song stereoMusic;
        Song endGameMusic;
        
        //Store the button images
        Texture2D playBtn;
        Texture2D menuBtn;
        Texture2D shopBtn;
        Texture2D startBtn;
        Texture2D instBtn;
        Texture2D[] purchaseBtns = new Texture2D[3];
        Texture2D takeOrderBtn;
        Texture2D[] garbageBtn = new Texture2D[2];
        Texture2D finishBtn;

        //Store the instruction slide images
        Texture2D[] instrSlides = new Texture2D[9];

        //Store the customer images
        Texture2D[] customerImgs = new Texture2D[MAX_CUSTOMERS];

        //Store the background objects in the storefront
        Texture2D storeBgImg;
        Texture2D frontCounterImg;
        Texture2D subwayLogoImg;
        Texture2D[] screenImgs = new Texture2D[2];
        Texture2D roofImg;

        //Store the background images
        Texture2D shopBg;
        Texture2D subwayWrapperBg;
        Texture2D closedSignImg;
        Texture2D openSignImg;
        Texture2D notepadImg;

        //Store the shop item images
        Texture2D[] itemImgs = new Texture2D[6];

        //Store the bread images
        Texture2D[][] breadImgs = new Texture2D[NUM_BREAD_CHOICES][];

        //Store the knife image and the guiding line image
        Texture2D[] knifeImgs = new Texture2D[3];
        Texture2D guidingLineImg;

        //Store the meat images
        Texture2D[] meatContImgs = new Texture2D[NUM_MEAT_CHOICES];
        Texture2D[] meatImgs = new Texture2D[NUM_MEAT_CHOICES];

        //Store the cheese images
        Texture2D[] cheeseContImgs = new Texture2D[NUM_CHEESE_CHOICES];
        Texture2D[] cheeseImgs = new Texture2D[NUM_CHEESE_CHOICES];

        //Store the toaster image and the toasting bar images
        Texture2D toasterImg;
        Texture2D[] toastingBarImg = new Texture2D[2];
        Texture2D perfectToastLineImg;
        Texture2D rightArrowImg;

        //Store the sprinkling bar images
        Texture2D[] sprinklingBarImgs = new Texture2D[2];
        Texture2D perfectAmntLineImg;

        //Store the veggie images
        Texture2D[] veggieImgs = new Texture2D[NUM_VEGGIE_CHOICES];
        Texture2D[] veggieContImgs = new Texture2D[NUM_VEGGIE_CHOICES];

        //Store the scooper image
        Texture2D scooperImg;

        //Store the sauce images
        Texture2D[] sauceBottleImgs = new Texture2D[NUM_SAUCE_CHOICES];
        Texture2D[] sauceTopImgs = new Texture2D[NUM_SAUCE_CHOICES];
        Texture2D[] sauceDropImgs = new Texture2D[NUM_SAUCE_CHOICES];
        Texture2D sauceContImg;

        //Store the order selection pop up images
        Texture2D orderSelectionImg;
        Texture2D[] orderNumBtns = new Texture2D[MAX_CUSTOMERS];

        //Store the score scale images
        Texture2D[] scoreScaleImgs = new Texture2D[4];
        Texture2D pointerImg;

        //Store the right and left arrow images and the check orders and back tabs
        Texture2D[] controlArrows = new Texture2D[2];
        Texture2D checkOrdersTab;
        Texture2D backTab;

        //Store the title's second word
        string title2ndWord = "Server";

        //Store the day text
        string dayNumText = "Day 1";

        //Store the ordering labels and order number
        string breadNoteLabel = "Bread:";
        string meatNoteLabel = "Protein:";
        string cheeseNoteLabel = "Cheese:";
        string veggieNoteLabel = "Topping(s):";
        string sauceNoteLabel = "Sauce(s):";
        string[] orderNumNoteLabel = new string[] { "#1", "#2", "#3", "#4", "#5", "#6", "#7", "#8" };

        //Store the remaining pieces texts
        string uncookedRemainPiecesText = "";
        string cookedRemainPiecesText = "";

        //Store the score texts
        string[] scoreTexts = new string[] { " ", " ", " ", " " };

        //Store the points and money text
        string pointsText = " ";
        string tipsText = " ";
        string totalPtsText = "Total XP: " + 0;
        string totalTipsText = "Bank: $0.00";

        //Store the total average text
        string finalAvgScoreText = "Average: ";

        //Store the names and descriptions of the store items
        string[] itemNames = new string[] { "Doorbell",
                                            "Top Toaster Timer",
                                            "Bottom Toaster Timer",
                                            "Poster",
                                            "Poster",
                                             "Radio" };
        string[] itemDescs = new string[] { "Rings when customer enters",
                                            "Rings at the perfect toastiness",
                                            "Rings at the perfect toastiness",
                                            "Increases customer patience x 1.4",
                                            "Increases customer patience x 1.2",
                                            "Plays music in the background" };

        //Store the price text
        string priceText = "$2.65";

        //Store the prices of the items in the shop
        double[] itemPrices = new double[] { 3.99, 3.99, 5.99, 7.50, 4.99 , 2.99 };
        
        //Store the customer walking animations
        Animation[] customers = new Animation[MAX_CUSTOMERS];

        //Store the sign flip animation
        Animation closedSignAnim;
        Animation openSignAnim;

        //Store the button rectangles
        Rectangle[] playBtnRecs = new Rectangle[2];
        Rectangle[] menuBtnRecs = new Rectangle[2];
        Rectangle[] startBtnRecs = new Rectangle[2];
        Rectangle[] shopBtnRecs = new Rectangle[2];
        Rectangle[] instBtnRecs = new Rectangle[2];
        Rectangle purchaseBtnRec;
        Rectangle[] takeOrderBtnRecs = new Rectangle[2];
        Rectangle uncookedGarbageBtnRec;
        Rectangle cookedGarbageBtnRec;
        Rectangle[] finishBtnRecs = new Rectangle[2];

        //Store the background rectangle
        Rectangle bgRec;

        //Store the instruction rectangles 
        Rectangle[] instrSlideRecs = new Rectangle[9];

        //Store the rectangles for the store front background objects
        Rectangle frontCounterRec;
        Rectangle subwayLogoRec;
        Rectangle[] screenRecs = new Rectangle[3];
        Rectangle roofRec;

        //Store the poster rectangles for the store front
        Rectangle poster1Rec;
        Rectangle poster2Rec;
        
        //Store the rectangle for the notepad background
        Rectangle notepadBgRec;

        //Store the upgrade rectangles
        Rectangle[] itemRecs = new Rectangle[6];
        Rectangle itemViewingRec;

        //Store the food rectangles for the notepad record
        Rectangle breadRecordRec;
        List<Rectangle> meatRecordRecs;
        Rectangle cheeseRecordRec;
        List<Rectangle[]> veggieRecordRecs;
        List<Rectangle[]> sauceRecordRecs;

        //Store the rectangle for the 3 bread options
        Rectangle[][] breadOptionRecs = new Rectangle[NUM_BREAD_CHOICES][];

        //Store the knife rectangle
        Rectangle[] unrotatedKnifeRecs = new Rectangle[2];
        Rectangle[] rotatedKnifeRecs = new Rectangle[2];

        //Store the guiding line rectangle
        Rectangle guidingLineRec;

        //Store the meat, cheese, veggies, and sauce container rectangles
        Rectangle[] meatContRecs = new Rectangle[NUM_MEAT_CHOICES];
        Rectangle[] cheeseContRecs = new Rectangle[NUM_CHEESE_CHOICES];
        Rectangle[] veggieContRecs = new Rectangle[NUM_VEGGIE_CHOICES];

        //Store the sauce storage and sauce bottle top rectangles
        Rectangle sauceContRec;
        Rectangle[] sauceBottleTopsRecs = new Rectangle[NUM_SAUCE_CHOICES];
        Rectangle sauceBottleSideRec;

        //Store the bread positioning rectangles in the cooked and uncooked stations and the positions for the breads at the toaster station
        Rectangle[] buildingBreadRecs = new Rectangle[3];        
        Rectangle breadInLineToasterRec;
        Rectangle[] breadToastingRec = new Rectangle[2];
        Rectangle breadAwayLineToasterRec;

        //Store the toaster rectangles and the toasting bar rectangles
        Rectangle[] toasterRecs = new Rectangle[NUM_TOASTERS];
        Rectangle[][] toastingBarRecs = new Rectangle[NUM_TOASTERS][];
        Rectangle[] perfToastLineRecs = new Rectangle[NUM_TOASTERS];

        //Store the right arrow rectangle at the toasting station
        Rectangle rightArrowRec;

        //Store the sprinkling bar rectangles and its line
        Rectangle[] sprinklingBarRecs = new Rectangle[2];
        Rectangle sprinklingBarLineRec;

        //Store the used ingredient rectangles
        List<Rectangle[]> subBreadRecs = new List<Rectangle[]>();
        List<List<Rectangle>> meatPiecesRecs = new List<List<Rectangle>>();
        List<List<Rectangle>> cheesePiecesRecs = new List<List<Rectangle>>();
        List<List<Rectangle>> veggiePiecesRecs = new List<List<Rectangle>>();
        List<List<Rectangle>> sauceDropRecs = new List<List<Rectangle>>();

        //Store the lettuce scooper rectangle
        Rectangle scooperRec;

        //Store the current ingredient piece and the true location of it when it is falling at the cooked and uncooked station
        Rectangle curUncookedIngrPieceRec;
        Vector2 curUncookedIngrPieceLoc;
        Rectangle curCookedIngrPieceRec;
        Vector2 curCookedIngrPieceLoc;

        //Store the rectangle for the order number selection
        Rectangle orderSelectionRec;
        Rectangle[][] orderNumBtnRecs = new Rectangle[MAX_CUSTOMERS][];

        //Store the rectangle for the score images
        Rectangle[] scoreScaleRecs = new Rectangle[4];
        Rectangle[] scorePointerRecs = new Rectangle[4];

        //Store 2 different arrow locations for the different stations and the tab location
        Rectangle[] controlArrowRecs1 = new Rectangle[2];
        Rectangle[] controlArrowRecs2 = new Rectangle[2];
        Rectangle tabRec;

        //Store the pointer rotation and the intended pointer rotation
        float[] pointerRotations = new float[4];
        float[] intendedPtrRotations = new float[4];
        
        //Store the horizontal true location of the currently sliding bread at the stations
        float uncookedBreadTrueLocX;
        float cookedBreadTrueLocX;
        List<float> toastedBreadTrueLocsX = new List<float>();

        //Store the location of the title's second word
        Vector2 title2ndWordLoc;

        //Store the unmoving description box rectangle in the shop 
        Rectangle descRec = new Rectangle(670, 90, 276, 347);

        //Store the store item title, description, and price location
        Vector2 itemTitleLoc;
        Vector2 itemDescLoc;
        Vector2 priceLoc;
        
        //Store the day number location in the pregame and in the other states
        Vector2 dayNumLoc;
        Vector2 preGameDayNumLoc;

        //Store the customer locations
        Vector2[] custLocs = new Vector2[MAX_CUSTOMERS];

        //Store the location of the notepad labels for taking/checking orders
        Vector2 breadNoteLoc;
        Vector2 meatNoteLoc;
        Vector2 cheeseNoteLoc;
        Vector2 veggieNoteLoc;
        Vector2 sauceNoteLoc;
        Vector2 orderNumNoteLoc;

        //Store the location of the remaining pieces texts
        Vector2 uncookedRemainPiecesLoc;
        Vector2 cookedRemainPiecesLoc;

        //Store the location of the score texts
        Vector2[] scoreTextLocs = new Vector2[NUM_SCORES];

        //Store the location of the sign flip background 
        Vector2 signFlipLoc;

        //Store the spacing above from the previous layer for each veggie on the sub
        int[] veggieSpacers = new int[] { 0, 5, 20, 10, 10 };
        
        //Store the current next layer height spacing for the veggies
        int nextLayerSpacing;

        //Store the distance between the ingredients and the top left corner of the bread
        List<Vector2[]> meatsBreadDists = new List<Vector2[]>();
        List<Vector2[]> cheesesBreadDists = new List<Vector2[]>();
        List<Vector2[]> veggieBreadDists = new List<Vector2[]>();
        List<Vector2[]> sauceBreadDists = new List<Vector2[]>();

        //Store the fixed pointer location in the scales and the pointer origin
        Vector2 pointerLoc = new Vector2(101, 140);
        Vector2 pointerOrigin;

        //Store the points, tips, and average score locations
        Vector2 finalAvgScoreLoc;
        Vector2 ptsLoc;
        Vector2 tipsLoc;
        Vector2 totalPtsLoc;
        Vector2 totalTipsLoc;

        //Store the balance location in the shop
        Vector2 balanceLoc;
        
        //Store the distance between the customer's feet to the bottom of the screen
        int custFootFloorDist = 35;

        //Store the bread option scalers
        float breadOptionScaler = 0.4f;

        //Store the scaler for the sub when it is at the toasting station
        float subScalerAtToaster = 0.3f;

        //Store the height of the ingredient images shown in the notepad
        int picturesNoteHeight = 60;

        //Store the current speed of the falling ingredients
        float uncookedIngrFallSpeed = 0f;
        float cookedIngrFallSpeed = 0f;

        //Store the speed of the subs moving right from the uncooked station and at the toaster station
        float subSlideSpeed1 = 0f;
        float subSlideSpeed2 = 0f;
        List<float> smallSubSlideSpeed = new List<float>();

        //Store the pointer max and min speed in radians per second
        float maxPtrSpeed = 4f;
        float minPtrSpeed = 0.8f;

        //Store the fading max speed per second
        float fadeMaxSpeed = 0.4f;

        //Store the transparency of the scores and the flip sign background
        float[] scoreTransparencies = new float[] { 0, 0, 0, 0 };
        float flipSignTransparency = 1f;

        //Store the stereo volume
        float stereoVol = 0.5f;
        
        //Store whether the mouse is hovering over the game buttons
        bool hoverOverPlayBtn = false;
        bool hoverOverMenuBtn = false;
        bool hoverOverInstBtn = false;
        bool hoverOverShopBtn = false;
        bool hoverOverStartBtn = false;
        bool hoverOverTakeOrder = false;
        bool[] hoverOverBreads = new bool[] { false, false, false };
        bool hoverOverKnife = false;
        bool hoveringOverTrash = false;
        bool hoverOverFinish = false;
        bool[] hoverOverOrderNums = new bool[] { false, false, false, false, false, false, false, false };
        bool hoverOverPurchase = false;

        //Store whether each item has shown up on the notepad in the taking orders
        bool breadShown = false;
        bool meatShown = false;
        bool cheeseShown = false;
        bool veggiesShown = false;
        bool sauceShown = false;

        //Store whether the mouse is cutting the bread
        bool isCutting = false;

        //Store whether the mouse is grabbing an ingredient and whether the ingredient is falling at each station
        bool grabbingUncookedIngr = false;
        bool uncookedIngrFalling = false;
        bool grabbingCookedIngr = false;
        bool cookedIngrFalling = false;

        //Store whether the mouse is grabbing the sub to put in the toaster
        bool grabbingSub = false;

        //Store whether the mouse is squeezing the sauce bottle
        bool sprinklingOrSqueezing = false;

        //Store the bread number in each toaster
        int[] toastersOccupancy = new int[] { -1, -1 };

        //Store whether each sauce drop/lettuce shred is falling, the true vertical location of each drop/shred, the stopping location of each drop/shred, each lettuce shred's speed, the max sauce fall speed in pixels per second, and the sauce drop speed of the sauce in the update
        List<bool> lettuceOrDropFalling = new List<bool>();
        List<float> lettuceOrDropTrueLocs = new List<float>();
        List<int> lettuceOrSauceFallLocs = new List<int>();
        List<float> lettuceSpeeds = new List<float>();
        float maxDropSpeed = 130;

        //Store the number of lettuce shreds and sauce drops falling per second and the fall buffer time
        int lettucePerSec = 5;
        int dropsPerSec = 30;
        double fallBufferTime = 0;

        //Store each veggie rotation value only for the lettuce and the lettuce origin
        List<List<float>> veggieRotations = new List<List<float>>();
        Vector2 lettuceOrigin;

        //Store the number of wasted lettuce and sauce drops
        int wastedPieces = 0;

        //Store the currently grabbed sub number at the toaster
        int grabbedSubNum;

        //Store the timer for the customer wait and for the buffer time between customer entrance
        Timer[] custWaitTimers;
        Timer custEntranceTimer;

        //Store the taking order timer for the timing between each igredient popping up in the notepad
        Timer takingOrderTimer;

        //Store the bread toasting timer
        Timer[] toastingTimer = new Timer[NUM_TOASTERS];

        //Store the evaluation timer in the customer's table state
        Timer custTableTimer;

        //Store the current instruction slide view
        int curInstrSlideView = 0;

        //Store the current item in the store that is being viewed and the purchase status of each
        int curItemView = 0;
        bool[] itemIsPurchased = new bool[] { false, false, false, false, false, false };

        //Store the states for each customer
        int[] custStates = new int[] { WALKING_TO_TABLE, WALKING_TO_TABLE, WALKING_TO_TABLE, WALKING_TO_TABLE, WALKING_TO_TABLE, WALKING_TO_TABLE, WALKING_TO_TABLE, WALKING_TO_TABLE };
        
        //Store the number of customers appeared
        int numCustAppeared = 0;

        //Store the customer numbers that will appear in order, the list of customers in the store and in the line
        int[] custAppearances;
        List<int> custInStore = new List<int>();
        List<int> custInLine = new List<int>();

        //Store the constant customer stopping locations
        int[] lineStoppingLocs = new int[] { CUSTOMER_LINE_START, CUSTOMER_LINE_START + 200, CUSTOMER_LINE_START + 400 };

        //Store the customer walk speed the currently walking customer's speed
        float maxCustWalkSpeed = 200f;
        float custSpeedX;
        
        //Store the customer orders        
        List<int> breadOrders = new List<int>();
        List<int> meatOrders = new List<int>();
        List<int> cheeseOrders = new List<int>();
        List<int[]> veggieOrders = new List<int[]>();
        List<int[]> sauceOrders = new List<int[]>();

        //Store the next order number
        int nextOrderNum;

        //Store the current checking order number, the uncompleted available orders, and the number of orders completed
        int curOrderView;
        List<int> availOrders = new List<int>();
        int ordersCompleted = 0;

        //Store the current ingredient chosen at the cooked and uncooked station to be added
        int curBreadChosen;
        int curMeatChosen;
        int curCheeseChosen;
        int curVeggieChosen;
        int curSauceChosen;

        //Store the current sub index at the uncooked and cooked
        int subIndexAtUncooked;
        int subIndexAtCooked;

        //Store the total number of subs started
        int subsStarted = 0;

        //Store the sub numbers, current sub states, the breads chosen, the meats chosen, cheesse chosen used for each sub
        List<int> subNums = new List<int>();
        List<int> subStates = new List<int>();
        List<int> subBreads = new List<int>();
        List<int> subMeats = new List<int>();
        List<int> subCheeses = new List<int>();

        //Store the list of each sub's cutting cutting score and toasting score
        List<double> toastScores = new List<double>();
        List<double> cutScores = new List<double>();

        //Store the veggies chosen, and sauces chosen only for the current sub at the cooked station
        List<int> subVeggies = new List<int>();
        List<int> subSauces = new List<int>();

        //Store the sub numbers in line at the toasting station and sliding away in line from the toasting station
        List<int> subsInLineAtToaster = new List<int>();
        List<int> subsAwayLineToaster = new List<int>();

        //Store the list of subs at each station and whether there is a sub at the uncooked station
        bool subAtUncooked = false;
        List<int> subsAtCooked = new List<int>();

        //Store the perfect cutting location of the bread as the number of pixels from the top of the full bread image
        int perfectCuttingLoc = 88;

        //Store the knife location relative to the perfect cutting location and the cutting scores at the bread checkpoints, the cutting progress, and the current bread zone for the cut scoring
        int knifeDistFromPerfect;
        double[] cuttingZoneScores = new double[BREAD_ZONES];
        int cuttingProgress = 0;
        int curBreadZone = 1;

        //Store the maximum number of pieces for each type of ingredient
        int[] maxMeatPieces = new int[NUM_MEAT_CHOICES];
        int[] maxCheesePieces = new int[NUM_CHEESE_CHOICES];
        int[] maxVeggiePieces = new int[NUM_VEGGIE_CHOICES];
        int maxSauceDrops = 400;

        //Store the perfect amount of sauce drops/lettuce shreds per sub zone
        int perfectSauceAmnt = 20;
        int perfectLettuceAmnt = 4;

        //Store the meat and cheese score
        double meatScore;
        double cheeseScore;

        //Store the sauce and vegetable placement, correctness, and final sauce/veggie score
        double[] veggiePlacementScores;
        double veggieCorrectnessScore;
        double finalVeggieScore;
        double[] saucePlacementScores;
        double sauceCorrectnessScore;
        double finalSauceScore;

        //Store all the sub scores, the average scores, and the final average score
        double[][] orderScores;
        double[] avgScores = new double[NUM_SCORES];
        double finalAvgScore;

        //Store the current average sub score, wait bonus points earned, and the maximum points that can be earned from the current sub order
        double curAvgSubScore;
        int bonusWaitPts = 0;
        int orderMaxPts = 0;

        //Store the points and tips earned from the current order, tips and points from the round, and the total points and tips
        int curPoints = 0;
        double curTips = 0;
        int dayPoints = 0;
        double dayTips = 0;
        int totalPts = 0;
        double totalTips = 0;

        //Store the maximum customer patience time in milliseconds
        double maxCustPatience = 200000;
        
        //Store the poster multipliers for the customer patience
        double poster1Multiplier = 1.2;
        double poster2Multiplier = 1.4;

        //Store the perfect toasting time in milliseconds
        float perfectToastTime = 20000f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Set the game screen size
            graphics.PreferredBackBufferWidth = 980;
            graphics.PreferredBackBufferHeight = 550;

            //Set the mouse as visible
            IsMouseVisible = true;

            //Apply the changes to the game graphics
            graphics.ApplyChanges();

            //Set the screen width and height
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Load the fonts
            titleFont = Content.Load<SpriteFont>("Fonts/TitleFont");
            writingFont = Content.Load<SpriteFont>("Fonts/WritingFont");
            remainPiecesFont = Content.Load<SpriteFont>("Fonts/RemainingPiecesFont");
            scoreFont = Content.Load<SpriteFont>("Fonts/ScoreFont");
            descFont = Content.Load<SpriteFont>("Fonts/DescFont");

            //Load the background music
            menuMusic = Content.Load<Song>("Audio/Music/BgMusic");
            stereoMusic = Content.Load<Song>("Audio/Music/PlayBgMusic");
            endGameMusic = Content.Load<Song>("Audio/Music/EndGameBgMusic");

            //Load the sound effects
            selectSnd = Content.Load<SoundEffect>("Audio/Sounds/Select");
            plopSnd = Content.Load<SoundEffect>("Audio/Sounds/PlopSound");
            purchaseSnd = Content.Load<SoundEffect>("Audio/Sounds/Purchase");
            openingJingle = Content.Load<SoundEffect>("Audio/Sounds/OpeningTone");
            closingJingle = Content.Load<SoundEffect>("Audio/Sounds/ClosingTone");
            doorbellSnd = Content.Load<SoundEffect>("Audio/Sounds/DoorbellRing");
            timerAlarmSnd = Content.Load<SoundEffect>("Audio/Sounds/AlarmRing");
            ovenStartSnd = Content.Load<SoundEffect>("Audio/Sounds/DoorClose");
            sauceSqzSnd = Content.Load<SoundEffect>("Audio/Sounds/SauceSqueezeSound");
            lettuceSnd = Content.Load<SoundEffect>("Audio/Sounds/LettuceDropSound");
            successSnd = Content.Load<SoundEffect>("Audio/Sounds/Success");
            failSnd = Content.Load<SoundEffect>("Audio/Sounds/Fail");
            writingSnd = Content.Load<SoundEffect>("Audio/Sounds/PencilWritingSound");
            paperFlipSnd = Content.Load<SoundEffect>("Audio/Sounds/PaperFlipSound");
            stepSnd = Content.Load<SoundEffect>("Audio/Sounds/StepSnd");
            slideSnd = Content.Load<SoundEffect>("Audio/Sounds/SlideSnd");
            btnClickSnd = Content.Load<SoundEffect>("Audio/Sounds/BtnSelectSnd");

            //Load the button images
            playBtn = Content.Load<Texture2D>("Images/Sprites/PlayBtn");
            menuBtn = Content.Load<Texture2D>("Images/Sprites/MenuBtn");
            shopBtn = Content.Load<Texture2D>("Images/Sprites/ShopBtn");
            startBtn = Content.Load<Texture2D>("Images/Sprites/StartBtn");
            instBtn = Content.Load<Texture2D>("Images/Sprites/InstBtn");
            takeOrderBtn = Content.Load<Texture2D>("Images/Sprites/TakeOrderBtn");
            garbageBtn[NOT_HOVERING] = Content.Load<Texture2D>("Images/Sprites/GarbageBtn");
            garbageBtn[HOVERING] = Content.Load<Texture2D>("Images/Sprites/GarbageHoverBtn");
            finishBtn = Content.Load<Texture2D>("Images/Sprites/FinishBtn");
            purchaseBtns[NOT_HOVERING] = Content.Load<Texture2D>("Images/Sprites/PurchaseBtn");
            purchaseBtns[HOVERING] = Content.Load<Texture2D>("Images/Sprites/PurchaseHoverBtn");
            purchaseBtns[LOCKED] = Content.Load<Texture2D>("Images/Sprites/PurchaseBtnLocked");

            //Load the background images for the store front
            storeBgImg = Content.Load<Texture2D>("Images/Backgrounds/Room");
            frontCounterImg = Content.Load<Texture2D>("Images/Backgrounds/FrontCounter");
            subwayLogoImg = Content.Load<Texture2D>("Images/Backgrounds/SubwayLogo");
            screenImgs[0] = Content.Load<Texture2D>("Images/Backgrounds/Screen");
            screenImgs[1] = Content.Load<Texture2D>("Images/Backgrounds/Screen2");
            roofImg = Content.Load<Texture2D>("Images/Backgrounds/StoreRoof");

            //Load the instruction slides
            instrSlides[0] = Content.Load<Texture2D>("Images/Backgrounds/Instr1");
            instrSlides[1] = Content.Load<Texture2D>("Images/Backgrounds/Instr2");
            instrSlides[2] = Content.Load<Texture2D>("Images/Backgrounds/Instr3");
            instrSlides[3] = Content.Load<Texture2D>("Images/Backgrounds/Instr4");
            instrSlides[4] = Content.Load<Texture2D>("Images/Backgrounds/Instr5");
            instrSlides[5] = Content.Load<Texture2D>("Images/Backgrounds/Instr6");
            instrSlides[6] = Content.Load<Texture2D>("Images/Backgrounds/Instr7");
            instrSlides[7] = Content.Load<Texture2D>("Images/Backgrounds/Instr8");
            instrSlides[8] = Content.Load<Texture2D>("Images/Backgrounds/Instr9");

            //Load the right and left arrow and the tabs
            controlArrows[RIGHT] = Content.Load<Texture2D>("Images/Sprites/ArrowD");
            controlArrows[LEFT] = Content.Load<Texture2D>("Images/Sprites/ArrowA");
            checkOrdersTab = Content.Load<Texture2D>("Images/Sprites/CheckOrdersTab");
            backTab = Content.Load<Texture2D>("Images/Sprites/BackTab");

            //Load the background images
            shopBg = Content.Load<Texture2D>("Images/Backgrounds/ShopBg");
            subwayWrapperBg = Content.Load<Texture2D>("Images/Backgrounds/WrapperBg");
            closedSignImg = Content.Load<Texture2D>("Images/Backgrounds/ezgif.com-gif-maker");
            openSignImg = Content.Load<Texture2D>("Images/Backgrounds/CloseToOpen");
            notepadImg = Content.Load<Texture2D>("Images/Backgrounds/Notepad");

            //Load the upgrades images
            itemImgs[DOORBELL] = Content.Load<Texture2D>("Images/Sprites/Doorbell");
            itemImgs[TIMER_TOP] = Content.Load<Texture2D>("Images/Sprites/OvenTimer");
            itemImgs[TIMER_BOT] = itemImgs[TIMER_TOP];
            itemImgs[POSTER_1] = Content.Load<Texture2D>("Images/Sprites/Poster1");
            itemImgs[POSTER_2] = Content.Load<Texture2D>("Images/Sprites/Poster2");
            itemImgs[RADIO] = Content.Load<Texture2D>("Images/Sprites/Radio");

            //Load the customer images
            customerImgs[0] = Content.Load<Texture2D>("Images/Sprites/Customer1Walker");
            customerImgs[1] = Content.Load<Texture2D>("Images/Sprites/Customer2Walker");
            customerImgs[2] = Content.Load<Texture2D>("Images/Sprites/Customer3Walker");
            customerImgs[3] = Content.Load<Texture2D>("Images/Sprites/Customer4Walker");
            customerImgs[4] = Content.Load<Texture2D>("Images/Sprites/Customer5Walker");
            customerImgs[5] = Content.Load<Texture2D>("Images/Sprites/Customer6Walker");
            customerImgs[6] = Content.Load<Texture2D>("Images/Sprites/Customer7Walker");
            customerImgs[7] = Content.Load<Texture2D>("Images/Sprites/Customer8Walker");

            //Load the italian bread images
            breadImgs[ITALIAN] = new Texture2D[3];
            breadImgs[ITALIAN][BOT_BUN] = Content.Load<Texture2D>("Images/Sprites/ItalianBreadBottom");
            breadImgs[ITALIAN][FULL_LOAF] = Content.Load<Texture2D>("Images/Sprites/ItalianBreadFull");
            breadImgs[ITALIAN][TOP_BUN] = Content.Load<Texture2D>("Images/Sprites/ItalianBreadTop");

            //Load the italian herbs and cheese bread images
            breadImgs[HERBS_CHEESE] = new Texture2D[3];
            breadImgs[HERBS_CHEESE][BOT_BUN] = Content.Load<Texture2D>("Images/Sprites/ItalianHerbsCheeseBottom");
            breadImgs[HERBS_CHEESE][FULL_LOAF] = Content.Load<Texture2D>("Images/Sprites/ItalianHerbsCheeseFull");
            breadImgs[HERBS_CHEESE][TOP_BUN] = Content.Load<Texture2D>("Images/Sprites/ItalianHerbsCheeseTop");

            //Load the whole wheat bread images
            breadImgs[WHOLE_WHEAT] = new Texture2D[3];
            breadImgs[WHOLE_WHEAT][BOT_BUN] = Content.Load<Texture2D>("Images/Sprites/WholeWheatBottom");
            breadImgs[WHOLE_WHEAT][FULL_LOAF] = Content.Load<Texture2D>("Images/Sprites/WholeWheatFull");
            breadImgs[WHOLE_WHEAT][TOP_BUN] = Content.Load<Texture2D>("Images/Sprites/WholeWheatTop");

            //Load the knife and guiding line image
            knifeImgs[UNROTATED] = Content.Load<Texture2D>("Images/Sprites/Knife");
            knifeImgs[ROTATED] = Content.Load<Texture2D>("Images/Sprites/KnifeRotated");
            knifeImgs[KNIFE_CUTTING] = Content.Load<Texture2D>("Images/Sprites/KnifeCutting");
            guidingLineImg = Content.Load<Texture2D>("Images/Sprites/GuidingLine");

            //Load the meat images
            meatImgs[CHICKEN] = Content.Load<Texture2D>("Images/Sprites/RoastChicken");
            meatImgs[HAM] = Content.Load<Texture2D>("Images/Sprites/BlackForestHamSlice");
            meatImgs[SALAMI] = Content.Load<Texture2D>("Images/Sprites/SalamiSlice");

            //Load the meat container images
            meatContImgs[CHICKEN] = Content.Load<Texture2D>("Images/Sprites/ChickenContainer");
            meatContImgs[HAM] = Content.Load<Texture2D>("Images/Sprites/BlackForestHamContainer");
            meatContImgs[SALAMI] = Content.Load<Texture2D>("Images/Sprites/SalamiContainer");

            //Load the cheese images
            cheeseImgs[CHEDDAR] = Content.Load<Texture2D>("Images/Sprites/CheddarSlice");
            cheeseImgs[OLD_ENGLISH] = Content.Load<Texture2D>("Images/Sprites/OldEnglishSlice");
            cheeseImgs[SWISS] = Content.Load<Texture2D>("Images/Sprites/SwissSlice");

            //Load the cheese container images
            cheeseContImgs[CHEDDAR] = Content.Load<Texture2D>("Images/Sprites/CheddarContainer");
            cheeseContImgs[OLD_ENGLISH] = Content.Load<Texture2D>("Images/Sprites/OldEnglishContainer");
            cheeseContImgs[SWISS] = Content.Load<Texture2D>("Images/Sprites/SwissContainer");

            //Load the toaster and bar images
            toasterImg = Content.Load<Texture2D>("Images/Sprites/ToasterOven");
            toastingBarImg[EMPTY_BAR] = Content.Load<Texture2D>("Images/Sprites/ToasterBarEmpty");
            toastingBarImg[FULL_BAR] = Content.Load<Texture2D>("Images/Sprites/ToasterBarFull");
            perfectToastLineImg = Content.Load<Texture2D>("Images/Sprites/PerfectToastLine");
            rightArrowImg = Content.Load<Texture2D>("Images/Sprites/RightArrow");

            //Load the sauce/lettuce sprinkling bar images
            sprinklingBarImgs[EMPTY_BAR] = Content.Load<Texture2D>("Images/Sprites/SprinklingBarEmpty");
            sprinklingBarImgs[FULL_BAR] = Content.Load<Texture2D>("Images/Sprites/SprinklingBarFull");
            perfectAmntLineImg = Content.Load<Texture2D>("Images/Sprites/PerfectSprinkleLine");

            //Load the veggie images
            veggieImgs[LETTUCE] = Content.Load<Texture2D>("Images/Sprites/SingleLettuceShred");
            veggieImgs[TOMATO] = Content.Load<Texture2D>("Images/Sprites/TomatoSlice");
            veggieImgs[OLIVE] = Content.Load<Texture2D>("Images/Sprites/Olive");
            veggieImgs[PICKLE] = Content.Load<Texture2D>("Images/Sprites/PickleSlice");
            veggieImgs[CUCUMBER] = Content.Load<Texture2D>("Images/Sprites/CucumberSlice");

            //Load the veggie container images
            veggieContImgs[LETTUCE] = Content.Load<Texture2D>("Images/Sprites/LettuceContainer");
            veggieContImgs[TOMATO] = Content.Load<Texture2D>("Images/Sprites/TomatoContainer");
            veggieContImgs[OLIVE] = Content.Load<Texture2D>("Images/Sprites/OliveContainer");
            veggieContImgs[PICKLE] = Content.Load<Texture2D>("Images/Sprites/PickleContainer");
            veggieContImgs[CUCUMBER] = Content.Load<Texture2D>("Images/Sprites/CucumberContainer");

            //Load the scooper image
            scooperImg = Content.Load<Texture2D>("Images/Sprites/Scooper");

            //Load the sauce bottle images
            sauceBottleImgs[BBQ] = Content.Load<Texture2D>("Images/Sprites/BbqBottle");
            sauceTopImgs[BBQ] = Content.Load<Texture2D>("Images/Sprites/BbqBottleTop");
            sauceBottleImgs[CHIPOTLE] = Content.Load<Texture2D>("Images/Sprites/ChipotleBottle");
            sauceTopImgs[CHIPOTLE] = Content.Load<Texture2D>("Images/Sprites/ChipotleBottleTop");
            sauceBottleImgs[HONEY_MUSTARD] = Content.Load<Texture2D>("Images/Sprites/HoneyMustardBottle");
            sauceTopImgs[HONEY_MUSTARD] = Content.Load<Texture2D>("Images/Sprites/HoneyMustardBottleTop");
            sauceBottleImgs[RANCH] = Content.Load<Texture2D>("Images/Sprites/RanchBottle");
            sauceTopImgs[RANCH] = Content.Load<Texture2D>("Images/Sprites/RanchBottleTop");
            sauceBottleImgs[SWEET_CHILI] = Content.Load<Texture2D>("Images/Sprites/SweetChiliBottle");
            sauceTopImgs[SWEET_CHILI] = Content.Load<Texture2D>("Images/Sprites/SweetChiliBottleTop");

            //Load the sauce drop images
            sauceDropImgs[BBQ] = Content.Load<Texture2D>("Images/Sprites/BbqDrop");
            sauceDropImgs[CHIPOTLE] = Content.Load<Texture2D>("Images/Sprites/ChipotleDrop");
            sauceDropImgs[HONEY_MUSTARD] = Content.Load<Texture2D>("Images/Sprites/HoneyMustardDrop");
            sauceDropImgs[RANCH] = Content.Load<Texture2D>("Images/Sprites/RanchDrop");
            sauceDropImgs[SWEET_CHILI] = Content.Load<Texture2D>("Images/Sprites/SweetChilliDrop");

            //Load the sauce container image
            sauceContImg = Content.Load<Texture2D>("Images/Sprites/SauceStorage");

            //Load the order number selection images
            orderSelectionImg = Content.Load<Texture2D>("Images/Sprites/OrderNumberSelectionBox");
            orderNumBtns[0] = Content.Load<Texture2D>("Images/Sprites/Order1Btn");
            orderNumBtns[1] = Content.Load<Texture2D>("Images/Sprites/Order2Btn");
            orderNumBtns[2] = Content.Load<Texture2D>("Images/Sprites/Order3Btn");
            orderNumBtns[3] = Content.Load<Texture2D>("Images/Sprites/Order4Btn");
            orderNumBtns[4] = Content.Load<Texture2D>("Images/Sprites/Order5Btn");
            orderNumBtns[5] = Content.Load<Texture2D>("Images/Sprites/Order6Btn");
            orderNumBtns[6] = Content.Load<Texture2D>("Images/Sprites/Order7Btn");
            orderNumBtns[7] = Content.Load<Texture2D>("Images/Sprites/Order8Btn");

            //Store the score scale images
            scoreScaleImgs[WAIT_SCORE] = Content.Load<Texture2D>("Images/Sprites/WaitingScale");
            scoreScaleImgs[CUT_SCORE] = Content.Load<Texture2D>("Images/Sprites/CuttingScale");
            scoreScaleImgs[TOAST_SCORE] = Content.Load<Texture2D>("Images/Sprites/ToastingScale");
            scoreScaleImgs[TOPPING_SCORE] = Content.Load<Texture2D>("Images/Sprites/ToppingsScale");
            pointerImg = Content.Load<Texture2D>("Images/Sprites/ScalePointer");
            
            //Set the title location
            subwayLogoRec = new Rectangle(screenWidth / 2 - subwayLogoImg.Width / 2, 0, subwayLogoImg.Width, subwayLogoImg.Height);
            title2ndWordLoc = new Vector2(screenWidth / 2 - titleFont.MeasureString(title2ndWord).X / 2, subwayLogoRec.Bottom);

            //Store the day number location in the pre game
            preGameDayNumLoc = new Vector2(screenWidth / 2 - titleFont.MeasureString(dayNumText).X / 2, screenHeight / 4);

            //Set each customer's location
            custLocs[0] = new Vector2(screenWidth, screenHeight + custFootFloorDist - customerImgs[0].Height / 3);
            custLocs[1] = new Vector2(screenWidth, screenHeight + custFootFloorDist - customerImgs[1].Height / 3);
            custLocs[2] = new Vector2(screenWidth, screenHeight + custFootFloorDist - customerImgs[2].Height / 3);
            custLocs[3] = new Vector2(screenWidth, screenHeight + custFootFloorDist - customerImgs[3].Height / 3);
            custLocs[4] = new Vector2(screenWidth, screenHeight + custFootFloorDist - customerImgs[4].Height / 3);
            custLocs[5] = new Vector2(screenWidth, screenHeight + custFootFloorDist - customerImgs[5].Height / 3);
            custLocs[6] = new Vector2(screenWidth, screenHeight + custFootFloorDist - customerImgs[6].Height / 3);
            custLocs[7] = new Vector2(screenWidth, screenHeight + custFootFloorDist - customerImgs[7].Height / 3);

            //Set the  locations for the labels recorded in the notepad
            breadNoteLoc = new Vector2(screenWidth / 2 - writingFont.MeasureString(breadNoteLabel).X / 2, NOTEPAD_TOP_SPACER);
            meatNoteLoc = new Vector2(screenWidth / 2 - writingFont.MeasureString(meatNoteLabel).X / 2, breadNoteLoc.Y + writingFont.MeasureString(breadNoteLabel).Y + picturesNoteHeight + NOTEPAD_SECTION_SPACER);
            cheeseNoteLoc = new Vector2(screenWidth / 2 - writingFont.MeasureString(cheeseNoteLabel).X / 2, meatNoteLoc.Y + writingFont.MeasureString(meatNoteLabel).Y + picturesNoteHeight + NOTEPAD_SECTION_SPACER);
            veggieNoteLoc = new Vector2(screenWidth / 2 - writingFont.MeasureString(veggieNoteLabel).X / 2, cheeseNoteLoc.Y + writingFont.MeasureString(cheeseNoteLabel).Y + picturesNoteHeight + NOTEPAD_SECTION_SPACER);
            sauceNoteLoc = new Vector2(screenWidth / 2 - writingFont.MeasureString(sauceNoteLabel).X / 2, veggieNoteLoc.Y + writingFont.MeasureString(sauceNoteLabel).Y + picturesNoteHeight + NOTEPAD_SECTION_SPACER);

            //Store the remaining pieces location
            uncookedRemainPiecesLoc = new Vector2(screenWidth / 2, TOP_SCREEN_SPACE);
            cookedRemainPiecesLoc = new Vector2(screenWidth / 2, TOP_SCREEN_SPACE);

            //Set the lettuce and pointer origin
            lettuceOrigin = new Vector2(veggieImgs[LETTUCE].Width / 2, veggieImgs[LETTUCE].Height / 2);
            pointerOrigin = new Vector2(pointerImg.Width / 2, pointerImg.Height);

            //Store the location of the flip sign background
            signFlipLoc = new Vector2(0, screenHeight / 2 - openSignImg.Height / 10 / 2 * (float)screenWidth / (openSignImg.Width / 5));

            //Store the balance location in the shop
            balanceLoc = new Vector2(0, screenHeight - scoreFont.MeasureString(totalTipsText).Y);

            //Set the button rectangles in the menu
            playBtnRecs[NOT_HOVERING] = new Rectangle(screenWidth / 2 - playBtn.Width / 2, screenHeight / 2, playBtn.Width, playBtn.Height);
            instBtnRecs[NOT_HOVERING] = new Rectangle(screenWidth / 2 - playBtn.Width / 2, playBtnRecs[NOT_HOVERING].Bottom + BUTTON_SPACER, playBtn.Width, playBtn.Height);
            shopBtnRecs[NOT_HOVERING] = new Rectangle(screenWidth / 2 - playBtn.Width / 2, instBtnRecs[NOT_HOVERING].Bottom + BUTTON_SPACER, playBtn.Width, playBtn.Height);
            playBtnRecs[HOVERING] = new Rectangle(screenWidth / 2 - playBtn.Width / 2, playBtnRecs[NOT_HOVERING].Y - HOVER_DISPLACEMENT, playBtn.Width, playBtn.Height);
            instBtnRecs[HOVERING] = new Rectangle(screenWidth / 2 - playBtn.Width / 2, instBtnRecs[NOT_HOVERING].Y - HOVER_DISPLACEMENT, playBtn.Width, playBtn.Height);
            shopBtnRecs[HOVERING] = new Rectangle(screenWidth / 2 - playBtn.Width / 2, shopBtnRecs[NOT_HOVERING].Y - HOVER_DISPLACEMENT, playBtn.Width, playBtn.Height);

            //Set the purchase rectangle button
            purchaseBtnRec = new Rectangle(descRec.X, descRec.Bottom - purchaseBtns[HOVERING].Height, purchaseBtns[HOVERING].Width, purchaseBtns[NOT_HOVERING].Height);

            //Set the item title, description, and price location
            itemTitleLoc = new Vector2(descRec.X - descFont.MeasureString(itemNames[0]).X / 2 + descRec.Width / 2, descRec.Y + 2 * VERT_SPACER);
            itemDescLoc = new Vector2(descRec.X - descFont.MeasureString(itemDescs[0]).X / 2 + descRec.Width / 2, itemTitleLoc.Y + VERT_SPACER);
            priceLoc = new Vector2(descRec.X - descFont.MeasureString(priceText).X / 2 + descRec.Width / 2, purchaseBtnRec.Y + descFont.MeasureString(priceText).Y / 2);

            //Set the button rectangle in the pregame
            startBtnRecs[NOT_HOVERING] = new Rectangle(screenWidth / 2 - playBtn.Width / 2, screenHeight / 2, playBtn.Width, playBtn.Height);
            startBtnRecs[HOVERING] = new Rectangle(screenWidth / 2 - playBtn.Width / 2, startBtnRecs[NOT_HOVERING].Y - HOVER_DISPLACEMENT, playBtn.Width, playBtn.Height);

            //Set the menu button rectangle in the end game
            menuBtnRecs[NOT_HOVERING] = new Rectangle(screenWidth - playBtn.Width - SMALL_HORIZ_SPACER, screenHeight - VERT_SPACER - menuBtn.Height, playBtn.Width, playBtn.Height);
            menuBtnRecs[HOVERING] = new Rectangle(menuBtnRecs[NOT_HOVERING].X, menuBtnRecs[NOT_HOVERING].Y - HOVER_DISPLACEMENT, playBtn.Width, playBtn.Height);

            //Set the button rectangles in the play state
            takeOrderBtnRecs[NOT_HOVERING] = new Rectangle(CUSTOMER_LINE_START, screenHeight, takeOrderBtn.Width, takeOrderBtn.Height);
            takeOrderBtnRecs[HOVERING] = new Rectangle(CUSTOMER_LINE_START, screenHeight, takeOrderBtn.Width, takeOrderBtn.Height);
            uncookedGarbageBtnRec = new Rectangle(-garbageBtn[HOVERING].Width, screenHeight - garbageBtn[HOVERING].Height, garbageBtn[HOVERING].Width, garbageBtn[HOVERING].Height);
            cookedGarbageBtnRec = uncookedGarbageBtnRec;
            finishBtnRecs[NOT_HOVERING] = new Rectangle(screenWidth - finishBtn.Width - BUTTON_SPACER, screenHeight, finishBtn.Width, finishBtn.Height);
            finishBtnRecs[HOVERING] = new Rectangle(screenWidth - finishBtn.Width - BUTTON_SPACER, screenHeight, finishBtn.Width, finishBtn.Height);

            //Set the rectangles for the background in the storefront
            bgRec = new Rectangle(0, 0, screenWidth, screenHeight);
            roofRec = new Rectangle(screenWidth / 2 - roofImg.Width / 2, 0, roofImg.Width, roofImg.Height);
            screenRecs[0] = new Rectangle(roofRec.X, subwayLogoRec.Bottom, screenImgs[0].Width, screenImgs[1].Height);
            screenRecs[1] = new Rectangle(screenRecs[0].Right, subwayLogoRec.Bottom, screenImgs[1].Width, screenImgs[1].Height);
            screenRecs[2] = new Rectangle(screenRecs[1].Right, subwayLogoRec.Bottom, screenImgs[1].Width, screenImgs[1].Height);
            frontCounterRec = new Rectangle(roofRec.X, screenRecs[1].Bottom, frontCounterImg.Width, frontCounterImg.Height);

            //Set the all the rectangles for the instruction slides
            for (int i = 0; i < instrSlides.Length; i++)
            {
                //Set the rectangle for the instruction slide
                instrSlideRecs[i] = new Rectangle(screenWidth / 2 - instrSlides[i].Width / 2, screenHeight / 2 - instrSlides[i].Height / 2, instrSlides[i].Width, instrSlides[i].Height);
            }

            //Set the poster rectangles
            poster1Rec = new Rectangle(frontCounterRec.X + frontCounterRec.Width / 2 - itemImgs[POSTER_1].Width, frontCounterRec.Bottom - itemImgs[POSTER_1].Height + VERT_SPACER, itemImgs[POSTER_1].Width, itemImgs[POSTER_1].Height);
            poster2Rec = new Rectangle(frontCounterRec.X + frontCounterRec.Width / 2, frontCounterRec.Bottom - itemImgs[POSTER_2].Height + VERT_SPACER, itemImgs[POSTER_2].Width, itemImgs[POSTER_2].Height);

            //Set the upgrades rectangles
            itemRecs[DOORBELL] = new Rectangle(77, 79, itemImgs[DOORBELL].Width, itemImgs[DOORBELL].Height);
            itemRecs[TIMER_TOP] = new Rectangle(itemRecs[DOORBELL].Right + SHOP_HORIZ_SPACER, itemRecs[DOORBELL].Y, itemImgs[TIMER_TOP].Width, itemImgs[TIMER_TOP].Height);
            itemRecs[TIMER_BOT] = new Rectangle(itemRecs[TIMER_TOP].Right + SHOP_HORIZ_SPACER, itemRecs[DOORBELL].Y, itemImgs[TIMER_BOT].Width, itemImgs[TIMER_BOT].Height);
            itemRecs[POSTER_1] = new Rectangle(itemRecs[DOORBELL].X, itemRecs[DOORBELL].Bottom + SHOP_VERT_SPACER, itemImgs[POSTER_1].Width, itemImgs[POSTER_1].Height);
            itemRecs[POSTER_2] = new Rectangle(itemRecs[TIMER_TOP].X, itemRecs[DOORBELL].Bottom + SHOP_VERT_SPACER, itemImgs[POSTER_2].Width, itemImgs[POSTER_2].Height);
            itemRecs[RADIO] = new Rectangle(itemRecs[TIMER_BOT].X, itemRecs[POSTER_1].Y, itemImgs[RADIO].Width, itemImgs[RADIO].Height);

            //Set the item viewing rectangle in the shop
            itemViewingRec = new Rectangle(purchaseBtnRec.X + purchaseBtnRec.Width / 2 - itemRecs[DOORBELL].Width / 2, purchaseBtnRec.Y - itemRecs[DOORBELL].Height, itemRecs[DOORBELL].Width, itemRecs[DOORBELL].Height);

            //Set the rectangle for the notepad background and order number label in the taking orders state
            notepadBgRec = new Rectangle(screenWidth / 2 - notepadImg.Width / 2, 0, notepadImg.Width, notepadImg.Height);
            orderNumNoteLoc = new Vector2(NOTEPAD_LEFT_SPACER + notepadBgRec.X, NOTEPAD_TOP_SPACER);

            //Set the rectangles for the images recorded in the notepad
            breadRecordRec = new Rectangle((int)(screenWidth / 2 - breadImgs[ITALIAN][FULL_LOAF].Width * picturesNoteHeight / breadImgs[ITALIAN][FULL_LOAF].Height / 2.0),
                                           screenHeight,
                                           (int)(breadImgs[ITALIAN][FULL_LOAF].Width * picturesNoteHeight / (float)breadImgs[ITALIAN][FULL_LOAF].Height),
                                           picturesNoteHeight);
            meatRecordRecs = new List<Rectangle>();
            cheeseRecordRec = new Rectangle((int)(screenWidth / 2 - cheeseImgs[CHEDDAR].Width * picturesNoteHeight / cheeseImgs[CHEDDAR].Height / 2.0),
                                            screenHeight,
                                            (int)(cheeseImgs[CHEDDAR].Width * picturesNoteHeight / (float)cheeseImgs[CHEDDAR].Height),
                                            picturesNoteHeight);
            veggieRecordRecs = new List<Rectangle[]>();
            sauceRecordRecs = new List<Rectangle[]>();

            //Set each bread option rectangles array to have 2 elements for the position when the mouse is hovering or not hovering
            for (int i = 0; i < NUM_BREAD_CHOICES; i++)
            {
                //Set the each bread option rectangle arrays having 2 element for a non hovering rectangle and a hovering rectangle
                breadOptionRecs[i] = new Rectangle[2];
            }

            //Set the bread option rectangles
            breadOptionRecs[ITALIAN][NOT_HOVERING] = new Rectangle(0, VERT_SPACER, (int)(breadImgs[ITALIAN][FULL_LOAF].Width * breadOptionScaler), (int)(breadImgs[ITALIAN][FULL_LOAF].Height * breadOptionScaler));
            breadOptionRecs[HERBS_CHEESE][NOT_HOVERING] = new Rectangle(0, VERT_SPACER + breadOptionRecs[ITALIAN][NOT_HOVERING].Bottom, (int)(breadImgs[HERBS_CHEESE][FULL_LOAF].Width * breadOptionScaler), (int)(breadImgs[HERBS_CHEESE][FULL_LOAF].Height * breadOptionScaler));
            breadOptionRecs[WHOLE_WHEAT][NOT_HOVERING] = new Rectangle(0, VERT_SPACER + breadOptionRecs[HERBS_CHEESE][NOT_HOVERING].Bottom, (int)(breadImgs[WHOLE_WHEAT][FULL_LOAF].Width * breadOptionScaler), (int)(breadImgs[WHOLE_WHEAT][FULL_LOAF].Height * breadOptionScaler));
            breadOptionRecs[ITALIAN][HOVERING] = new Rectangle(HOVER_DISPLACEMENT, VERT_SPACER, (int)(breadImgs[ITALIAN][FULL_LOAF].Width * breadOptionScaler), (int)(breadImgs[ITALIAN][FULL_LOAF].Height * breadOptionScaler));
            breadOptionRecs[HERBS_CHEESE][HOVERING] = new Rectangle(HOVER_DISPLACEMENT, VERT_SPACER + breadOptionRecs[ITALIAN][HOVERING].Bottom, (int)(breadImgs[HERBS_CHEESE][FULL_LOAF].Width * breadOptionScaler), (int)(breadImgs[HERBS_CHEESE][FULL_LOAF].Height * breadOptionScaler));
            breadOptionRecs[WHOLE_WHEAT][HOVERING] = new Rectangle(HOVER_DISPLACEMENT, VERT_SPACER + breadOptionRecs[HERBS_CHEESE][HOVERING].Bottom, (int)(breadImgs[WHOLE_WHEAT][FULL_LOAF].Width * breadOptionScaler), (int)(breadImgs[WHOLE_WHEAT][FULL_LOAF].Height * breadOptionScaler));


            //Set the bread rectangle for building the sandwich
            for (int i = 0; i < buildingBreadRecs.Length; i++)
            {
                //Set each building bread rectangle
                buildingBreadRecs[BOT_BUN] = new Rectangle(screenWidth / 2 - breadImgs[ITALIAN][BOT_BUN].Width / 2, screenHeight - SUB_BOTTOM_VERT_SPACER - breadImgs[ITALIAN][BOT_BUN].Height, breadImgs[ITALIAN][BOT_BUN].Width, breadImgs[ITALIAN][BOT_BUN].Height);
                buildingBreadRecs[FULL_BAR] = new Rectangle(screenWidth / 2 - breadImgs[ITALIAN][FULL_LOAF].Width / 2, screenHeight - SUB_BOTTOM_VERT_SPACER - breadImgs[ITALIAN][FULL_LOAF].Height, breadImgs[ITALIAN][FULL_LOAF].Width, breadImgs[ITALIAN][FULL_LOAF].Height);
                buildingBreadRecs[TOP_BUN] = new Rectangle(screenWidth / 2 - breadImgs[ITALIAN][TOP_BUN].Width / 2, -breadImgs[ITALIAN][TOP_BUN].Height, breadImgs[ITALIAN][TOP_BUN].Width, breadImgs[ITALIAN][TOP_BUN].Height);
            }

            //Set the toaster rectangles
            toasterRecs[TOP_TOASTER] = new Rectangle(screenWidth / 2 - toasterImg.Width / 2, 0, toasterImg.Width, toasterImg.Height);
            toasterRecs[BOT_TOASTER] = new Rectangle(screenWidth / 2 - toasterImg.Width / 2, screenHeight - toasterImg.Height, toasterImg.Width, toasterImg.Height);

            //Set the toaster bars and toaster perfect line rectangles
            for (int i = 0; i < NUM_TOASTERS; i++)
            {
                //Set the empty and full toasting bar rectangles
                toastingBarRecs[i] = new Rectangle[2];
                toastingBarRecs[i][EMPTY_BAR] = new Rectangle(toasterRecs[i].X + TOASTER_BAR_X_SPACER, toasterRecs[i].Y + TOASTER_BAR_Y_SPACER, toastingBarImg[FULL_BAR].Width, toastingBarImg[FULL_BAR].Height);
                toastingBarRecs[i][FULL_BAR] = new Rectangle(toasterRecs[i].X + TOASTER_BAR_X_SPACER, toastingBarRecs[i][EMPTY_BAR].Bottom, toastingBarImg[FULL_BAR].Width, 0);

                //Set the perfect toast line rectangle at four fifths of the bar height
                perfToastLineRecs[i] = new Rectangle(toastingBarRecs[i][FULL_BAR].X, (int)(toastingBarRecs[i][EMPTY_BAR].Y + toastingBarRecs[i][EMPTY_BAR].Height * 1.0 / 5), perfectToastLineImg.Width, perfectToastLineImg.Height);
            }

            //Set the right arrow rectangle
            rightArrowRec = new Rectangle(screenWidth - rightArrowImg.Width, screenHeight - rightArrowImg.Height - SUB_BOTTOM_VERT_SPACER, rightArrowImg.Width, rightArrowImg.Height);

            //Set the bread rectangles at the toasting station
            breadInLineToasterRec = new Rectangle(0,
                                                  (int)(screenHeight - buildingBreadRecs[BOT_BUN].Height * subScalerAtToaster - SUB_BOTTOM_VERT_SPACER),
                                                  (int)(buildingBreadRecs[BOT_BUN].Width * subScalerAtToaster),
                                                  (int)(buildingBreadRecs[BOT_BUN].Height * subScalerAtToaster));
            breadToastingRec[TOP_TOASTER] = new Rectangle(toasterRecs[TOP_TOASTER].X + TOASTER_WINDOW_SPACER,
                                                          (int)(toasterRecs[TOP_TOASTER].Bottom / 2 - buildingBreadRecs[BOT_BUN].Height * subScalerAtToaster / 2),
                                                          (int)(buildingBreadRecs[BOT_BUN].Width * subScalerAtToaster),
                                                          (int)(buildingBreadRecs[BOT_BUN].Height * subScalerAtToaster));
            breadToastingRec[BOT_TOASTER] = new Rectangle(toasterRecs[TOP_TOASTER].X + TOASTER_WINDOW_SPACER,
                                                          (int)(toasterRecs[BOT_TOASTER].Y + toasterRecs[BOT_TOASTER].Height / 2 - buildingBreadRecs[BOT_BUN].Height * subScalerAtToaster / 2),
                                                          (int)(buildingBreadRecs[BOT_BUN].Width * subScalerAtToaster),
                                                          (int)(buildingBreadRecs[BOT_BUN].Height * subScalerAtToaster));
            breadAwayLineToasterRec = new Rectangle((int)(screenWidth - buildingBreadRecs[BOT_BUN].Width * subScalerAtToaster),
                                                    (int)(screenHeight - buildingBreadRecs[BOT_BUN].Height * subScalerAtToaster - SUB_BOTTOM_VERT_SPACER),
                                                    (int)(buildingBreadRecs[BOT_BUN].Width * subScalerAtToaster),
                                                    (int)(buildingBreadRecs[BOT_BUN].Height * subScalerAtToaster));

            //Set the knife rectangles
            unrotatedKnifeRecs[NOT_HOVERING] = new Rectangle(breadOptionRecs[WHOLE_WHEAT][NOT_HOVERING].Right / 2 - knifeImgs[UNROTATED].Width / 2, screenHeight / 2, knifeImgs[UNROTATED].Width, knifeImgs[UNROTATED].Height);
            unrotatedKnifeRecs[HOVERING] = new Rectangle(breadOptionRecs[WHOLE_WHEAT][NOT_HOVERING].Right / 2 - knifeImgs[UNROTATED].Width / 2, screenHeight / 2 - HOVER_DISPLACEMENT, knifeImgs[UNROTATED].Width, knifeImgs[UNROTATED].Height);
            rotatedKnifeRecs[ROTATED - 1] = new Rectangle(unrotatedKnifeRecs[HOVERING].X, unrotatedKnifeRecs[HOVERING].Y, knifeImgs[ROTATED].Width, knifeImgs[ROTATED].Height);
            rotatedKnifeRecs[KNIFE_CUTTING - 1] = new Rectangle(unrotatedKnifeRecs[HOVERING].X, unrotatedKnifeRecs[HOVERING].Y, knifeImgs[KNIFE_CUTTING].Width, knifeImgs[KNIFE_CUTTING].Height);

            //Set the guiding line rectangle 
            guidingLineRec = new Rectangle(buildingBreadRecs[FULL_LOAF].X, buildingBreadRecs[FULL_LOAF].Y + perfectCuttingLoc, guidingLineImg.Width, guidingLineImg.Height);

            //Set the meat container rectangles
            meatContRecs[CHICKEN] = new Rectangle(screenWidth - meatContImgs[CHICKEN].Width - meatContImgs[SALAMI].Width - meatContImgs[HAM].Width, 0, meatContImgs[CHICKEN].Width, meatContImgs[CHICKEN].Height);
            meatContRecs[HAM] = new Rectangle(meatContRecs[CHICKEN].Right, 0, meatContImgs[HAM].Width, meatContImgs[HAM].Height);
            meatContRecs[SALAMI] = new Rectangle(meatContRecs[HAM].Right, 0, meatContImgs[SALAMI].Width, meatContImgs[SALAMI].Height);

            //Set the cheese container rectangles
            cheeseContRecs[CHEDDAR] = new Rectangle(meatContRecs[CHICKEN].X, meatContRecs[CHICKEN].Bottom, cheeseContImgs[CHEDDAR].Width, cheeseContImgs[CHEDDAR].Height);
            cheeseContRecs[OLD_ENGLISH] = new Rectangle(meatContRecs[HAM].X, meatContRecs[HAM].Bottom, cheeseContImgs[OLD_ENGLISH].Width, cheeseContImgs[OLD_ENGLISH].Height);
            cheeseContRecs[SWISS] = new Rectangle(meatContRecs[SALAMI].X, meatContRecs[SALAMI].Bottom, cheeseContImgs[SWISS].Width, cheeseContImgs[SWISS].Height);

            //Set the veggie container rectangles
            veggieContRecs[LETTUCE] = new Rectangle(0, 0, veggieContImgs[LETTUCE].Width, veggieContImgs[LETTUCE].Height);
            veggieContRecs[TOMATO] = new Rectangle(veggieContRecs[LETTUCE].Right, 0, veggieContImgs[TOMATO].Width, veggieContImgs[TOMATO].Height);
            veggieContRecs[OLIVE] = new Rectangle(0, veggieContRecs[LETTUCE].Bottom, veggieContImgs[OLIVE].Width, veggieContImgs[OLIVE].Height);
            veggieContRecs[PICKLE] = new Rectangle(veggieContRecs[OLIVE].Right, veggieContRecs[LETTUCE].Bottom, veggieContImgs[PICKLE].Width, veggieContImgs[PICKLE].Height);
            veggieContRecs[CUCUMBER] = new Rectangle(veggieContRecs[TOMATO].X, veggieContRecs[TOMATO].Bottom, veggieContImgs[CUCUMBER].Width, veggieContImgs[CUCUMBER].Height);

            //Set the scooper rectangle out of the screen
            scooperRec = new Rectangle(screenWidth, TOP_SCREEN_SPACE, scooperImg.Width / 2, scooperImg.Height / 2);

            //Set the sauce container rectangle
            sauceContRec = new Rectangle(screenWidth - sauceContImg.Width, 0, sauceContImg.Width, sauceContImg.Height);

            //Set the sauce bottle top rectangles
            sauceBottleTopsRecs[CHIPOTLE] = new Rectangle(sauceContRec.X + SAUCE_CONT_HORIZ_EDGE_SPACER, VERT_SPACER, sauceTopImgs[BBQ].Width, sauceTopImgs[BBQ].Height);
            sauceBottleTopsRecs[HONEY_MUSTARD] = new Rectangle(sauceBottleTopsRecs[CHIPOTLE].Right, VERT_SPACER, sauceTopImgs[BBQ].Width, sauceTopImgs[BBQ].Height);
            sauceBottleTopsRecs[BBQ] = new Rectangle(sauceBottleTopsRecs[CHIPOTLE].X, sauceBottleTopsRecs[CHIPOTLE].Bottom, sauceTopImgs[BBQ].Width, sauceTopImgs[BBQ].Height);
            sauceBottleTopsRecs[RANCH] = new Rectangle(sauceBottleTopsRecs[CHIPOTLE].Right, sauceBottleTopsRecs[CHIPOTLE].Bottom, sauceTopImgs[BBQ].Width, sauceTopImgs[BBQ].Height);
            sauceBottleTopsRecs[SWEET_CHILI] = new Rectangle(sauceBottleTopsRecs[CHIPOTLE].X, sauceBottleTopsRecs[BBQ].Bottom, sauceTopImgs[BBQ].Width, sauceTopImgs[BBQ].Height);

            //Set the sprinkling bar rectangle
            sprinklingBarRecs[EMPTY_BAR] = new Rectangle(screenWidth, sauceContRec.Bottom + VERT_SPACER, sprinklingBarImgs[EMPTY_BAR].Width, sprinklingBarImgs[EMPTY_BAR].Height);
            sprinklingBarRecs[FULL_BAR] = new Rectangle(screenWidth, sauceContRec.Bottom + VERT_SPACER, 0, sprinklingBarImgs[EMPTY_BAR].Height);
            sprinklingBarLineRec = new Rectangle(screenWidth, sauceContRec.Bottom + VERT_SPACER, perfectAmntLineImg.Width, perfectAmntLineImg.Height);

            //Set the sauce bottle side rectangle
            sauceBottleSideRec = new Rectangle(screenWidth, TOP_SCREEN_SPACE, sauceBottleImgs[BBQ].Width, sauceBottleImgs[BBQ].Height);

            //Set the order number selection rectangle
            orderSelectionRec = new Rectangle(screenWidth / 2 - orderSelectionImg.Width / 2, screenHeight, orderSelectionImg.Width, orderSelectionImg.Height);

            //Set the order number button rectangles
            for (int i = 0; i < MAX_CUSTOMERS; i++)
            {
                //Set each order number button as having 2 rectangles for hovering and not hovering
                orderNumBtnRecs[i] = new Rectangle[2];

                //Set the rectangle for each order number button
                orderNumBtnRecs[i][NOT_HOVERING] = new Rectangle(orderSelectionRec.X + (i - 1) * orderNumBtns[i].Width + ORDER_NUM_BTN_SPACER, orderSelectionRec.Y + ORDER_SELECTION_TOP_SPACER, orderNumBtns[i].Width, orderNumBtns[i].Height);
                orderNumBtnRecs[i][HOVERING] = new Rectangle(orderSelectionRec.X + (i - 1) * orderNumBtns[i].Width + ORDER_NUM_BTN_SPACER, orderSelectionRec.Y + ORDER_SELECTION_TOP_SPACER - HOVER_DISPLACEMENT, orderNumBtns[i].Width, orderNumBtns[i].Height);
            }

            //Set the rectangle for the score scales
            scoreScaleRecs[CUT_SCORE] = new Rectangle(screenWidth / 2 - scoreScaleImgs[CUT_SCORE].Width - SMALL_HORIZ_SPACER / 2, 0, scoreScaleImgs[CUT_SCORE].Width, scoreScaleImgs[CUT_SCORE].Height);
            scoreScaleRecs[WAIT_SCORE] = new Rectangle(scoreScaleRecs[CUT_SCORE].X - scoreScaleImgs[WAIT_SCORE].Width - SMALL_HORIZ_SPACER, 0, scoreScaleImgs[WAIT_SCORE].Width, scoreScaleImgs[WAIT_SCORE].Height);
            scoreScaleRecs[TOAST_SCORE] = new Rectangle(screenWidth / 2 + SMALL_HORIZ_SPACER / 2, 0, scoreScaleImgs[TOAST_SCORE].Width, scoreScaleImgs[TOAST_SCORE].Height);
            scoreScaleRecs[TOPPING_SCORE] = new Rectangle(scoreScaleRecs[TOAST_SCORE].Right + SMALL_HORIZ_SPACER, 0, scoreScaleImgs[TOPPING_SCORE].Width, scoreScaleImgs[TOPPING_SCORE].Height);
            scorePointerRecs[CUT_SCORE] = new Rectangle(scoreScaleRecs[CUT_SCORE].X + (int)pointerLoc.X + pointerImg.Width / 2, scoreScaleRecs[CUT_SCORE].Y + (int)pointerLoc.Y, pointerImg.Width, pointerImg.Height);
            scorePointerRecs[WAIT_SCORE] = new Rectangle(scoreScaleRecs[WAIT_SCORE].X + (int)pointerLoc.X + pointerImg.Width / 2, scoreScaleRecs[WAIT_SCORE].Y + (int)pointerLoc.Y, pointerImg.Width, pointerImg.Height);
            scorePointerRecs[TOAST_SCORE] = new Rectangle(scoreScaleRecs[TOAST_SCORE].X + (int)pointerLoc.X + pointerImg.Width / 2, scoreScaleRecs[TOAST_SCORE].Y + (int)pointerLoc.Y, pointerImg.Width, pointerImg.Height);
            scorePointerRecs[TOPPING_SCORE] = new Rectangle(scoreScaleRecs[TOPPING_SCORE].X + (int)pointerLoc.X + pointerImg.Width / 2, scoreScaleRecs[TOPPING_SCORE].Y + (int)pointerLoc.Y, pointerImg.Width, pointerImg.Height);

            //Set the control arrows and tabs rectangles
            controlArrowRecs1[LEFT] = new Rectangle(0, 0, controlArrows[LEFT].Width, controlArrows[LEFT].Height);
            controlArrowRecs1[RIGHT] = new Rectangle(screenWidth - controlArrows[RIGHT].Width, 0, controlArrows[LEFT].Width, controlArrows[LEFT].Height);
            controlArrowRecs2[LEFT] = new Rectangle(0, unrotatedKnifeRecs[NOT_HOVERING].Bottom + VERT_SPACER, controlArrows[LEFT].Width, controlArrows[LEFT].Height);
            controlArrowRecs2[RIGHT] = new Rectangle(screenWidth - controlArrows[RIGHT].Width, unrotatedKnifeRecs[NOT_HOVERING].Bottom + VERT_SPACER, controlArrows[LEFT].Width, controlArrows[LEFT].Height);
            tabRec = new Rectangle(finishBtnRecs[HOVERING].X - checkOrdersTab.Width - SMALL_HORIZ_SPACER, screenHeight - checkOrdersTab.Height, checkOrdersTab.Width, checkOrdersTab.Height);

            //Set the order's score text locations at the customer's table
            for (int i = 0; i < NUM_SCORES; i++)
            {
                //Set the score text location
                scoreTextLocs[i] = new Vector2(scoreScaleRecs[i].Center.X - scoreFont.MeasureString(scoreTexts[i]).X / 2, scoreScaleRecs[i].Bottom - scoreFont.MeasureString(scoreTexts[i]).Y);
            }

            //Set the final average score text location, points text, and tips text
            finalAvgScoreLoc = new Vector2(screenWidth / 2, scoreScaleRecs[0].Bottom + VERT_SPACER);
            ptsLoc = new Vector2(screenWidth - scoreFont.MeasureString(pointsText).X, finalAvgScoreLoc.Y + scoreFont.MeasureString(finalAvgScoreText).Y + VERT_SPACER);
            tipsLoc = new Vector2(screenWidth - scoreFont.MeasureString(tipsText).X, ptsLoc.Y + scoreFont.MeasureString(pointsText).Y + VERT_SPACER);
            totalPtsLoc = new Vector2(screenWidth - scoreFont.MeasureString(totalPtsText).X - HORIZ_SCORE_SPACER, finalAvgScoreLoc.Y + scoreFont.MeasureString(finalAvgScoreText).Y + VERT_SPACER);
            totalTipsLoc = new Vector2(screenWidth - scoreFont.MeasureString(totalTipsText).X - HORIZ_SCORE_SPACER, ptsLoc.Y + scoreFont.MeasureString(totalPtsText).Y + VERT_SPACER);

            //Set the day number location in the menu and the end game
            dayNumLoc = new Vector2(screenWidth - scoreFont.MeasureString(dayNumText).X - HORIZ_SCORE_SPACER, finalAvgScoreLoc.Y);

            //Set the animations for the customers
            customers[0] = new Animation(customerImgs[0], 8, 3, 24, 0, 4, Animation.ANIMATE_FOREVER, 3, custLocs[0], 1, true);
            customers[1] = new Animation(customerImgs[1], 8, 3, 24, 0, 4, Animation.ANIMATE_FOREVER, 3, custLocs[1], 1f, true);
            customers[2] = new Animation(customerImgs[2], 8, 3, 24, 0, 4, Animation.ANIMATE_FOREVER, 3, custLocs[2], 1f, true);
            customers[3] = new Animation(customerImgs[3], 8, 3, 24, 0, 4, Animation.ANIMATE_FOREVER, 3, custLocs[3], 1f, true);
            customers[4] = new Animation(customerImgs[4], 8, 3, 24, 0, 4, Animation.ANIMATE_FOREVER, 3, custLocs[4], 1f, true);
            customers[5] = new Animation(customerImgs[5], 8, 3, 24, 0, 4, Animation.ANIMATE_FOREVER, 3, custLocs[5], 1f, true);
            customers[6] = new Animation(customerImgs[6], 8, 3, 24, 0, 4, Animation.ANIMATE_FOREVER, 3, custLocs[6], 1f, true);
            customers[7] = new Animation(customerImgs[7], 8, 3, 24, 0, 4, Animation.ANIMATE_FOREVER, 3, custLocs[7], 1f, true);

            //Set the animation for the sign flip
            closedSignAnim = new Animation(closedSignImg, 5, 10, 48, 0, 47, Animation.ANIMATE_ONCE, 4, signFlipLoc, (float)screenWidth / (closedSignImg.Width / 5), false);
            openSignAnim = new Animation(openSignImg, 5, 10, 48, 0, 47, Animation.ANIMATE_ONCE, 4, signFlipLoc, (float)screenWidth / (openSignImg.Width / 5), false);

            //Set the taking order timer
            takingOrderTimer = new Timer(MILLISEC_PER_SEC, false);

            //Set the toaster timers
            toastingTimer[TOP_TOASTER] = new Timer(Timer.INFINITE_TIMER, false);
            toastingTimer[BOT_TOASTER] = new Timer(Timer.INFINITE_TIMER, false);

            //Set the timer at the customer table
            custTableTimer = new Timer(5000, false);

            //Set the maximum number of pieces of each type of meat
            for (int i = 0; i < NUM_MEAT_CHOICES; i++)
            {
                maxMeatPieces[i] = buildingBreadRecs[FULL_LOAF].Width / meatImgs[i].Width;
            }

            //Set the maximum number of pieces of each type of cheese
            for (int i = 0; i < NUM_CHEESE_CHOICES; i++)
            {
                maxCheesePieces[i] = buildingBreadRecs[FULL_LOAF].Width / cheeseImgs[i].Width;
            }

            //Set the max pieces for each type of vegetable
            maxVeggiePieces[LETTUCE] = 100;
            maxVeggiePieces[TOMATO] = buildingBreadRecs[FULL_LOAF].Width / veggieImgs[TOMATO].Width;
            maxVeggiePieces[OLIVE] = 12;
            maxVeggiePieces[PICKLE] = buildingBreadRecs[FULL_LOAF].Width / veggieImgs[PICKLE].Width;
            maxVeggiePieces[CUCUMBER] = buildingBreadRecs[FULL_LOAF].Width / veggieImgs[CUCUMBER].Width;

            //Set up the sound effect volume
            SoundEffect.MasterVolume = 1f;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Get the current and previous mouse and keyboard states
            prevMouse = mouse;
            prevKb = kb;
            mouse = Mouse.GetState();
            kb = Keyboard.GetState();

            //Update the game depending on the game state
            switch (gameState)
            {
                case MENU:
                    //Update the menu
                    UpdateMenu();
                    break;

                case PLAY:
                    //Update the play
                    UpdatePlay(gameTime);
                    break;

                case PRE_PREGAME:
                    //Update the pre-pregame
                    UpdatePrePregame();
                    break;

                case PREGAME:
                    //Update the pregame
                    UpdatePregame(gameTime);
                    break;

                case INSTRUCTIONS:
                    //Update the instructions
                    UpdateInstructions();
                    break;

                case PRE_ENDGAME:
                    //Update the pre-endgame
                    UpdatePreEndGame(gameTime);
                    break;

                case ENDGAME:
                    //Update the end game
                    UpdateEndGame(gameTime);
                    break;

                case SHOP:
                    //Update the shop
                    UpdateShop();
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGreen);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            switch (gameState)
            {
                case MENU:
                    //Draw the menu
                    DrawMenu();
                    break;

                case PLAY:
                    //Draw the play state
                    DrawPlay();
                    break;

                case PRE_PREGAME:
                    //Draw the pre-pregame
                    DrawPrePregame();
                    break;

                case PREGAME:
                    //Draw the pregame
                    DrawPregame();
                    break;

                case INSTRUCTIONS:
                    //Draw the instructions
                    DrawInstructions();
                    break;

                case PRE_ENDGAME:
                    //Draw the pre-endgame
                    DrawPreEndGame();
                    break;

                case ENDGAME:
                    //Draw the endgame
                    DrawEndGame();
                    break;

                case SHOP:
                    //Draw the shop
                    DrawShop();
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Pre: None
        //Post: None
        //Desc: Update the menu state
        private void UpdateMenu()
        {
            //Play the menu music
            if (MediaPlayer.State != MediaState.Playing)
            {
                //Play the menu music at max volume
                MediaPlayer.Play(menuMusic);
                MediaPlayer.Volume = 1f;
            }

            //Set the hovering over the buttons
            hoverOverPlayBtn = playBtnRecs[NOT_HOVERING].Contains(mouse.Position);
            hoverOverInstBtn = instBtnRecs[NOT_HOVERING].Contains(mouse.Position);
            hoverOverShopBtn = shopBtnRecs[NOT_HOVERING].Contains(mouse.Position);

            //Change the state according to the mouse click
            if (hoverOverPlayBtn && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                //Play the button click sound effect 
                btnClickSnd.CreateInstance().Play();

                //Change the games state to pre game
                gameState = PRE_PREGAME;

                //Update the day number
                day++;
                dayNumText = "Day " + day;

                //Stop the music
                MediaPlayer.Stop();
            }
            else if (hoverOverInstBtn && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                //Play the button click sound effect 
                btnClickSnd.CreateInstance().Play();

                //Change the game state to the instructions
                gameState = INSTRUCTIONS;
            }
            else if (hoverOverShopBtn && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                //Change the game state to shop
                gameState = SHOP;

                //Play the button click sound effect 
                btnClickSnd.CreateInstance().Play();
            }
        }
        
        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update the play state and its gameplay
        private void UpdatePlay(GameTime gameTime)
        {
            //Play the background music if the radio is purchased
            if (itemIsPurchased[RADIO] && MediaPlayer.State != MediaState.Playing)
            {
                //Play the background music at the right volume
                MediaPlayer.Play(stereoMusic);
                MediaPlayer.Volume = stereoVol;
            }

            //Update the customer entrance timer for when the next customer enters
            custEntranceTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            //Add a new customer in the store when the entrance timer is finished
            if (custEntranceTimer.IsFinished())
            {
                //Only add a new customer when there is less customers than the total amount of customers
                if (numCustAppeared < totalNumCust && custInLine.Count < lineStoppingLocs.Length)
                {
                    //Play the doorbell sound if it is purchased
                    if (itemIsPurchased[DOORBELL])
                    {
                        //Play the doorbell sound
                        doorbellSnd.CreateInstance().Play();
                    }

                    //Add a new customer in the store and in the line
                    custInStore.Add(custAppearances[numCustAppeared]);
                    custInLine.Add(custAppearances[numCustAppeared]);

                    //Set the customer's state as walking in
                    custStates[custAppearances[numCustAppeared]] = WALKING_IN;

                    //Reset the customer entrance timer
                    custEntranceTimer = new Timer(rng.Next((int)MIN_CUST_ENTRANCE_DELAY, (int)MAX_CUST_ENTRANCE_DELAY + 1), false);

                    //Increase the number of customers that have appeared
                    numCustAppeared++;
                }

            }

            //Update every customer that is in the store
            for (int i = 0; i < custInStore.Count; i++)
            {
                //Update every customer
                switch (custStates[custInStore[i]])
                {
                    case NOT_ORDERED:
                        //Update the customer wait timer
                        custWaitTimers[Array.IndexOf(custAppearances, custInStore[i])].Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                        break;

                    case WALKING_IN:
                        //Move the customer left
                        MoveCustomer(custInStore[i], gameTime);

                        //Change the customer's state when they've reached their position in line
                        if (custLocs[custInStore[i]].X <= lineStoppingLocs[custInLine.IndexOf(custInStore[i])])
                        {
                            //Set the customer as not ordered and start their wait timer
                            custStates[custInStore[i]] = NOT_ORDERED;
                            custWaitTimers[numCustAppeared - 1].Activate();

                            //Prepare for the next customer's entrance if there are more customers
                            if (numCustAppeared < totalNumCust)
                            {
                                //Activate the cutomer entrance timer
                                custEntranceTimer.Activate();
                            }

                            //Check if it is the first customer in line
                            if (custInLine[0] == custInStore[i])
                            {
                                //Set the take order button above the customer
                                takeOrderBtnRecs[NOT_HOVERING].Y = (int)custLocs[custInStore[i]].Y - takeOrderBtnRecs[NOT_HOVERING].Height;
                                takeOrderBtnRecs[HOVERING].Y = takeOrderBtnRecs[NOT_HOVERING].Y - HOVER_DISPLACEMENT;
                            }
                        }
                        break;

                    case WALKING_TO_TABLE:
                        //Update the customer wait timer
                        custWaitTimers[Array.IndexOf(custAppearances, custInStore[i])].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                        //Move the customer to the left
                        MoveCustomer(custInStore[i], gameTime);

                        //Set the customer as waiting for its order if it is off the screen
                        if (customers[custInStore[i]].destRec.Right <= 0)
                        {
                            //Set the customer as waiting for its order
                            custStates[custInStore[i]] = WAITING_FOR_ORDER;
                        }
                        break;

                    case WAITING_FOR_ORDER:
                        //Update each customer's wait timer if they are waiting for their order
                        custWaitTimers[Array.IndexOf(custAppearances, custInStore[i])].Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                        break;

                    case MOVING_UP:
                        //Move the customer that just gave their order
                        MoveCustomer(custInStore[i], gameTime);

                        //Change the customer's state to not ordered yet if they are at their position in line
                        if (customers[custInStore[i]].destRec.X <= lineStoppingLocs[custInLine.IndexOf(custInStore[i])])
                        {
                            //Set the customer as not ordered
                            custStates[custInStore[i]] = NOT_ORDERED;

                            //Check if it is the first customer in line
                            if (custInLine[0] == custInStore[i])
                            {
                                //Set the take order button above the customer
                                takeOrderBtnRecs[NOT_HOVERING].Y = (int)custLocs[custInStore[i]].Y - takeOrderBtnRecs[NOT_HOVERING].Height;
                                takeOrderBtnRecs[HOVERING].Y = takeOrderBtnRecs[NOT_HOVERING].Y - HOVER_DISPLACEMENT;
                            }
                        }
                        break;
                }
            }

            //Update the sliding of the sub at uncooked
            if (subAtUncooked)
            {
                //If the sub is sliding, update the sub's position
                if (subStates[subIndexAtUncooked] == SLIDING_1)
                {
                    //Update the sliding of the sub
                    UpdateSliding(gameTime, UNCOOKED_COUNTER, ref subSlideSpeed1, ref uncookedBreadTrueLocX, subIndexAtUncooked);
                }
            }

            //Update the sliding of the sub at cooked
            if (subsAtCooked.Count > 0)
            {
                //If the sub is sliding, update the sub's position
                if (subStates[subIndexAtCooked] == SLIDING_2 && playState != CUST_TABLE)
                {
                    //Update the sliding of the sub
                    UpdateSliding(gameTime, COOKED_COUNTER, ref subSlideSpeed2, ref cookedBreadTrueLocX, subIndexAtCooked);
                }
            }

            //Update the sliding away at the toaster station
            UpdateToasterSlidingAway(gameTime);
            
            //Update the toaster time
            for (int i = 0; i < NUM_TOASTERS; i++)
            {
                //Update the toaster times
                if (toastersOccupancy[i] != -1)
                {
                    if (grabbedSubNum != toastersOccupancy[i])
                    {
                        //Update each toasting timer
                        toastingTimer[i].Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                        //Set the toasting bar rectangle as the max bar
                        if (toastingBarRecs[i][FULL_BAR].Height < toastingBarRecs[i][EMPTY_BAR].Height)
                        {
                            //Increase the height of the bar and update the y location of the full bar
                            toastingBarRecs[i][FULL_BAR].Height = (int)((toastingBarRecs[i][EMPTY_BAR].Bottom - perfToastLineRecs[i].Y) * toastingTimer[i].GetTimePassed() / perfectToastTime);
                            toastingBarRecs[i][FULL_BAR].Y = toastingBarRecs[i][EMPTY_BAR].Bottom - toastingBarRecs[i][FULL_BAR].Height;
                        }

                        //Play the alarm if the timer was purchased and if the time is up
                        if (toastingTimer[i].GetTimePassed() >= perfectToastTime && itemIsPurchased[TIMER_TOP + i] && timerAlarms[i].State != SoundState.Playing)
                        {
                            //Play the timer sound
                            timerAlarms[i].Play();
                        }
                    }
                }
            }

            //Update the play state depending on which location the player is in
            switch (playState)
            {
                case STORE_FRONT:
                    //Update the store front
                    UpdateStoreFront(gameTime);
                    break;

                case UNCOOKED_COUNTER:
                    //Update the uncooked counter station
                    UpdateUncookedCounter(gameTime);
                    break;

                case TOASTER_STATION:
                    //Update the toaster station
                    UpdateToaster(gameTime);
                    break;

                case COOKED_COUNTER:
                    //Update the cooked counter station
                    UpdateCookedCounter(gameTime);
                    break;

                case CUST_TABLE:
                    //Udate the customer's table for evaluation of the sub
                    UpdateCustTable(gameTime);
                    break;

                case TAKING_ORDER:
                    //Update the taking order state
                    UpdateTakingOrder(gameTime);
                    break;

                case CHECKING_ORDER:
                    //Update the checking orders page
                    UpdateCheckingOrder();
                    break;
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update the pre-pregame state
        private void UpdatePrePregame()
        {
            //Set whether the mouse is hovering over the start button
            hoverOverStartBtn = startBtnRecs[NOT_HOVERING].Contains(mouse.Position);

            //Start the game if the user clicked on the start button
            if (hoverOverStartBtn)
            {
                //Change the state to pre game when the start button is clicked on
                if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                {
                    //Play the opening sound
                    openingJingle.CreateInstance().Play();

                    //Change the game state pregame and set the pregame animation as animating
                    gameState = PREGAME;
                    openSignAnim.isAnimating = true;

                    //Play the button click sound effect 
                    btnClickSnd.CreateInstance().Play();
                }
            }
        }

        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update the pregame state by updating the open sign flip and reseting and preparing for the round
        private void UpdatePregame(GameTime gameTime)
        {
            //Update the open sign
            openSignAnim.Update(gameTime);
            
            //Change the state to play when the sign animation is done
            if (!openSignAnim.isAnimating)
            {
                //Change the game state to play and the play state to the storefront
                gameState = PLAY;
                playState = STORE_FRONT;

                //Set the total number of customers in the day
                if (day + MIN_CUSTOMERS - 1 <= MAX_CUSTOMERS)
                {
                    //Set the number of customers as the day number plus the minimum number of customers
                    totalNumCust = day + MIN_CUSTOMERS - 1;
                }
                else
                {
                    //Set the number of customers to the maximum number
                    totalNumCust = MAX_CUSTOMERS;
                }

                //Set the day's tips to 0 and the points to 0
                dayTips = 0;
                dayPoints = 0;

                //Set the length of the customer appearances and wait timers arrays and the customer pool
                custAppearances = new int[totalNumCust];
                custWaitTimers = new Timer[totalNumCust];

                //Set the array of scores
                orderScores = new double[totalNumCust][];
                
                //Set each customer wait timer as an infinite timer and each order's array of scores as having 4 scores
                for (int i = 0; i < totalNumCust; i++)
                {
                    //Set the customer wait timers and the order scores
                    custWaitTimers[i] = new Timer(Timer.INFINITE_TIMER, false);
                    orderScores[i] = new double[NUM_SCORES];
                }

                //Set the orders completed as 0
                ordersCompleted = 0;

                //Reset the orders as nothing 
                breadOrders.Clear();
                meatOrders.Clear();
                cheeseOrders.Clear();
                veggieOrders.Clear();
                sauceOrders.Clear();

                //Reset the meat, veggies and sauce notepad rectangles
                meatRecordRecs.Clear();
                veggieRecordRecs.Clear();
                sauceRecordRecs.Clear();

                //Randomize the appearance of the customers in line
                RandomizeCustomers();

                //Set the orders of each customer
                for (int i = 0; i < totalNumCust; i++)
                {
                    //Randomize the bread choices and meat and cheese choices
                    breadOrders.Add(rng.Next(0, NUM_BREAD_CHOICES));
                    meatOrders.Add(rng.Next(0, NUM_MEAT_CHOICES));
                    cheeseOrders.Add(rng.Next(0, NUM_CHEESE_CHOICES));

                    //Set the width of the rectangles of the meat in the notepad for taking orders but out of the screen
                    meatRecordRecs.Add(new Rectangle((int)(screenWidth / 2 - meatImgs[meatOrders[i]].Width * picturesNoteHeight / meatImgs[meatOrders[i]].Height / 2.0),
                                                     screenHeight,
                                                     (int)((float)meatImgs[meatOrders[i]].Width * picturesNoteHeight / meatImgs[meatOrders[i]].Height),
                                                     picturesNoteHeight));

                    //Randomize the number of different veggies and sauces for each sub order
                    veggieOrders.Add(new int[rng.Next(1, MAX_VEGGIES + 1)]);
                    sauceOrders.Add(new int[rng.Next(1, MAX_SAUCES + 1)]);

                    //Set the array size for the rectangles for the veggie and sauce that appear in the notepad for taking orders
                    veggieRecordRecs.Add(new Rectangle[veggieOrders[i].Length]);
                    sauceRecordRecs.Add(new Rectangle[sauceOrders[i].Length]);

                    //Randomize the sauce and veggie orders
                    RandomizeSauceOrVeggieOrder(NUM_VEGGIE_CHOICES, i, ref veggieOrders, veggieImgs, ref veggieRecordRecs, (int)(veggieNoteLoc.Y + writingFont.MeasureString(veggieNoteLabel).Y));
                    RandomizeSauceOrVeggieOrder(NUM_SAUCE_CHOICES, i, ref sauceOrders, sauceTopImgs, ref sauceRecordRecs, (int)(sauceNoteLoc.Y + writingFont.MeasureString(sauceNoteLabel).Y));
                    
                    //Set the total number of subs that were started at 0
                    subsStarted = 0;
                }

                //Reset the next order number
                nextOrderNum = 1;

                //Set the customer entrance delay timer
                custEntranceTimer = new Timer(rng.Next((int)MIN_CUST_ENTRANCE_DELAY, (int)MAX_CUST_ENTRANCE_DELAY + 1), false);

                //Set the first customer as in the store
                custInStore.Add(custAppearances[0]);
                custInLine.Add(custAppearances[0]);
                numCustAppeared = 1;
                custStates[custAppearances[0]] = WALKING_IN;

                //Play the doorbell sound if it is purchased
                if (itemIsPurchased[DOORBELL])
                {
                    //Play the doorbell
                    doorbellSnd.CreateInstance().Play();
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update the instructions by switching the instruction slides
        private void UpdateInstructions()
        {
            //Switch the slide if the A or D key is pressed
            if (kb.IsKeyDown(Keys.A) && !prevKb.IsKeyDown(Keys.A) && curInstrSlideView > 0)
            {
                //Decrease the slide number
                curInstrSlideView--;
            }
            else if (kb.IsKeyDown(Keys.D) && !prevKb.IsKeyDown(Keys.D))
            {
                //Increase the slide number
                curInstrSlideView++;

                //Change the state back to the menu if all slides have been gone through
                if (curInstrSlideView >= instrSlides.Length)
                {
                    //Change the side number back to 0 and the game state back to menu
                    curInstrSlideView = 0;
                    gameState = MENU;
                }
            }
        }

        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update the pre-endgame state by updating the sign flip animation and calculating the average scores
        private void UpdatePreEndGame(GameTime gameTime)
        {
            //Update the closed sign animation
            closedSignAnim.Update(gameTime);

            //Fade out to the end game if the sign is no longer animating
            if (!closedSignAnim.isAnimating)
            {
                //Fade out the sign
                flipSignTransparency = FadeOut(flipSignTransparency, gameTime);

                if (flipSignTransparency <= 0)
                {
                    //Clear the average scores from the previous round
                    avgScores = new double[NUM_SCORES];

                    //Change the game state to end game
                    gameState = ENDGAME;

                    //Calculate the average for each type of score
                    for (int i = 0; i < NUM_SCORES; i++)
                    {
                        //Find the total sum of all the scores for each type of score
                        for (int j = 0; j < totalNumCust; j++)
                        {
                            //Find the sum of the average score
                            avgScores[i] += orderScores[j][i];
                        }

                        //Calculate the average score, set the score texts and locations
                        avgScores[i] = avgScores[i] / totalNumCust;
                        scoreTexts[i] = Math.Round(avgScores[i], 2) * 100 + "%";

                        //Calculate the intended pointer rotation and reset the scales
                        intendedPtrRotations[i] = (float)(avgScores[i] * Math.PI);
                        ResetScoreScales(i);
                    }

                    //Calculate the final average score, set the text, and set the location
                    finalAvgScore = avgScores.Sum() / NUM_SCORES;
                    finalAvgScoreText = "Average: " + Math.Round(finalAvgScore, 2) * 100 + "%";
                    finalAvgScoreLoc.X = screenWidth / 2 - scoreFont.MeasureString(finalAvgScoreText).X / 2;

                    //Set the points text and locations to the side of the screen
                    pointsText = dayPoints + " pts";
                    ptsLoc.X = screenWidth / 2 - scoreFont.MeasureString(pointsText).X / 2;

                    //Set the tips text and its location
                    tipsText = RealignMoneyDecimals(dayTips);
                    tipsLoc.X = screenWidth / 2 - scoreFont.MeasureString(tipsText).X / 2;

                    //Set the total points, the text and the location
                    totalPts += dayPoints;
                    totalPtsText = "Total XP: " + totalPts;
                    totalPtsLoc.X = screenWidth - scoreFont.MeasureString(totalPtsText).X - HORIZ_SCORE_SPACER;

                    //Set the total tips and text
                    totalTips += dayTips;
                    totalTipsText = "Bank: " + RealignMoneyDecimals(totalTips);
                    totalTipsLoc.X = screenWidth - scoreFont.MeasureString(totalTipsText).X - HORIZ_SCORE_SPACER;

                    //Play the background music for the end game
                    MediaPlayer.Play(endGameMusic);
                    MediaPlayer.Volume = 1f;
                }
            }
        }

        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update the endgame state by updating the score scales and checking for a mouse click on the menu button
        private void UpdateEndGame(GameTime gameTime)
        {
            //Play the endgame music
            if (MediaPlayer.State != MediaState.Playing)
            {
                //Play the end game music
                MediaPlayer.Play(endGameMusic);
            }

            //Set whether the mouse is hovering over the menu button
            hoverOverMenuBtn = menuBtnRecs[NOT_HOVERING].Contains(mouse.Position);

            //Change the game state to the menu if the mouse pressed the menu button
            if (hoverOverMenuBtn && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                //Change the game state to menu
                gameState = MENU;

                //Play the button click sound effect 
                btnClickSnd.CreateInstance().Play();

                //Stop the music
                MediaPlayer.Stop();
            }

            //Update the score scales
            UpdateScoreScales(gameTime);
        }

        //Pre: None
        //Post: None
        //Desc: Update the shop by allowing the player to purchase upgrades
        private void UpdateShop()
        {
            //Play the menu music
            if (MediaPlayer.State != MediaState.Playing)
            {
                //Play the menu music
                MediaPlayer.Play(menuMusic);
            }

            //Set the hovering over the menu button 
            hoverOverMenuBtn = menuBtnRecs[NOT_HOVERING].Contains(mouse.Position);
            
            //Change the game state to the menu if the mouse pressed the menu button
            if (hoverOverMenuBtn && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                //Change the game state to menu
                gameState = MENU;

                //Play the button click sound effect 
                btnClickSnd.CreateInstance().Play();
            }

            //Check for a mouse click on each of the items
            for (int i = 0; i < itemRecs.Length; i++)
            {
                //Display the item in the description box
                if (itemRecs[i].Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                {
                    //Play the select sound effect
                    selectSnd.CreateInstance().Play();

                    //Set the current item in view
                    curItemView = i;

                    //Set the location of the item title and description
                    itemTitleLoc.X = descRec.X + descRec.Width / 2 - descFont.MeasureString(itemNames[curItemView]).X / 2;
                    itemDescLoc.X = descRec.X + descRec.Width / 2 - descFont.MeasureString(itemDescs[curItemView]).X / 2;

                    //Set the price text
                    priceText = "$" + itemPrices[curItemView];

                    //Set the location of the purchase button depending on if it is purchased yet
                    if (itemIsPurchased[i])
                    {
                        //Set the purchase button off screen
                        purchaseBtnRec.X = screenWidth;
                        priceLoc.X = screenWidth;
                    }
                    else if (purchaseBtnRec.X == screenWidth)
                    {
                        //Set the purchase button on screen
                        purchaseBtnRec.X = descRec.X;
                        priceLoc.X = descRec.X + descRec.Width / 2 - descFont.MeasureString(priceText).X / 2;
                    }
                }
            }

            //Check for a click on the purchase button if it is not purchased yet or if there is enough money
            if (!itemIsPurchased[curItemView] && totalTips > itemPrices[curItemView])
            {
                //Play the menu music
                if (MediaPlayer.State != MediaState.Playing)
                {
                    //Play the menu music
                    MediaPlayer.Play(menuMusic);
                }

                //Set the hover over the purchase button
                hoverOverPurchase = purchaseBtnRec.Contains(mouse.Position);

                //Check for a click on the purchase button
                if (hoverOverPurchase && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                {
                    //Play the purchase sound effect
                    purchaseSnd.CreateInstance().Play();

                    //Set the item as purchased
                    itemIsPurchased[curItemView] = true;

                    //Set the purchase button off screen
                    purchaseBtnRec.X = screenWidth;
                    priceLoc.X = screenWidth;

                    //Reduce the balance and set its text
                    totalTips -= itemPrices[curItemView];
                    totalTipsText = "Bank: " + RealignMoneyDecimals(totalTips);

                    //Increase the customer's patience if it is a poster
                    if (curItemView == POSTER_1)
                    {
                        //Set the max customer by the product of the customer's patience and the multiplier
                        maxCustPatience *= poster1Multiplier;
                    }
                    else if (curItemView == POSTER_2)
                    {
                        //Set the max customer by the product of the customer's patience and the multiplier
                        maxCustPatience *= poster2Multiplier;
                    }
                }
            }
        }

        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update the storefront by updating the customer walks allowing the player to take orders
        private void UpdateStoreFront(GameTime gameTime)
        {
            //Switch the player location in the Subway store or check orders
            SwitchPlaces();
            CheckOrders();

            //Set whether the mouse is hovering over the take order button
            hoverOverTakeOrder = takeOrderBtnRecs[NOT_HOVERING].Contains(mouse.Position);

            //Update each of the customer animations that are in the store
            for (int i = 0; i < custInStore.Count; i++)
            {
                // for those that are walking, Update the customer animations
                if (custStates[custInStore[i]] == WALKING_IN || custStates[custInStore[i]] == WALKING_TO_TABLE || custStates[custInStore[i]] == MOVING_UP)
                {
                    //Update the animation
                    customers[custInStore[i]].Update(gameTime);
                }
            }

            //Change the state to taking order if the taking order button is pressed
            if (hoverOverTakeOrder && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                //Play the button click and writing sound effect 
                writing = writingSnd.CreateInstance();
                writing.Play();
                btnClickSnd.CreateInstance().Play();
                

                //Change the state to taking orders and reset the taking order timer
                takingOrderTimer.ResetTimer(true);
                playState = TAKING_ORDER;

                //Set each ingredient as not shown
                breadShown = false;
                meatShown = false;
                cheeseShown = false;
                veggiesShown = false;
                sauceShown = false;

                //Set the bread and cheese record rectangles off screen
                breadRecordRec.Y = screenHeight;
                cheeseRecordRec.Y = screenHeight;
            }
        }

        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update the cooked counter for the player to choose their bread, cut it, add meat and cheese
        private void UpdateUncookedCounter(GameTime gameTime)
        {
            //Allow for switching places, checking orders or deleting the sub if not grabbing an ingredient or cutting the bread
            if (!grabbingUncookedIngr && !isCutting)
            {
                //Check for hovering over the garbage button
                hoveringOverTrash = uncookedGarbageBtnRec.Contains(mouse.Position);

                //Switch the player location in the Subway store or check orders
                SwitchPlaces();
                CheckOrders();
            }

            //Allow for a bread to be selected if there is no subs at the station
            if (!subAtUncooked)
            {
                //Check for a mouse click on each of the bread options
                for (int i = 0; i < NUM_BREAD_CHOICES; i++)
                {
                    //Set the mouse hovering over the bread options
                    hoverOverBreads[i] = breadOptionRecs[i][NOT_HOVERING].Contains(mouse.Position);

                    //Check for a mouse click on each of the breads if it is being hovered over
                    if (hoverOverBreads[i])
                    {
                        //Select that bread type if the mouse clicked on it
                        if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                        {
                            //Play the select sound effect 
                            selectSnd.CreateInstance().Play();

                            //Increase the number of subs started
                            subsStarted++;

                            //Set the bread that was chosen, set a bread at the uncooked station, and add a new sub state, new scores, and new ingredients
                            subAtUncooked = true;

                            //Set the current bread chosen 
                            curBreadChosen = i;
                            
                            //Add a new sub number, state, cut score, and toast score
                            subNums.Add(subsStarted);
                            subStates.Add(BREAD_CHOSEN);
                            cutScores.Add(0);
                            toastScores.Add(0);

                            //Set the sub index at the uncooked station
                            subIndexAtUncooked = subNums.Count - 1;

                            //Add a new value for the bread, meats, and cheeses in the list of subs that were created
                            subBreads.Add(curBreadChosen);
                            subMeats.Add(-1);
                            subCheeses.Add(-1);

                            //Add a new list of rectangle array with 3 elements for the top, bottom, and full bread rectangles
                            subBreadRecs.Add(new Rectangle[3]);
                            
                            //Copy the rectangles into the array of the bread rectangles for the current sub at the uncooked counter
                            subBreadRecs[subIndexAtUncooked][BOT_BUN] = buildingBreadRecs[BOT_BUN];
                            subBreadRecs[subIndexAtUncooked][TOP_BUN] = buildingBreadRecs[TOP_BUN];
                            subBreadRecs[subIndexAtUncooked][FULL_LOAF] = buildingBreadRecs[FULL_LOAF];

                            //Add a new list of rectangles for each new piece of meat and cheese to be added
                            meatPiecesRecs.Add(new List<Rectangle>());
                            cheesePiecesRecs.Add(new List<Rectangle>());

                            //Add a new list of horizontal distances between the left side of the bread and the left side of the ingredient
                            meatsBreadDists.Add(new Vector2[0]);
                            cheesesBreadDists.Add(new Vector2[0]);
                            
                            //Set the garbage button on screen
                            uncookedGarbageBtnRec.X = 0;
                        }
                    }
                }
            }
            else
            {
                //Update the sub only at the correct step it is in
                switch (subStates[subIndexAtUncooked])
                {
                    case BREAD_CHOSEN:
                        //Update bread chosen
                        UpdateBreadChosen();
                        break;

                    case CUTTING_BREAD:
                        //Update cutting the bread
                        UpdateCuttingBread();
                        break;

                    case CHOOSING_MEAT:
                        //Update choosing meat
                        ChooseIngredient(NUM_MEAT_CHOICES, meatContRecs, ref curMeatChosen, ref subMeats, subIndexAtUncooked, ref uncookedRemainPiecesText, ref uncookedRemainPiecesLoc, maxMeatPieces, ref meatsBreadDists, CHOOSING_MEAT);                      
                        break;

                    case ADDING_MEAT:
                        //Update adding meat by dragging and dropping the meat pieces from the container
                        DragAndDropIngredient(gameTime, buildingBreadRecs[BOT_BUN].Bottom - FIRST_LAYER_VERT_SPACER, ref meatPiecesRecs, ADDING_MEAT, ref grabbingUncookedIngr, ref uncookedIngrFalling, ref curUncookedIngrPieceRec, ref curUncookedIngrPieceLoc, ref meatsBreadDists, subIndexAtUncooked, ref uncookedRemainPiecesText, maxMeatPieces[curMeatChosen], ref uncookedIngrFallSpeed, curMeatChosen, meatContRecs, meatImgs);
                        break;

                    case CHOOSING_CHEESE:
                        //Update choosing cheese
                        ChooseIngredient(NUM_CHEESE_CHOICES, cheeseContRecs, ref curCheeseChosen, ref subCheeses, subIndexAtUncooked, ref uncookedRemainPiecesText, ref uncookedRemainPiecesLoc, maxCheesePieces, ref cheesesBreadDists, CHOOSING_CHEESE);
                        break;

                    case ADDING_CHEESE:
                        //Update adding cheese by dragging and dropping the cheese pieces from the container
                        DragAndDropIngredient(gameTime, buildingBreadRecs[BOT_BUN].Bottom - FIRST_LAYER_VERT_SPACER - CHEESE_VERT_SPACING, ref cheesePiecesRecs, ADDING_CHEESE, ref grabbingUncookedIngr, ref uncookedIngrFalling, ref curUncookedIngrPieceRec, ref curUncookedIngrPieceLoc, ref cheesesBreadDists, subIndexAtUncooked, ref uncookedRemainPiecesText, maxCheesePieces[curCheeseChosen], ref uncookedIngrFallSpeed, curCheeseChosen, cheeseContRecs, cheeseImgs);
                        break;
                        
                    case SLIDING_1:
                        //Set the sub rectangle location
                        subBreadRecs[subIndexAtUncooked][BOT_BUN].X = (int)uncookedBreadTrueLocX;
                        SetSubFillingRecsLocation(subNums[subIndexAtUncooked], 1f);
                        break;
                }

                //Remove the last sub if the trash button is pressed
                if (hoveringOverTrash && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                {
                    //Reset the knife if the state was cutting bread
                    if (subStates[subIndexAtUncooked] == CUTTING_BREAD)
                    {
                        //Reset the knfie
                        ResetKnife();
                    }

                    //Delete the latest sub
                    DeleteSub(subIndexAtUncooked);

                    //Set the uncooked station as not having any subs, no bread/meat/cheese chosen
                    subAtUncooked = false;
                    curBreadChosen = -1;
                    curMeatChosen = -1;
                    curCheeseChosen = -1;

                    //Set grabbing and falling both as false
                    grabbingUncookedIngr = false;
                    uncookedIngrFalling = false;

                    //Set the garbage button off screen
                    uncookedGarbageBtnRec.X = screenWidth;
                }
            }
        }

        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update the toaster station by allowing for subs to be dragged into and out of ovens and updating the toastiness of the subs
        private void UpdateToaster(GameTime gameTime)
        {
            //Allow for switching places and checking orders if a sub is not being grabbed
            if (!grabbingSub)
            {
                //Switch the player location in the Subway store or check orders
                SwitchPlaces();
                CheckOrders();
            }
            
            //Check for grabbing the sub if there are subs in line
            if (subsInLineAtToaster.Count > 0)
            {
                //Set the mouse as grabbing the sub if the sub in line is being pressed on
                if (subBreadRecs[subNums.IndexOf(subsInLineAtToaster[0])][BOT_BUN].Contains(mouse.Position))
                {
                    //Start moving the sub if the mouse clicked on it
                    if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                    {
                        //Play the select sound effect 
                        selectSnd.CreateInstance().Play();

                        //Set grabbing the sub as true, set the number of the sub that is being grabbed, and remove the sub from the line to the toaster
                        grabbingSub = true;
                        grabbedSubNum = subsInLineAtToaster[0];
                        subsInLineAtToaster.RemoveAt(0);
                    }
                }
            }
            
            //Check for grabbing the subs at each toaster
            for (int i = 0; i < NUM_TOASTERS; i++)
            {
                //Only grab the sub from the oven if the oven is occupied
                if (toastersOccupancy[i] != -1)
                {
                    //Grab the sub if the mouse clicked on the sub's bread
                    if (subBreadRecs[subNums.IndexOf(toastersOccupancy[i])][BOT_BUN].Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                    {
                        //Stop the alarm if it is playing
                        if (timerAlarms[i].State == SoundState.Playing)
                        {
                            //Stop the alarm sound
                            timerAlarms[i].Stop();
                        }

                        //Play the select sound effect
                        selectSnd.CreateInstance().Play();

                        //Set grabbing sub as true, set the grabbed sub number, and set the toasting score of the sub
                        grabbingSub = true;
                        grabbedSubNum = toastersOccupancy[i];
                        toastScores[subNums.IndexOf(toastersOccupancy[i])] = 1 - Math.Abs(perfectToastTime - toastingTimer[i].GetTimePassed()) / perfectToastTime;

                        //Set the score at 0 if the score is less than 0
                        if (toastScores[subNums.IndexOf(toastersOccupancy[i])] < 0)
                        {
                            //Set the score at 0
                            toastScores[subNums.IndexOf(toastersOccupancy[i])] = 0;
                        }
                    }
                }
            }

            //Update the sub's location depending on if it is grabbed or no longer grabbed 
            if (grabbingSub)
            {
                //Update the sub's location if the mouse is still pressed down
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    //Move the sub where the mouse is
                    subBreadRecs[subNums.IndexOf(grabbedSubNum)][BOT_BUN] = GrabIngrPiece(subBreadRecs[subNums.IndexOf(grabbedSubNum)][BOT_BUN]);
                    SetSubFillingRecsLocation(grabbedSubNum, subScalerAtToaster);
                }
                else
                {
                    //Move the sub to where it is dragged to, depending on if it came from the line to the toaster or from the toaster
                    if (subStates[subNums.IndexOf(grabbedSubNum)] == IN_LINE_TOASTER)
                    {
                        //Put the sub in the toaster if its bread rectangle is partially in the toaster rectangle. Otherwise, put it back at the front of the line
                        if (toasterRecs[TOP_TOASTER].Intersects(subBreadRecs[subNums.IndexOf(grabbedSubNum)][BOT_BUN]) && toastersOccupancy[TOP_TOASTER] == -1)
                        {
                            //Play the oven start sound effect and set the timer alarm sound as a new sound effect instance
                            ovenStartSnd.CreateInstance().Play();
                            timerAlarms[TOP_TOASTER] = timerAlarmSnd.CreateInstance();

                            //Set the location of the sub in the top toaster
                            subBreadRecs[subNums.IndexOf(grabbedSubNum)][BOT_BUN] = breadToastingRec[TOP_TOASTER];
                            SetSubFillingRecsLocation(grabbedSubNum, subScalerAtToaster);

                            //Remove that sub from the line at the toaster and set the toaster as occupied
                            toastersOccupancy[TOP_TOASTER] = grabbedSubNum;
                            subStates[subNums.IndexOf(toastersOccupancy[TOP_TOASTER])] = ENTERED_TOASTER;
                            toastingTimer[TOP_TOASTER].ResetTimer(true);
                        }
                        else if (toasterRecs[BOT_TOASTER].Contains(subBreadRecs[subNums.IndexOf(grabbedSubNum)][BOT_BUN]) && toastersOccupancy[BOT_TOASTER] == -1)
                        {
                            //Play the oven start sound effect and set the timer alarm sound as a new sound effect instance
                            ovenStartSnd.CreateInstance().Play();
                            timerAlarms[BOT_TOASTER] = timerAlarmSnd.CreateInstance();

                            //Set the location of the sub in the bottom toaster
                            subBreadRecs[subNums.IndexOf(grabbedSubNum)][BOT_BUN] = breadToastingRec[BOT_TOASTER];
                            SetSubFillingRecsLocation(grabbedSubNum, subScalerAtToaster);

                            //Remove that sub from the line at the toaster and set the toaster as occupied                            
                            toastersOccupancy[BOT_TOASTER] = grabbedSubNum;
                            subStates[subNums.IndexOf(toastersOccupancy[BOT_TOASTER])] = ENTERED_TOASTER;
                            toastingTimer[BOT_TOASTER].ResetTimer(true);
                        }
                        else
                        {
                            //Add the sub back to the front of the line
                            subsInLineAtToaster.Insert(0, grabbedSubNum);

                            //Set the sub rectangle back to the line
                            subBreadRecs[subNums.IndexOf(grabbedSubNum)][BOT_BUN] = breadInLineToasterRec;
                            SetSubFillingRecsLocation(grabbedSubNum, subScalerAtToaster);
                        }
                    }
                    else if (subStates[subNums.IndexOf(grabbedSubNum)] == ENTERED_TOASTER)
                    {
                        //Move the sub to the away line or back to the toaster depending on if the sub intersets the right arrow
                        if (rightArrowRec.Intersects(subBreadRecs[subNums.IndexOf(grabbedSubNum)][BOT_BUN]))
                        {
                            //Play the sliding sound effect
                            slideSnd.CreateInstance().Play();

                            //Set the sub state as sliding and add it to the subs at sliding away and add its slide speed
                            subsAwayLineToaster.Add(grabbedSubNum);
                            smallSubSlideSpeed.Add(0f);
                            
                            //Set the sub location
                            subBreadRecs[subNums.IndexOf(grabbedSubNum)][BOT_BUN] = breadAwayLineToasterRec;
                            SetSubFillingRecsLocation(grabbedSubNum, subScalerAtToaster);

                            //Set the bread true location
                            toastedBreadTrueLocsX.Add(breadAwayLineToasterRec.X);

                            //Determine which toaster toasted the grabbed sub, set it as unoccupied and reset the toastiness bar
                            for (int i = 0; i < NUM_TOASTERS; i++)
                            {
                                //Set the toaster that the bread was pulled out from as not occupied
                                if (toastersOccupancy[i] == grabbedSubNum)
                                {
                                    //Set the toaster as not occupied and reset the bar height to 0
                                    toastersOccupancy[i] = -1;
                                    toastingBarRecs[i][FULL_BAR].Height = 0;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //Determine which toaster the sub was pulled from and move the sub back in it
                            for (int i = 0; i < NUM_TOASTERS; i++)
                            {
                                //Set the location of the bread back in the toaster
                                if (toastersOccupancy[i] == grabbedSubNum)
                                {
                                    //Set the location of the sub back in the toaster
                                    subBreadRecs[subNums.IndexOf(grabbedSubNum)][BOT_BUN] = breadToastingRec[i];
                                    SetSubFillingRecsLocation(grabbedSubNum, subScalerAtToaster);
                                    break;
                                }
                            }
                        }
                    }

                    //Set the mouse as not grabbing the sub
                    grabbingSub = false;
                    grabbedSubNum = -1;
                }
            }
        }

        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update the cooked counter station by letting the player add veggies and sauce and choose the right order number
        private void UpdateCookedCounter(GameTime gameTime)
        {
            //Allow switching states or deleting the sub if an ingredient is not being added
            if (!grabbingCookedIngr && !sprinklingOrSqueezing)
            {
                //Switch the player location in the Subway store or check orders
                SwitchPlaces();
                CheckOrders();

                //Set the hovering over garbage button
                hoveringOverTrash = cookedGarbageBtnRec.Contains(mouse.Position);
            }

            //Update the sub building station as long as there is a sub at the station
            if (subsAtCooked.Count > 0)
            {
                //Set the first sub in line to be worked if it was in line previously, otherwise update the sub according to its steps
                if (subStates[subNums.IndexOf(subsAtCooked[0])] == IN_LINE_COOKED)
                {
                    //Set the garbage button rectangle 
                    cookedGarbageBtnRec.X = 0;

                    //Set the first sub in line as choosing veggies and its index as the first index at the cooked station
                    subIndexAtCooked = subNums.IndexOf(subsAtCooked[0]);
                    subStates[subIndexAtCooked] = CHOOSING_VEGGIES;

                    //Set the rectangle of the sub's bread and toppings
                    subBreadRecs[subIndexAtCooked][BOT_BUN] = buildingBreadRecs[BOT_BUN];
                    SetSubFillingRecsLocation(subsAtCooked[0], 1f);

                    //Clear the list of veggies, lettuce rotations and sauces and their distances from the bread
                    subVeggies.Clear();
                    subSauces.Clear();
                    veggiePiecesRecs.Clear();
                    sauceDropRecs.Clear();
                    veggieRotations.Clear();
                    veggieBreadDists.Clear();
                    sauceBreadDists.Clear();

                    //Reset the next layer spacing
                    nextLayerSpacing = buildingBreadRecs[BOT_BUN].Bottom - FIRST_LAYER_VERT_SPACER - CHEESE_VERT_SPACING;
                }
                else
                {
                    //Update the sub according to its next step
                    switch (subStates[subIndexAtCooked])
                    {
                        case CHOOSING_VEGGIES:
                            //Check for click on the add sauce button if there is at least 1 veggie already added
                            if (subVeggies.Count >= 1)
                            {
                                //Update choosing sauce
                                UpdateChoosingSauce();
                            }

                            //Choose the veggie ingredient
                            ChooseIngredient(NUM_VEGGIE_CHOICES, veggieContRecs, ref curVeggieChosen, ref subVeggies, subIndexAtCooked, ref cookedRemainPiecesText, ref cookedRemainPiecesLoc, maxVeggiePieces, ref veggieBreadDists, CHOOSING_VEGGIES);
                            break;

                        case ADDING_VEGGIES:
                            //Add veggies by dragging and dropping or by sprinkling depending on if it is lettuce
                            if (curVeggieChosen != LETTUCE)
                            {
                                //Update dragging and dropping the veggie ingredient
                                DragAndDropIngredient(gameTime, nextLayerSpacing, ref veggiePiecesRecs, ADDING_VEGGIES, ref grabbingCookedIngr, ref cookedIngrFalling, ref curCookedIngrPieceRec, ref curCookedIngrPieceLoc, ref veggieBreadDists, subIndexAtCooked, ref cookedRemainPiecesText, maxVeggiePieces[curVeggieChosen], ref cookedIngrFallSpeed, curVeggieChosen, veggieContRecs, veggieImgs);
                            }
                            else
                            {
                                //Update sprinkling lettuce on the sub
                                UpdateLettuceOrSauceApplication(ADDING_VEGGIES, gameTime, maxVeggiePieces[LETTUCE], ref veggiePiecesRecs, ref scooperRec, lettucePerSec, veggieImgs, curVeggieChosen, lettuceSnd, ref veggieBreadDists);
                            }
                            break;

                        case CHOOSING_SAUCE:
                            //Update the choosing sauce
                            UpdateChoosingSauce();
                            break;

                        case ADDING_SAUCE:
                            //Update drizzling sauce on the sub
                            UpdateLettuceOrSauceApplication(ADDING_SAUCE, gameTime, maxSauceDrops, ref sauceDropRecs, ref sauceBottleSideRec, dropsPerSec, sauceDropImgs, curSauceChosen, sauceSqzSnd, ref sauceBreadDists);
                            break;

                        case ADDING_TOP_BUN:
                            //Update adding the top bun
                            UpdateAddingTopBun(gameTime);
                            break;

                        case SELECT_ORDER_NUM:
                            //Update selecting order number
                            UpdateSelectingOrderNum();
                            break;

                        case SLIDING_2:
                            //Set the sub rectangle location
                            subBreadRecs[subIndexAtCooked][BOT_BUN].X = (int)cookedBreadTrueLocX;
                            SetSubFillingRecsLocation(subNums[subIndexAtCooked], 1f);
                            subBreadRecs[subIndexAtCooked][TOP_BUN].X = (int)cookedBreadTrueLocX;
                            break;
                    }
                }
                
                //Remove the sub if the trash button is pressed
                if (hoveringOverTrash && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                {
                    //Delete the latest sub and remove it from the cooked station
                    DeleteSub(subIndexAtCooked);
                    subsAtCooked.RemoveAt(0);

                    //Remove the veggie and sauce drop selections, rectangles and bread distances and any rotation
                    subVeggies.Clear();
                    subSauces.Clear();
                    veggiePiecesRecs.Clear();
                    sauceDropRecs.Clear();
                    veggieBreadDists.Clear();
                    sauceBreadDists.Clear();
                    veggieRotations.Clear();

                    //Set the sprinkling bar off screen
                    RemoveSprinklingBar();

                    //Set the cooked station as not grabbing or falling
                    grabbingCookedIngr = false;
                    cookedIngrFalling = false;
                    sprinklingOrSqueezing = false;

                    //Set the finish button rectangle off screen
                    finishBtnRecs[NOT_HOVERING].Y = screenHeight;
                    finishBtnRecs[HOVERING].Y = screenHeight;

                    //Set the garbage button off the screen
                    cookedGarbageBtnRec.X = screenWidth;
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update the selecting order sub state by allowing the player to choose an order number to serve to and then calculating the score
        private void UpdateSelectingOrderNum()
        {
            //Check for a mouse click on each of the order number buttons
            for (int i = 0; i < availOrders.Count; i++)
            {
                //Set the hovering of the order number button
                hoverOverOrderNums[availOrders[i] - 1] = orderNumBtnRecs[availOrders[i] - 1][NOT_HOVERING].Contains(mouse.Position);

                //Select that order if the order number button is pressed
                if (hoverOverOrderNums[availOrders[i] - 1] && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                {
                    //Play the button click sound
                    btnClickSnd.CreateInstance().Play();

                    //Calculate the sub scores, points, and tips
                    CalculateSubScorePointsTips(availOrders[i] - 1);

                    //Set the text for the points and tips and their locations
                    pointsText = curPoints + " pts";
                    tipsText = "Tips: " + RealignMoneyDecimals(curTips);
                    ptsLoc.X = screenWidth - scoreFont.MeasureString(pointsText).X - HORIZ_SCORE_SPACER;
                    tipsLoc.X = screenWidth - scoreFont.MeasureString(tipsText).X - HORIZ_SCORE_SPACER;

                    //Calculate the intended rotation of each pointer, set each pointer rotation to 0, and set the score text and location
                    for (int j = 0; j < NUM_SCORES; j++)
                    {
                        //Set each intended and initial pointer rotation
                        intendedPtrRotations[j] = (float)(orderScores[availOrders[i] - 1][j] * Math.PI);
                        ResetScoreScales(j);

                        //Set the score text and the score text location
                        scoreTexts[j] = Math.Round(orderScores[availOrders[i] - 1][j] * 100) + "%";
                    }

                    //Set the state as sliding away, the initial speed at 0, and the true location of the bread pieces
                    subStates[subIndexAtCooked] = SLIDING_2;
                    subSlideSpeed2 = 0f;
                    cookedBreadTrueLocX = buildingBreadRecs[BOT_BUN].X;

                    //Play the sliding sound 
                    slideSnd.CreateInstance().Play();

                    //Remove the order number, customer, and set the order selection options 
                    custInStore.RemoveAt(i);
                    availOrders.RemoveAt(i);
                    UpdateOrderSelectionChoices();

                    //Change the current order view to the last order if there are available orders at the moment
                    if (availOrders.Count > 0)
                    {
                        //Set the current order view
                        curOrderView = availOrders.Count - 1;
                    }
                    break;
                }
            }
        }
        
        //Pre: orderIndex is the index of an order number that is available
        //Post: None
        //Desc: Calculate and store the 4 order scores, the points and the tips earned
        private void CalculateSubScorePointsTips(int orderIndex)
        {
            //Set the waiting score depending on if it is less than the customer's maximum patience
            if (custWaitTimers[orderIndex].GetTimePassed() <= maxCustPatience)
            {
                //Set the wait score for the order as 1 and calculate the bonus wait points
                orderScores[orderIndex][WAIT_SCORE] = 1;
                bonusWaitPts = (int)((maxCustPatience - custWaitTimers[orderIndex].GetTimePassed()) / MILLISEC_PER_SEC * BONUS_WAIT_POINTS);
            }
            else
            {
                //Set the wait score as the amount of time over the customer's max patience subtracted from 1
                orderScores[orderIndex][WAIT_SCORE] = 1 - (custWaitTimers[orderIndex].GetTimePassed() - maxCustPatience) / maxCustPatience;

                //Set the wait score at 0 if it is less than 0
                if (orderScores[orderIndex][WAIT_SCORE] < 0)
                {
                    //Set the wait score to 0
                    orderScores[orderIndex][WAIT_SCORE] = 0;
                }

                //Set the bonus points to 0
                bonusWaitPts = 0;
            }

            //Recalculate the bread score as its own weighting while the bread choice is 0 for its weighting
            orderScores[orderIndex][CUT_SCORE] = PLACEMENT_WEIGHTING * cutScores[subIndexAtCooked] + Convert.ToInt32(subBreads[subIndexAtCooked] == breadOrders[orderIndex]) * INGR_CHOICE_WEIGHTING;

            //Set the toasting score
            orderScores[orderIndex][TOAST_SCORE] = toastScores[subIndexAtCooked];

            //Calculate the meat and cheese placement score
            meatScore = CalculateNormalPlacementScore(meatsBreadDists[subIndexAtCooked], maxMeatPieces[subMeats[subIndexAtCooked]], meatImgs[subMeats[subIndexAtCooked]].Width);
            cheeseScore = CalculateNormalPlacementScore(cheesesBreadDists[subIndexAtCooked], maxCheesePieces[subCheeses[subIndexAtCooked]], cheeseImgs[subCheeses[subIndexAtCooked]].Width);

            //Recalculate the meat and cheese score with the proper weighting of the placement score and the choice weighting
            meatScore = meatScore * PLACEMENT_WEIGHTING + Convert.ToInt32(subMeats[subIndexAtCooked] == meatOrders[orderIndex]) * INGR_CHOICE_WEIGHTING;
            cheeseScore = cheeseScore * PLACEMENT_WEIGHTING + Convert.ToInt32(subCheeses[subIndexAtCooked] == cheeseOrders[orderIndex]) * INGR_CHOICE_WEIGHTING;

            //Set the array length of the placement scores as the amount of veggie types added
            veggiePlacementScores = new double[subVeggies.Count];

            //Calculate the veggie placement score for each vegetable used
            for (int j = 0; j < subVeggies.Count; j++)
            {
                //Calculate the vegetable placement score depending on the vegetable type
                if (subVeggies[j] == LETTUCE)
                {
                    //Calculate the placement score for lettuce
                    veggiePlacementScores[j] = CalculateSauceOrLettucePlacementScore(veggieBreadDists[j], perfectLettuceAmnt);
                }
                else
                {
                    //Calculate the regular placement score
                    veggiePlacementScores[j] = CalculateNormalPlacementScore(veggieBreadDists[j], maxVeggiePieces[subVeggies[j]], veggieImgs[subVeggies[j]].Width);
                }
            }

            //Calculate the veggie correctness
            veggieCorrectnessScore = CalculateIngrCorrectness(MAX_VEGGIES, veggieOrders[orderIndex], subVeggies.ToArray());

            //Calculate the final veggie score
            finalVeggieScore = veggiePlacementScores.Average() * PLACEMENT_WEIGHTING + veggieCorrectnessScore * INGR_CHOICE_WEIGHTING;
            
            //Set the array length of the sauce placement scores as the amount of sauce types added
            saucePlacementScores = new double[subSauces.Count];

            //Calculate the sauce evenness for the subs
            for (int j = 0; j < subSauces.Count; j++)
            {
                //Calculate the sauce placement
                saucePlacementScores[j] = CalculateSauceOrLettucePlacementScore(sauceBreadDists[j], perfectSauceAmnt);
            }

            //Calculate the sauce correctness
            sauceCorrectnessScore = CalculateIngrCorrectness(MAX_SAUCES, sauceOrders[orderIndex], subSauces.ToArray());

            //Calculate the final sauce score
            finalSauceScore = PLACEMENT_WEIGHTING * saucePlacementScores.Average() + INGR_CHOICE_WEIGHTING * sauceCorrectnessScore;

            //Calculate the final topping score
            orderScores[orderIndex][TOPPING_SCORE] = (meatScore + cheeseScore + finalVeggieScore + finalSauceScore) / 4;

            //Calculate the average score for the current sub and the max score possible as the base score plus each additional topping, the current points, and the day's points
            curAvgSubScore = orderScores[orderIndex].Average();
            orderMaxPts = MAX_BASE_SCORE + ADDITIONAL_TOPPING_PTS * (subVeggies.Count + subSauces.Count - 2);
            curPoints = (int)(curAvgSubScore * orderMaxPts) + bonusWaitPts;
            dayPoints += curPoints;

            //Calculate the tips earned and add it to the day's tips
            curTips = Math.Round(TIPS_MULTIPLIER * curPoints / orderMaxPts, 2);
            dayTips += curTips;
        }

        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update adding the top bun by moving the bread down and changing the state when it is in its landing position
        private void UpdateAddingTopBun(GameTime gameTime)
        {
            //Move the top bun as long as it is well above the toppings
            if (curCookedIngrPieceLoc.Y < screenHeight - SUB_BOTTOM_VERT_SPACER - FIRST_LAYER_VERT_SPACER - CHEESE_VERT_SPACING * (1 + subVeggies.Count) - buildingBreadRecs[TOP_BUN].Height)
            {
                //Drop the top bun
                DropIngredient(ref subBreadRecs[subIndexAtCooked][TOP_BUN], screenHeight - SUB_BOTTOM_VERT_SPACER - FIRST_LAYER_VERT_SPACER - CHEESE_VERT_SPACING * (1 + subVeggies.Count), ref curCookedIngrPieceLoc, ref cookedIngrFallSpeed, gameTime);
            }
            else
            {
                //Remove the garbage button from the screen
                cookedGarbageBtnRec.X = -cookedGarbageBtnRec.Width;

                //Set the select order pop up rectangle
                orderSelectionRec.Y = screenHeight / 2 - orderSelectionRec.Height / 2;

                //Set the order number buttons on the screen
                for (int i = 0; i < availOrders.Count; i++)
                {
                    //set the order number button y location on the screen
                    orderNumBtnRecs[availOrders[i] - 1][NOT_HOVERING].Y = orderSelectionRec.Y + ORDER_SELECTION_TOP_SPACER;
                    orderNumBtnRecs[availOrders[i] - 1][HOVERING].Y = orderSelectionRec.Y + ORDER_SELECTION_TOP_SPACER - HOVER_DISPLACEMENT;
                }

                //Set the sub state to selecting the order number
                subStates[subIndexAtCooked] = SELECT_ORDER_NUM;

                //Set the top bun at the right location
                subBreadRecs[subIndexAtCooked][TOP_BUN].Y = screenHeight - SUB_BOTTOM_VERT_SPACER - FIRST_LAYER_VERT_SPACER - CHEESE_VERT_SPACING * (1 + subVeggies.Count) - buildingBreadRecs[TOP_BUN].Height;

                //Play the plop sound
                plopSnd.Play();
            }
        }

        //Pre: gameTime is the game time of the game and positive, step is either at adding sauce or adding veggies, ingrPiecesRecs is a list of either veggies or sauce drop rectangles, toolRec is either the scooper or the side bottle rectangle,
        //     piecesPerSec is a positive integer value of lettuce shreds or sauce drops that comes out of the tool per second, ingrImgs is either sauce drops or veggie images, curIngrChosen is the type of sauce or veggie chosen and less than the
        //     max types of the sauce or veggies, ingrSndEffect is the type of sound effect from the lettuce or the sauce bottle, ingrBreadDists is a list of arrays of the distances between each sauce/lettuce piece and the top left corner of the bread.
        //Post: None
        //Desc: Update the application of lettuce or sauce by moving each piece down to the correct landing position
        private void UpdateLettuceOrSauceApplication(int step, GameTime gameTime, int maxAmnt, ref List<List<Rectangle>> ingrPiecesRecs, ref Rectangle toolRec, int piecesPerSec, Texture2D[] ingrImgs, int curIngrChosen, SoundEffect ingrSndEffect, ref List<Vector2[]> ingrBreadDists)
        {
            //Store the fall speed
            float fallSpeed = 0;

            //Only add more pieces if the mouse is continously pressed
            if (sprinklingOrSqueezing)
            {
                //Move the tool as long as the mouse is pressed
                if (mouse.LeftButton == ButtonState.Pressed && ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count <= maxAmnt - wastedPieces)
                {
                    //Increase the all buffer time
                    fallBufferTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                    //Drag the tool
                    DragScooperOrBottle(ref toolRec);

                    //Add new pieces while the fall buffer time is greater than the time in between piece release times
                    while (fallBufferTime > (double)MILLISEC_PER_SEC / piecesPerSec)
                    {
                        //Add a new sauce drop
                        ingrPiecesRecs[ingrPiecesRecs.Count - 1].Add(new Rectangle(toolRec.Center.X, toolRec.Bottom, ingrImgs[curIngrChosen].Width, ingrImgs[curIngrChosen].Height));
                        lettuceOrDropFalling.Add(true);
                        lettuceOrDropTrueLocs.Add(ingrPiecesRecs[ingrPiecesRecs.Count - 1][ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count - 1].Y);
                        lettuceOrSauceFallLocs.Add(ingrPiecesRecs[ingrPiecesRecs.Count - 1][ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count - 1].Y - toolRec.Height - TOP_SCREEN_SPACE + buildingBreadRecs[BOT_BUN].Y - ingrPiecesRecs[ingrPiecesRecs.Count - 1][ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count - 1].Height);

                        //Add a new lettuce speed and veggie rotation if the step is adding veggies
                        if (step == ADDING_VEGGIES)
                        {
                            //Add a new lettuce speed and a new veggie rotation
                            lettuceSpeeds.Add(0);
                            veggieRotations[subVeggies.Count - 1].Add(rng.Next((int)(Math.Round(Math.PI, 2) * 2 * NUM_DIFF_LETTUCE_ROTATIONS)));
                        }

                        //Reduce the fall buffer time
                        fallBufferTime -= (double)MILLISEC_PER_SEC / piecesPerSec;

                        //Update the sprinkling bar
                        sprinklingBarRecs[FULL_BAR].Width = (int)(sprinklingBarRecs[EMPTY_BAR].Width * (ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count + wastedPieces) / (double)maxAmnt);
                    }
                }
                else
                {
                    //Set squeezing/sprinkling as false
                    sprinklingOrSqueezing = false;

                    //Stop the sound effect
                    lettuceOrSauceSnd.Stop();

                    //Remove the tool from the screen
                    toolRec.X = screenWidth;
                    
                    //Set the sauce bottle top view back in the container if the step was adding sauce
                    if (step == ADDING_SAUCE)
                    {
                        //Set the sauce bottle top rec into the sauce container
                        sauceBottleTopsRecs[subSauces[subSauces.Count - 1]].X = SAUCE_CONT_HORIZ_EDGE_SPACER + sauceContRec.X + subSauces[subSauces.Count - 1] % 2 * sauceTopImgs[BBQ].Width;
                    }
                }
            }
            else if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed && toolRec.Contains(mouse.Position))
            {
                //Set squeezing/sprinkling as true
                sprinklingOrSqueezing = true;

                //Play the ingredient sound
                lettuceOrSauceSnd = ingrSndEffect.CreateInstance();
                lettuceOrSauceSnd.Play();
            }
            else if (!lettuceOrDropFalling.Contains(true) && lettuceOrDropFalling.Count > 0)
            {
                //Remove the sprinkling bar
                RemoveSprinklingBar();

                //Set the ingredient to bread distances array length
                ingrBreadDists[ingrBreadDists.Count - 1] = new Vector2[ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count];

                //Set each ingredient piece to bread distances for the current ingredient used
                for (int i = 0; i < ingrBreadDists[ingrBreadDists.Count - 1].Length; i++)
                {
                    //Set the ingredient piece to bread distances
                    ingrBreadDists[ingrBreadDists.Count - 1][i] = new Vector2(ingrPiecesRecs[ingrPiecesRecs.Count - 1][i].X - buildingBreadRecs[BOT_BUN].X,
                                                                              ingrPiecesRecs[ingrPiecesRecs.Count - 1][i].Y - buildingBreadRecs[BOT_BUN].Y);
                }

                //Change the state depending if the step is currently adding sauce or adding veggies
                if (step == ADDING_SAUCE)
                {
                    //Change the state depending on if the number of sauces that were added is at the max number of sauces
                    if (ingrBreadDists.Count >= MAX_SAUCES)
                    {
                        //Set the state as adding top bun
                        subStates[subIndexAtCooked] = ADDING_TOP_BUN;

                        //Set the fall speed at 0 initially and the top bun true location
                        cookedIngrFallSpeed = 0f;
                        curCookedIngrPieceLoc = buildingBreadRecs[TOP_BUN].Location.ToVector2();
                    }
                    else
                    {
                        //Set the state to choosing sauce and set the finish button on the screen
                        subStates[subIndexAtCooked] = CHOOSING_SAUCE;
                        finishBtnRecs[NOT_HOVERING].Y = screenHeight - finishBtn.Height - VERT_SPACER;
                        finishBtnRecs[HOVERING].Y = screenHeight - finishBtn.Height - VERT_SPACER - HOVER_DISPLACEMENT;
                    }
                }
                else
                {
                    //Change the state depending on if the number of types of veggies added is at the max
                    if (ingrBreadDists.Count < MAX_VEGGIES)
                    {
                        //Set the state of the sub as choosing vegetables
                        subStates[subIndexAtCooked] = CHOOSING_VEGGIES;
                    }
                    else
                    {
                        //Set the state of the sub as choosing sauce
                        subStates[subIndexAtCooked] = CHOOSING_SAUCE;
                    }
                }
            }

            //Set the fall speed to the max drop speed per update
            if (step == ADDING_SAUCE)
            {
                //Calculate the sauce drop speed in this update
                fallSpeed = (float)(maxDropSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }

            //Translate all pieces that are still falling
            for (int i = 0; i < lettuceOrDropFalling.Count; i++)
            {
                //Translate the piece if it is falling
                if (lettuceOrDropFalling[i])
                {
                    //Increase the current lettuce speed by its acceleration if the step is adding veggies
                    if (step == ADDING_VEGGIES)
                    {
                        //Increase the lettuce speed
                        lettuceSpeeds[i] += (float)((GRAVITY - AIR_RESISTANCE) * gameTime.ElapsedGameTime.TotalSeconds);
                        fallSpeed = lettuceSpeeds[i];
                    }

                    //Translate the sauce drop that is still falling
                    TranslateLettuceOrSauceDrop(i, ingrPiecesRecs[ingrPiecesRecs.Count - 1], fallSpeed, step);
                }
            }
        }

        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update the customer's table by updating the score scales and changing back to the previous state or the end of the game when the time is up at the customer's table
        private void UpdateCustTable(GameTime gameTime)
        {
            //Update the customer table timer
            custTableTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            //Update the pointers and the written scores
            UpdateScoreScales(gameTime);

            //Return back to the previous game state if the customer table timer is up or end the game if no customers remain
            if (custTableTimer.IsFinished())
            {
                //Increase orders completed
                ordersCompleted++;

                //Remove the first sub at the cooked counter
                subsAtCooked.RemoveAt(0);

                //End the game or return back to the game depending on if there are orders remaining
                if (ordersCompleted == totalNumCust)
                {
                    //Play the closing sound
                    closingJingle.CreateInstance().Play();

                    //Change the gamestate to the pre endgame and start the animation
                    gameState = PRE_ENDGAME;
                    closedSignAnim.isAnimating = true;
                    flipSignTransparency = 1f;

                    //Remove all the subs remaining
                    subStates.Clear();
                    cutScores.Clear();
                    toastScores.Clear();
                    subNums.Clear();
                    subBreads.Clear();
                    subMeats.Clear();
                    subCheeses.Clear();
                    subBreadRecs.Clear();
                    meatPiecesRecs.Clear();
                    cheesePiecesRecs.Clear();
                    meatsBreadDists.Clear();
                    cheesesBreadDists.Clear();

                    //Remove the veggie and sauce drop selections, rectangles and bread distances and any rotation
                    subVeggies.Clear();
                    subSauces.Clear();
                    veggiePiecesRecs.Clear();
                    sauceDropRecs.Clear();
                    veggieBreadDists.Clear();
                    sauceBreadDists.Clear();
                    veggieRotations.Clear();

                    //Set no ingredient as chosen
                    curMeatChosen = -1;
                    curBreadChosen = -1;
                    curCheeseChosen = -1;
                    
                    //Set no sub at any station and set grabbing as false
                    subAtUncooked = false;
                    grabbingUncookedIngr = false;
                    grabbingCookedIngr = false;
                    grabbingSub = false;
                    subsAtCooked.Clear();

                    //Remove the cooked and uncooked garbage buttons from the screen
                    uncookedGarbageBtnRec.X = -uncookedGarbageBtnRec.Width;
                    cookedGarbageBtnRec.X = -uncookedGarbageBtnRec.Width;

                    //Stop the music
                    MediaPlayer.Stop();
                }
                else
                {
                    //Remove the sub from the cooked station
                    DeleteSub(subIndexAtCooked);

                    //Remove the veggie and sauce drop selections, rectangles and bread distances and any rotation
                    subVeggies.Clear();
                    subSauces.Clear();
                    veggiePiecesRecs.Clear();
                    sauceDropRecs.Clear();
                    veggieBreadDists.Clear();
                    sauceBreadDists.Clear();
                    veggieRotations.Clear();

                    //Set the finish button rectangle off screen
                    finishBtnRecs[NOT_HOVERING].Y = screenHeight;
                    finishBtnRecs[HOVERING].Y = screenHeight;

                    //Set the play state to the cooked counter
                    playState = COOKED_COUNTER;
                }
            }
        }

        //Pre: gameTime is the game time of the game and positive
        //Post: None
        //Desc: Update taking order by displaying each ingredient when the time for them to pop up is ready
        private void UpdateTakingOrder(GameTime gameTime)
        { 
            //Update the taking order timer
            takingOrderTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            //Show each item when the timer is up
            if (takingOrderTimer.IsFinished())
            {
                //Reset the taking order timer
                takingOrderTimer.ResetTimer(true);

                //Show the next ingredient image that have not shown up
                if (!breadShown)
                {
                    //Set the bread record rectangle and set the bread as shown
                    breadRecordRec.Y = (int)(writingFont.MeasureString(breadNoteLabel).Y + breadNoteLoc.Y);
                    breadShown = true;
                }
                else if (!meatShown)
                {
                    //Set the meat record rectangle and set meat as shown
                    meatRecordRecs[nextOrderNum - 1] = new Rectangle(meatRecordRecs[nextOrderNum - 1].X,
                                                                     (int)(writingFont.MeasureString(meatNoteLabel).Y + meatNoteLoc.Y),
                                                                     meatRecordRecs[nextOrderNum - 1].Width,
                                                                     meatRecordRecs[nextOrderNum - 1].Height);
                    meatShown = true;
                }
                else if (!cheeseShown)
                {
                    //Set the cheese record rectangle and set the cheese as shown
                    cheeseRecordRec.Y = (int)(writingFont.MeasureString(cheeseNoteLabel).Y + cheeseNoteLoc.Y);
                    cheeseShown = true;
                }
                else if (!veggiesShown)
                {
                    //Set each of the veggie record rectangles
                    for (int i = 0; i < veggieOrders[nextOrderNum - 1].Length; i++)
                    {
                        //Set each of the veggie rectangles y location
                        veggieRecordRecs[nextOrderNum - 1][i].Y = (int)(writingFont.MeasureString(veggieNoteLabel).Y + veggieNoteLoc.Y);
                    }

                    //Set the veggies as shown
                    veggiesShown = true;
                }
                else if (!sauceShown)
                {
                    //Set each of the veggie record rectangles
                    for (int i = 0; i < sauceOrders[nextOrderNum - 1].Length; i++)
                    {
                        //Set each of the sauce rectangles y location
                        sauceRecordRecs[nextOrderNum - 1][i].Y = (int)(writingFont.MeasureString(sauceNoteLabel).Y + sauceNoteLoc.Y);
                    }

                    //Set the sauces as shown
                    sauceShown = true;
                }
                else
                {
                    //Stop the pencil sound
                    writing.Stop();

                    //Set the play state to the storefront
                    playState = STORE_FRONT;

                    //Set the current order for checking as the newest one and add this order to the orders available
                    availOrders.Add(nextOrderNum);
                    curOrderView = availOrders.IndexOf(nextOrderNum);

                    //Change the customer state to walking to the table
                    custStates[custInLine[0]] = WALKING_TO_TABLE;

                    //Change all other customers in line to their states as moving up in line
                    for (int i = 1; i < custInLine.Count; i++)
                    {
                        //Set each customer that was waiting in line as moving up
                        if (custStates[custInLine[i]] == NOT_ORDERED)
                        {
                            //Set the customer's state as moving up
                            custStates[custInLine[i]] = MOVING_UP;
                        }
                    }

                    //Update the order choices
                    UpdateOrderSelectionChoices();

                    //Remove the first customer from the line
                    custInLine.RemoveAt(0);

                    //Set the take order button out of the screen
                    takeOrderBtnRecs[NOT_HOVERING].Y = screenHeight;
                    takeOrderBtnRecs[HOVERING].Y = screenHeight;

                    //Increase the next order number
                    nextOrderNum++;
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update the checking order state by flipping through the order pages
        private void UpdateCheckingOrder()
        {
            //Show the next order when the D key is pressed
            if (kb.IsKeyDown(Keys.D) && !prevKb.IsKeyDown(Keys.D))
            {
                //Play the flipping sound if there is more than 1 available order
                if (availOrders.Count > 1)
                {
                    //Play the paper flipping sound
                    paperFlipSnd.CreateInstance().Play();
                }

                //Flip to the next order
                curOrderView++;

                //Change the currently viewing order number to the first one if the viewing number is greater than the count of the available orders
                if (curOrderView > availOrders.Count - 1)
                {
                    //Set the current order index for viewing at 0
                    curOrderView = 0;
                }
            }

            //Show the previous order when the A key is pressed
            if (kb.IsKeyDown(Keys.A) && !prevKb.IsKeyDown(Keys.A))
            {
                //Play the flipping sound if there is more than 1 available order
                if (availOrders.Count > 1)
                {
                    //Play the paper flipping sound
                    paperFlipSnd.CreateInstance().Play();
                }

                //Flip to the previous order
                curOrderView--;

                //Change the currently viewing order number to the last one if the viewing number is less than 0
                if (curOrderView < 0)
                {
                    //Set the current order index for viewing at the last index of the available orders list
                    curOrderView = availOrders.Count - 1;
                }
            }

            //Show change back to the storefront
            if (kb.IsKeyDown(Keys.S) && !prevKb.IsKeyDown(Keys.S))
            {
                //Play the step sound
                stepSnd.CreateInstance().Play();

                //Change the state to the previous state
                playState = prevPlayState;
            }
        }
        
        //Pre: None
        //Post: None
        //Desc: Update the bread chosen sub state by checking for a mouse click on the knife
        private void UpdateBreadChosen()
        {
            //Set whether the mouse is hovering over the knife
            hoverOverKnife = unrotatedKnifeRecs[NOT_HOVERING].Contains(mouse.Position);

            //Check for a mouse click on the knife if the mouse is hovering over it
            if (hoverOverKnife)
            {
                //Change the state to cutting bread when the mouse is pressed and when the play state is not changed at the same time
                if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed && playState == UNCOOKED_COUNTER)
                {
                    //Play the select sound effect 
                    selectSnd.CreateInstance().Play();

                    //Set the newest sub state as cutting bread
                    subStates[subIndexAtUncooked] = CUTTING_BREAD;

                    //Set the location of the rotated knife 
                    rotatedKnifeRecs[ROTATED - 1].X = buildingBreadRecs[FULL_LOAF].X;
                    rotatedKnifeRecs[ROTATED - 1].Y = buildingBreadRecs[FULL_LOAF].Y + perfectCuttingLoc;
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update cutting bread by moving the knife rectangle when the mouse is pressed and set the cut score for that sub
        private void UpdateCuttingBread()
        {
            //Continue to cut the bread as long as not all the bread is cut
            if (cuttingProgress < breadImgs[curBreadChosen][FULL_LOAF].Width)
            {
                //If the mouse stated pressing and the mouse's location is less than the cutting progress then start cutting
                if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed && mouse.X <= buildingBreadRecs[FULL_LOAF].X + cuttingProgress)
                {
                    //Set the knife as cutting
                    isCutting = true;
                }

                //Move the mouse 
                if (isCutting)
                {
                    //If the mouse is outside of the bread image, then keep the knife rectangle inside the bread image at its boudaries
                    if (mouse.Position.Y > buildingBreadRecs[FULL_LOAF].Bottom)
                    {
                        //Set the knife rectangle so its point is at at the bottom of the bread
                        rotatedKnifeRecs[KNIFE_CUTTING - 1].Y = buildingBreadRecs[FULL_LOAF].Bottom;
                    }
                    else if (mouse.Position.Y < buildingBreadRecs[FULL_LOAF].Y)
                    {
                        //Set the knife rectangle so its point is at the top of the bread
                        rotatedKnifeRecs[KNIFE_CUTTING - 1].Y = buildingBreadRecs[FULL_LOAF].Y;
                    }
                    else
                    {
                        //Set the knife rectangle at the mouse
                        rotatedKnifeRecs[KNIFE_CUTTING - 1].Y = mouse.Y;
                    }

                    //Set the knife rectangle with its point at the mouse's x position and always to the right of the bread
                    if (mouse.X < buildingBreadRecs[FULL_LOAF].X)
                    {
                        //Set the rotated knife rectangle at the start of the bread
                        rotatedKnifeRecs[KNIFE_CUTTING - 1].X = buildingBreadRecs[FULL_LOAF].X;
                    }
                    else
                    {
                        //Set the rotated knife rectangle at the mouse's x location
                        rotatedKnifeRecs[KNIFE_CUTTING - 1].X = mouse.X;
                    }


                    //Increase the progress and update the cutting score if the mouse was pressed and held while moving right of its previous cutting progress
                    if (mouse.Position.X > buildingBreadRecs[FULL_LOAF].X + cuttingProgress && mouse.LeftButton == ButtonState.Pressed)
                    {
                        //Set the new progress of how far the mouse moved horizontally in cutting the bread
                        cuttingProgress = mouse.Position.X - buildingBreadRecs[FULL_LOAF].X;

                        //If the cutting progress is at the next zone, calculate the cutting score
                        if (cuttingProgress >= curBreadZone * buildingBreadRecs[FULL_LOAF].Width / BREAD_ZONES)
                        {
                            //Calculate the knife distance from the perfect line
                            knifeDistFromPerfect = buildingBreadRecs[FULL_LOAF].Y + perfectCuttingLoc - rotatedKnifeRecs[KNIFE_CUTTING - 1].Y;

                            //Calculate the cutting bread score for the current zone depending on whether it is cut above or below the perfect line
                            if (knifeDistFromPerfect >= 0)
                            {
                                //Calcualte the score for when the knife is above the perfect cutting line
                                cuttingZoneScores[curBreadZone - 1] = 1 - (double)knifeDistFromPerfect / perfectCuttingLoc;
                            }
                            else
                            {
                                //Calcualte the score for when the knife is below the perfect cutting line
                                cuttingZoneScores[curBreadZone - 1] = (double)knifeDistFromPerfect / (buildingBreadRecs[FULL_LOAF].Height - perfectCuttingLoc) + 1;
                            }

                            //Increase the bread zone
                            curBreadZone++;
                        }
                    }
                    else if (mouse.LeftButton != ButtonState.Pressed)
                    {
                        //Set the mouse as not cutting
                        isCutting = false;

                        //Set the rotated but not cutting knife rectangle at the cutting location
                        rotatedKnifeRecs[ROTATED - 1].X = rotatedKnifeRecs[KNIFE_CUTTING - 1].X;
                        rotatedKnifeRecs[ROTATED - 1].Y = rotatedKnifeRecs[KNIFE_CUTTING - 1].Y;
                    }
                }
            }
            else
            {
                //Set the current bread zone as the maximum 
                curBreadZone = BREAD_ZONES;

                //Calculate the average score
                cutScores[subIndexAtUncooked] = cuttingZoneScores.Sum() / BREAD_ZONES;

                //Change the sub state to adding meat and reset the knife for the next sub
                subStates[subIndexAtUncooked] = CHOOSING_MEAT;
                ResetKnife();
            }
        }

        //Pre: gameTime is the game time of the game and positive, stoppingHeight is a positive integer value of the location for where the bottom of the ingredient rectangle will stop at, ingrPiecesRecs is a list of a list of rectangles for each of a specific type 
        //     of ingredient for each sub that was created, step is either the vallue of adding veggies, adding meat, or adding cheese, grabbingIngr is a bool of whether the ingredient is currently grabbed, curIngrPieceRec is a rectangle of the current ingredient piece
        //     that's either being grabbed or falling, curIngrPieceLoc is the positive integer value of the true location of the ingredient piece, ingrBreadDists is a list of arrays of the x and y distances from the top left corner of the bread, sub index is the index
        //     of the current bread, remainPiecesText is the text showing the number of pieces remaining to put on the bread left, maxPieces is the maximum number of pieces for that ingredient, ingrFallSpeed is the positive speed of the ingredient in that update, 
        //     curIngrChosen is the current ingredient that is chosen and is less than the maximum types of the veggie, meat, or cheese, ingrContRecs is the array of container rectangles, ingrImgs is the array of the ingredient images for meats, veggies or cheeses
        //Post: None
        //Desc: Move the ingredient at the mouse when the mouse is pressed on it and drop it onto the sub when the mouse is released
        private void DragAndDropIngredient(GameTime gameTime, int stoppingHeight, ref List<List<Rectangle>> ingrPiecesRecs, int step, ref bool grabbingIngr, ref bool ingrFalling, ref Rectangle curIngrPieceRec, ref Vector2 curIngrPieceLoc, ref List<Vector2[]> ingrBreadDists, int subIndex, ref string remainPiecesText, int maxPieces, ref float ingrFallSpeed, int curIngrChosen, Rectangle[] ingrContRecs, Texture2D[] ingrImgs)
        {
            //Move the piece of ingredient with the mouse if it is grabbed or move it towards its proper location on the sub if the mouse is released or grab a new piece of ingredient if the container is pressed
            if (grabbingIngr)
            {
                //Move the piece of ingredient with the mouse
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    //Set the position of the current ingredient being grabbed and set the meat ingredient location
                    curIngrPieceRec = GrabIngrPiece(curIngrPieceRec);
                    ingrPiecesRecs[ingrPiecesRecs.Count - 1][ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count - 1] = curIngrPieceRec;
                }
                else
                {
                    //Remove the piece of ingredient if it is too much below the bread or not balanced well on the bread or move the ingredient down by gravity if it is above the bread or set the ingredient location at its proper position if it is slightly below that stopping location
                    if (curIngrPieceRec.Center.X > buildingBreadRecs[BOT_BUN].Right || curIngrPieceRec.Center.X < buildingBreadRecs[BOT_BUN].X || curIngrPieceRec.Center.Y > stoppingHeight)
                    {
                        //Remove that piece of ingredient
                        ingrPiecesRecs[ingrPiecesRecs.Count - 1].RemoveAt(ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count - 1);
                    }
                    else
                    {
                        //Set the ingredient as falling
                        ingrFalling = true;

                        //Set the current ingredient piece location to its rectangle location
                        curIngrPieceLoc = curIngrPieceRec.Location.ToVector2();

                        //Set the initial speed of the ingredient fall speed
                        ingrFallSpeed = 0f;
                    }

                    //Set the ingredient as not being grabbed
                    grabbingIngr = false;
                }
            }
            else if (ingrFalling)
            {
                //Drop the ingredient and set the ingredient piece at the current ingredient rectangle
                DropIngredient(ref curIngrPieceRec, stoppingHeight, ref curIngrPieceLoc, ref ingrFallSpeed, gameTime);
                ingrPiecesRecs[ingrPiecesRecs.Count - 1][ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count - 1] = curIngrPieceRec;

                //Set the ingredient as no longer falling if it is at the proper position on the bread
                if (curIngrPieceRec.Y == stoppingHeight - curIngrPieceRec.Height)
                {
                    //Play the plop sound effect 
                    plopSnd.CreateInstance().Play();

                    //Set the ingredient as no longer falling
                    ingrFalling = false;

                    //Set the text of the remaining pieces message
                    remainPiecesText = maxPieces - ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count + " / " + maxPieces;

                    //Set the ingredient to bread distances and change the state when all pieces are added
                    if (ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count == maxPieces)
                    {
                        //Set the ingredient to bread distances
                        for (int i = 0; i < ingrPiecesRecs[ingrPiecesRecs.Count - 1].Count; i++)
                        {
                            //Set each distance between the ingredient to the left side of the bread
                            ingrBreadDists[ingrPiecesRecs.Count - 1][i] = new Vector2(ingrPiecesRecs[ingrPiecesRecs.Count - 1][i].X - buildingBreadRecs[BOT_BUN].X,
                                                                                      ingrPiecesRecs[ingrPiecesRecs.Count - 1][i].Y - buildingBreadRecs[BOT_BUN].Y);
                        }

                        //Change the state depending and prepare for the next state depending on the current step
                        if (step != ADDING_VEGGIES)
                        {
                            //Update the sub state
                            subStates[subIndex]++;
                            
                            //Prepare for the sliding step if the step was adding cheese
                            if (step == ADDING_CHEESE)
                            {
                                //Set the sub slide speed to 0, and the bread true location
                                subSlideSpeed1 = 0f;
                                uncookedBreadTrueLocX = buildingBreadRecs[BOT_BUN].X;

                                //Play the sliding sound effect
                                slideSnd.CreateInstance().Play();
                            }
                        }
                        else
                        {
                            //Change the state depending on if the number of types of veggies added is at the max
                            if (subVeggies.Count < MAX_VEGGIES)
                            {
                                //Set the state of the sub as choosing vegetables
                                subStates[subIndexAtCooked] = CHOOSING_VEGGIES;
                            }
                            else
                            {
                                //Set the state of the sub as choosing sauce
                                subStates[subIndexAtCooked] = CHOOSING_SAUCE;
                            }
                        }
                        
                    }
                }
            }
            else if (ingrContRecs[curIngrChosen].Contains(mouse.Position))
            {
                //Add a new piece of meat if the mouse clicked on the container
                if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                {
                    //Play the select sound effect 
                    selectSnd.CreateInstance().Play();

                    //Add a new ingredient piece rectangle
                    curIngrPieceRec = new Rectangle(mouse.Position.X - ingrImgs[curIngrChosen].Width / 2,
                                                    mouse.Position.Y - ingrImgs[curIngrChosen].Height / 2,
                                                    ingrImgs[curIngrChosen].Width,
                                                    ingrImgs[curIngrChosen].Height);
                    ingrPiecesRecs[ingrPiecesRecs.Count - 1].Add(curIngrPieceRec);

                    //Set grabbing ingredient as true
                    grabbingIngr = true;
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Update choosing sauce depending on where the mouse clicks
        private void UpdateChoosingSauce()
        {
            //Check for a mouse click on the finish button if there is at least one sauce added 
            if (subSauces.Count > 0)
            {
                //Set whether the mouse is hovering over the finish button
                hoverOverFinish = finishBtnRecs[NOT_HOVERING].Contains(mouse.Position);

                //Change the sub's state to sliding away if the mouse clicked on the button
                if (hoverOverFinish && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                {
                    //Play the button click sound
                    btnClickSnd.CreateInstance().Play();

                    //Set squeezing bottle as false and the state as adding top bun
                    sprinklingOrSqueezing = false;
                    subStates[subIndexAtCooked] = ADDING_TOP_BUN;

                    //Set the fall speed at 0 initially and the top bun true location
                    cookedIngrFallSpeed = 0f;
                    curCookedIngrPieceLoc = buildingBreadRecs[TOP_BUN].Location.ToVector2();
                }
            }

            //Check for a mouse click on either one of the veggie containers
            for (int i = 0; i < NUM_SAUCE_CHOICES; i++)
            {
                //Check for a mouse click if the mouse is hovering over the sauce bottle
                if (sauceBottleTopsRecs[i].Contains(mouse.Position))
                {
                    //Choose the veggie if the mouse clicked on the container
                    if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                    {
                        //Play the select sound
                        selectSnd.CreateInstance().Play();

                        //Set the finish button off screen 
                        finishBtnRecs[NOT_HOVERING].Y = screenHeight;
                        finishBtnRecs[HOVERING].Y = screenHeight;
                        
                        //Set the current sub sauce that will be added to the sub
                        curSauceChosen = i;
                        subSauces.Add(curSauceChosen);

                        //Switch to adding sauce
                        subStates[subIndexAtCooked] = ADDING_SAUCE;

                        //Set the a new list of rectangles for each sauce drop of the current sauce type and clear the list for the sauce drop states
                        sauceDropRecs.Add(new List<Rectangle>());
                        sauceBreadDists.Add(new Vector2[0]);

                        //Setup the sauce for adding sauce and remove the sauce bottle top from the container
                        SetupLettuceOrSauce(ref sauceBottleSideRec);
                        sauceBottleTopsRecs[i].X = screenWidth;
                        
                        //Set the sprinkling bar on screen
                        ResetSprinklingBar(maxSauceDrops, perfectSauceAmnt);
                    }
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Draw the menu
        private void DrawMenu()
        {
            //Draw the menu screen's title, buttons, and stats
            spriteBatch.Draw(subwayLogoImg, subwayLogoRec, Color.White);
            spriteBatch.DrawString(titleFont, title2ndWord, title2ndWordLoc, Color.Green);
            spriteBatch.Draw(playBtn, playBtnRecs[Convert.ToInt32(hoverOverPlayBtn)], Color.White);
            spriteBatch.Draw(instBtn, instBtnRecs[Convert.ToInt32(hoverOverInstBtn)], Color.White);
            spriteBatch.Draw(shopBtn, shopBtnRecs[Convert.ToInt32(hoverOverShopBtn)], Color.White);
            spriteBatch.DrawString(scoreFont, totalPtsText, totalPtsLoc, Color.Black);
            spriteBatch.DrawString(scoreFont, totalTipsText, totalTipsLoc, Color.Black);
            spriteBatch.DrawString(scoreFont, dayNumText, dayNumLoc, Color.DarkGreen);

        }

        //Pre: None
        //Post: None
        //Desc: Draw the play state depending on where the player is in the game
        private void DrawPlay()
        {
            //Draw the play state that the player is currently in
            switch (playState)
            {
                case STORE_FRONT:
                    //Draw the storefront
                    DrawStoreFront();
                    break;

                case UNCOOKED_COUNTER:
                    //Draw the uncooked counter
                    DrawUncookedCounter();
                    break;

                case TOASTER_STATION:
                    //Draw the toasting station
                    DrawToaster();
                    break;

                case COOKED_COUNTER:
                    //Draw the cooked counter
                    DrawCookedCounter();
                    break;

                case CUST_TABLE:
                    //Draw the customer's table
                    DrawCustTable();
                    break;

                case TAKING_ORDER:
                    //Draw the taking order state
                    DrawTakingOrder();
                    break;

                case CHECKING_ORDER:
                    //Draw the checking order state
                    DrawCheckingOrder();
                    break;
            }
        }

        //Pre: None
        //Post: None
        //Desc: Draw the pre-pregame
        private void DrawPrePregame()
        {
            //Draw the day number and the start button
            spriteBatch.DrawString(titleFont, dayNumText, preGameDayNumLoc, Color.DarkGreen);
            spriteBatch.Draw(startBtn, startBtnRecs[Convert.ToInt32(hoverOverStartBtn)], Color.White);
        }

        //Pre: None
        //Post: None
        //Desc: Draw the pregame
        private void DrawPregame()
        {
            //Draw the sign flip animation
            openSignAnim.Draw(spriteBatch, Color.White, SpriteEffects.None);
        }

        //Pre: None
        //Post: None
        //Desc: Draw the instructions
        private void DrawInstructions()
        {
            //Draw the instruction slide and the arrow images
            spriteBatch.Draw(instrSlides[curInstrSlideView], instrSlideRecs[curInstrSlideView], Color.White);
            spriteBatch.Draw(controlArrows[LEFT], controlArrowRecs1[LEFT], Color.White);
            spriteBatch.Draw(controlArrows[RIGHT], controlArrowRecs1[RIGHT], Color.White);
        }

        //Pre: None
        //Post: None
        //Desc: Draw the pre-endgame
        private void DrawPreEndGame()
        {
            //Draw the closed sign flip
            closedSignAnim.Draw(spriteBatch, Color.White * flipSignTransparency, SpriteEffects.None);
        }

        //Pre: None
        //Post: None
        //Desc: Draw the end game screen
        private void DrawEndGame()
        {
            //Draw the score scales with the pointers and scores
            for (int i = 0; i < NUM_SCORES; i++)
            {
                spriteBatch.Draw(scoreScaleImgs[i], scoreScaleRecs[i], Color.White);
                spriteBatch.Draw(pointerImg, scorePointerRecs[i], null, Color.White, (float)(pointerRotations[i] - Math.PI / 2), pointerOrigin, SpriteEffects.None, 1f);
                spriteBatch.DrawString(scoreFont, scoreTexts[i], scoreTextLocs[i], Color.DarkBlue * scoreTransparencies[i]);
            }

            //Draw the round's stats
            spriteBatch.DrawString(scoreFont, finalAvgScoreText, finalAvgScoreLoc, Color.DarkBlue);
            spriteBatch.DrawString(scoreFont, pointsText, ptsLoc, Color.DarkRed);
            spriteBatch.DrawString(scoreFont, tipsText, tipsLoc, Color.DarkViolet);
            spriteBatch.DrawString(scoreFont, totalPtsText, totalPtsLoc, Color.Black);
            spriteBatch.DrawString(scoreFont, totalTipsText, totalTipsLoc, Color.Black);
            spriteBatch.DrawString(scoreFont, dayNumText, dayNumLoc, Color.DarkGreen);

            //Draw the menu button
            spriteBatch.Draw(menuBtn, menuBtnRecs[Convert.ToInt32(hoverOverMenuBtn)], Color.White);
        }

        //Pre: None
        //Post: None
        //Desc: Draw the shop
        private void DrawShop()
        {
            //Draw the shop background and images
            spriteBatch.Draw(shopBg, bgRec, Color.White);
            spriteBatch.Draw(itemImgs[DOORBELL], itemRecs[DOORBELL], Color.White);
            spriteBatch.Draw(itemImgs[TIMER_TOP], itemRecs[TIMER_TOP], Color.White);
            spriteBatch.Draw(itemImgs[TIMER_BOT], itemRecs[TIMER_BOT], Color.White);
            spriteBatch.Draw(itemImgs[POSTER_1], itemRecs[POSTER_1], Color.White);
            spriteBatch.Draw(itemImgs[POSTER_2], itemRecs[POSTER_2], Color.White);
            spriteBatch.Draw(itemImgs[RADIO], itemRecs[RADIO], Color.White);

            //Draw the locked purchase button 
            if (totalTips < itemPrices[curItemView])
            {
                //Draw the locked purchase button
                spriteBatch.Draw(purchaseBtns[LOCKED], purchaseBtnRec, Color.White);
            }
            else
            {
                //Draw the unlocked purchase button
                spriteBatch.Draw(purchaseBtns[Convert.ToInt32(hoverOverPurchase)], purchaseBtnRec, Color.White);
            }

            //Draw the description box
            spriteBatch.DrawString(descFont, itemNames[curItemView], itemTitleLoc, Color.Black);
            spriteBatch.DrawString(descFont, itemDescs[curItemView], itemDescLoc, Color.Black);
            spriteBatch.Draw(itemImgs[curItemView], itemViewingRec, Color.White);
            spriteBatch.DrawString(descFont, priceText, priceLoc, Color.Black);

            //Draw the balance 
            spriteBatch.DrawString(scoreFont, totalTipsText, balanceLoc, Color.White);

            //Draw the menu button
            spriteBatch.Draw(menuBtn, menuBtnRecs[Convert.ToInt32(hoverOverMenuBtn)], Color.White);
        }

        //Pre: None
        //Post: None
        //Desc: Draw the storefront
        private void DrawStoreFront()
        {
            //Draw the store background
            spriteBatch.Draw(storeBgImg, bgRec, Color.White);
            spriteBatch.Draw(roofImg, roofRec, Color.White);
            spriteBatch.Draw(subwayLogoImg, subwayLogoRec, Color.White);
            spriteBatch.Draw(screenImgs[0], screenRecs[0], Color.White);
            spriteBatch.Draw(screenImgs[1], screenRecs[1], Color.White);
            spriteBatch.Draw(screenImgs[1], screenRecs[2], Color.White);
            spriteBatch.Draw(frontCounterImg, frontCounterRec, Color.White);

            //Draw the first posters if it is purchased
            if (itemIsPurchased[POSTER_1])
            {
                //Draw the first poster
                spriteBatch.Draw(itemImgs[POSTER_1], poster1Rec, Color.White);
            }
            
            //Draw the second poster if it is purchased
            if (itemIsPurchased[POSTER_2])
            {
                //Draw the second poster
                spriteBatch.Draw(itemImgs[POSTER_2], poster2Rec, Color.White);
            }

            //Draw each customer
            for (int i = 0; i < custInStore.Count; i++)
            {
                customers[custInStore[i]].Draw(spriteBatch, Color.White, SpriteEffects.None);
            }

            //Draw the taking order button
            spriteBatch.Draw(takeOrderBtn, takeOrderBtnRecs[Convert.ToInt32(hoverOverTakeOrder)], Color.White);

            //Draw the control arrow and tabs
            spriteBatch.Draw(controlArrows[LEFT], controlArrowRecs1[LEFT], Color.White);
            spriteBatch.Draw(controlArrows[RIGHT], controlArrowRecs1[RIGHT], Color.White);
            spriteBatch.Draw(checkOrdersTab, tabRec, Color.White);
        }

        //Pre: None
        //Post: None
        //Desc: Draw the uncooked counter
        private void DrawUncookedCounter()
        {
            //Draw the garbage button
            spriteBatch.Draw(garbageBtn[Convert.ToInt32(hoveringOverTrash)], uncookedGarbageBtnRec, Color.White);

            //Draw the bread options
            spriteBatch.Draw(breadImgs[ITALIAN][FULL_LOAF], breadOptionRecs[ITALIAN][Convert.ToInt32(hoverOverBreads[ITALIAN])], Color.White);
            spriteBatch.Draw(breadImgs[HERBS_CHEESE][FULL_LOAF], breadOptionRecs[HERBS_CHEESE][Convert.ToInt32(hoverOverBreads[HERBS_CHEESE])], Color.White);
            spriteBatch.Draw(breadImgs[WHOLE_WHEAT][FULL_LOAF], breadOptionRecs[WHOLE_WHEAT][Convert.ToInt32(hoverOverBreads[WHOLE_WHEAT])], Color.White);

            //Draw the meat containers
            spriteBatch.Draw(meatContImgs[CHICKEN], meatContRecs[CHICKEN], Color.White);
            spriteBatch.Draw(meatContImgs[HAM], meatContRecs[HAM], Color.White);
            spriteBatch.Draw(meatContImgs[SALAMI], meatContRecs[SALAMI], Color.White);

            //Draw the cheese containers
            spriteBatch.Draw(cheeseContImgs[CHEDDAR], cheeseContRecs[CHEDDAR], Color.White);
            spriteBatch.Draw(cheeseContImgs[OLD_ENGLISH], cheeseContRecs[OLD_ENGLISH], Color.White);
            spriteBatch.Draw(cheeseContImgs[SWISS], cheeseContRecs[SWISS], Color.White);
            
            //Draw the control arrow and tabs
            spriteBatch.Draw(controlArrows[LEFT], controlArrowRecs2[LEFT], Color.White);
            spriteBatch.Draw(controlArrows[RIGHT], controlArrowRecs2[RIGHT], Color.White);
            spriteBatch.Draw(checkOrdersTab, tabRec, Color.White);

            //Draw the sub builder
            if (subAtUncooked)
            {
                //Draw the uncut bread without any toppings if it is being cut or not cut yet
                if (subStates[subStates.Count - 1] <= CUTTING_BREAD)
                {
                    //Draw the full bread if it is not cut yet
                    spriteBatch.Draw(breadImgs[curBreadChosen][FULL_LOAF], subBreadRecs[subBreadRecs.Count - 1][FULL_LOAF], Color.White);

                    //Draw the guiding line if the 
                    if (subStates[subStates.Count - 1] == CUTTING_BREAD && day == 1)
                    {
                        //Draw the guiding line
                        spriteBatch.Draw(guidingLineImg, guidingLineRec, Color.White);
                    }
                }
                else
                {
                    //Draw the cut bread
                    spriteBatch.Draw(breadImgs[curBreadChosen][BOT_BUN], subBreadRecs[subBreadRecs.Count - 1][BOT_BUN], Color.White);

                    //Draw the remaining pieces text if the state is adding meat or adding cheese
                    if (subStates[subStates.Count - 1] == ADDING_MEAT || subStates[subStates.Count - 1] == ADDING_CHEESE)
                    {
                        //Draw the remaining pieces text
                        spriteBatch.DrawString(remainPiecesFont, uncookedRemainPiecesText, uncookedRemainPiecesLoc, Color.DarkGreen);
                    }

                    //Draw each meat piece that has been taken out of the container
                    for (int i = 0; i < meatPiecesRecs[meatPiecesRecs.Count - 1].Count; i++)
                    {
                        //Draw the certain type of meat chosen at each of their rectangles
                        spriteBatch.Draw(meatImgs[curMeatChosen], meatPiecesRecs[meatPiecesRecs.Count - 1][i], Color.White);
                    }

                    //Draw each cheese slice that has been taken out of the container
                    for (int i = 0; i < cheesePiecesRecs[cheesePiecesRecs.Count - 1].Count; i++)
                    {
                        //Draw the certain type of cheese chosen at each of their rectangles
                        spriteBatch.Draw(cheeseImgs[curCheeseChosen], cheesePiecesRecs[cheesePiecesRecs.Count - 1][i], Color.White);
                    }
                }
            }
 
            //Draw the unrotated knife if the state is not cutting bread
            if (!subAtUncooked || subStates[subStates.Count - 1] != CUTTING_BREAD)
            {
                //Draw the unrotated knife
                spriteBatch.Draw(knifeImgs[UNROTATED], unrotatedKnifeRecs[Convert.ToInt32(hoverOverKnife)], Color.White);
            }
            else
            {
                //Draw the rotated knife depending on if the mouse is currently cutting or not
                if (isCutting)
                {
                    //Draw the knife cutting the bread
                    spriteBatch.Draw(knifeImgs[KNIFE_CUTTING], rotatedKnifeRecs[KNIFE_CUTTING - 1], Color.White);
                }
                else
                {
                    //Draw only the knife rotated but not cutting the bread
                    spriteBatch.Draw(knifeImgs[ROTATED], rotatedKnifeRecs[ROTATED - 1], Color.White);
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Draw the toaster station
        private void DrawToaster()
        {
            //Draw the toasters
            spriteBatch.Draw(toasterImg, toasterRecs[TOP_TOASTER], Color.White);
            spriteBatch.Draw(toasterImg, toasterRecs[BOT_TOASTER], Color.White);

            //Draw the control arrow and tabs
            spriteBatch.Draw(controlArrows[LEFT], controlArrowRecs1[LEFT], Color.White);
            spriteBatch.Draw(controlArrows[RIGHT], controlArrowRecs1[RIGHT], Color.White);
            spriteBatch.Draw(checkOrdersTab, tabRec, Color.White);

            //Draw the toaster bars and lines
            for (int i = 0; i < NUM_TOASTERS; i++)
            {
                //Draw the empty and full bar at each toaster
                spriteBatch.Draw(toastingBarImg[EMPTY_BAR], toastingBarRecs[i][EMPTY_BAR], Color.White);
                spriteBatch.Draw(toastingBarImg[FULL_BAR], toastingBarRecs[i][FULL_BAR], Color.White);

                //Draw the perfect toasting line
                spriteBatch.Draw(perfectToastLineImg, perfToastLineRecs[i], Color.White);
            }

            //Draw the right arrow
            spriteBatch.Draw(rightArrowImg, rightArrowRec, Color.White);

            //Draw the full sub at the line to the toaster station
            if (subsInLineAtToaster.Count > 0)
            {
                //Draw the full sub
                DrawFullSub(subsInLineAtToaster[0], Color.White);
            }

            //Draw the grabbed sub number
            if (grabbingSub)
            {
                //Draw the full sub of the grabbed sub
                DrawFullSub(grabbedSubNum, Color.White);
            }

            //Draw the subs that are in each toaster
            for (int i = 0; i < NUM_TOASTERS; i++)
            {
                //Draw the sub only if the toaster is occupied by the sub
                if (toastersOccupancy[i] != -1 && grabbedSubNum != toastersOccupancy[i])
                {
                    //Draw the full sub 
                    DrawFullSub(toastersOccupancy[i], Color.Black * TOASTER_WINDOW_TRANSPARENCY);
                }
            }

            //Draw the subs in line moving away from the toasting station
            for (int i = 0; i < subsAwayLineToaster.Count; i++)
            {
                //Draw the sub that is in the line moving away from the toasting station
                DrawFullSub(subsAwayLineToaster[i], Color.White);
            }
        }

        //Pre: None
        //Post: None
        //Desc: Draw the cooked counter
        private void DrawCookedCounter()
        {
            //Draw the garbage button
            spriteBatch.Draw(garbageBtn[Convert.ToInt32(hoveringOverTrash)], cookedGarbageBtnRec, Color.White);

            //Draw the veggie containers
            for (int i = 0; i < NUM_VEGGIE_CHOICES; i++)
            {
                //Draw each veggie container at their location
                spriteBatch.Draw(veggieContImgs[i], veggieContRecs[i], Color.White);
            }

            //Draw the sauce container
            spriteBatch.Draw(sauceContImg, sauceContRec, Color.White);

            //Draw the sauce bottle tops
            for (int i = 0; i < NUM_SAUCE_CHOICES; i++)
            {
                //Draw each sauce bottle top
                spriteBatch.Draw(sauceTopImgs[i], sauceBottleTopsRecs[i], Color.White);
            }

            //Draw the control arrow and tabs
            spriteBatch.Draw(controlArrows[LEFT], controlArrowRecs2[LEFT], Color.White);
            spriteBatch.Draw(controlArrows[RIGHT], controlArrowRecs2[RIGHT], Color.White);
            spriteBatch.Draw(checkOrdersTab, tabRec, Color.White);

            //Draw sprinkling bar
            spriteBatch.Draw(sprinklingBarImgs[EMPTY_BAR], sprinklingBarRecs[EMPTY_BAR], Color.White);
            spriteBatch.Draw(sprinklingBarImgs[FULL_BAR], sprinklingBarRecs[FULL_BAR], Color.White);
            spriteBatch.Draw(perfectAmntLineImg, sprinklingBarLineRec, Color.White);
            
            //Draw the sub at the station if there is at least 1 sub at the cooked counter
            if (subsAtCooked.Count > 0)
            {
                //Draw the current sub that is at the counter and the top bun
                DrawFullSub(subsAtCooked[0], Color.White);
                spriteBatch.Draw(breadImgs[subBreads[subNums.IndexOf(subsAtCooked[0])]][TOP_BUN], subBreadRecs[subNums.IndexOf(subsAtCooked[0])][TOP_BUN], Color.White);

                //Draw the remaining pieces text or the scooper
                if (subStates[subIndexAtCooked] == ADDING_VEGGIES)
                {
                    //Draw the scooper image if the veggie chosen is lettuce
                    if (curVeggieChosen == LETTUCE)
                    {
                        //Draw the scooper
                        spriteBatch.Draw(scooperImg, scooperRec, Color.White);
                    }
                    else
                    {
                        //Draw the remaining pieces text
                        spriteBatch.DrawString(remainPiecesFont, cookedRemainPiecesText, cookedRemainPiecesLoc, Color.Black);
                    }
                }

                //Draw the sauce side bottle if there is at least 1 sauce that is being added to the sub
                if (subSauces.Count > 0)
                {
                    //Draw the sauce side bottle
                    spriteBatch.Draw(sauceBottleImgs[subSauces[subSauces.Count - 1]], sauceBottleSideRec, Color.White);
                }

                //Draw the finish button
                spriteBatch.Draw(finishBtn, finishBtnRecs[Convert.ToInt32(hoverOverFinish)], Color.White);

                //Draw the select order pop up
                if (subStates[subIndexAtCooked] == SELECT_ORDER_NUM)
                {
                    //Draw the order selection pop up
                    spriteBatch.Draw(orderSelectionImg, orderSelectionRec, Color.White);

                    //Draw each available order number
                    for (int i = 0; i < availOrders.Count; i++)
                    {
                        //Draw the available order number buttons
                        spriteBatch.Draw(orderNumBtns[availOrders[i] - 1], orderNumBtnRecs[availOrders[i] - 1][Convert.ToInt32(hoverOverOrderNums[availOrders[i] - 1])], Color.White);
                    }
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Draw the customer's table
        private void DrawCustTable()
        {
            //Draw the wrapper background
            spriteBatch.Draw(subwayWrapperBg, bgRec, Color.White);

            //Draw the sub and the top bun
            DrawFullSub(subsAtCooked[0], Color.White);
            spriteBatch.Draw(breadImgs[subBreads[subIndexAtCooked]][TOP_BUN], subBreadRecs[subIndexAtCooked][TOP_BUN], Color.White);
            
            //Draw the score scales, pointers, and scores
            for (int i = 0; i < NUM_SCORES; i++)
            {
                //Draw each scale, pointer, and score
                spriteBatch.Draw(scoreScaleImgs[i], scoreScaleRecs[i], Color.White);
                spriteBatch.Draw(pointerImg, scorePointerRecs[i], null, Color.White, (float)(pointerRotations[i] - Math.PI / 2), pointerOrigin, SpriteEffects.None, 1f);
                spriteBatch.DrawString(scoreFont, scoreTexts[i], scoreTextLocs[i], Color.Black * scoreTransparencies[i]);
            }

            //Draw the points and tips earned from the order
            spriteBatch.DrawString(scoreFont, pointsText, ptsLoc, Color.DarkRed);
            spriteBatch.DrawString(scoreFont, tipsText, tipsLoc, Color.DarkViolet);
        }

        //Pre: None
        //Post: None
        //Desc: Draw the taking order screen
        private void DrawTakingOrder()
        {
            //Draw the notepad
            DrawNotepad(nextOrderNum);
        }
        
        //Pre: None
        //Post: None
        //Desc: Draw the checking order state
        private void DrawCheckingOrder()
        {
            //Draw the notepad
            DrawNotepad(availOrders[curOrderView]);
            
            //Draw the control arrow and tabs
            spriteBatch.Draw(controlArrows[LEFT], controlArrowRecs1[LEFT], Color.White);
            spriteBatch.Draw(controlArrows[RIGHT], controlArrowRecs1[RIGHT], Color.White);
            spriteBatch.Draw(backTab, tabRec, Color.White);
        }

        //Pre: gameTime is a positive value of the amount of time passed in the game and customerNum is the positive customer number that is less than the max number of customers
        //Post: None
        //Desc: Move each customer
        private void MoveCustomer(int customerNum, GameTime gameTime)
        {
            //Set the current speed of the current customer walking in
            custSpeedX = -1 * maxCustWalkSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Set the true location of that customer
            custLocs[customerNum].X += custSpeedX;

            //Set the customer at its rounded true location
            customers[customerNum].destRec.X = (int)custLocs[customerNum].X;
        }

        //Pre: orderNum is the current positive integer of the order number
        //Post: None
        //Desc: Draw the notepad
        private void DrawNotepad(int orderNum)
        {
            //Draw the notepad background
            spriteBatch.Draw(notepadImg, notepadBgRec, Color.White);

            //Draw the notepad order number and food labels
            spriteBatch.DrawString(writingFont, orderNumNoteLabel[orderNum - 1], orderNumNoteLoc, Color.Black);
            spriteBatch.DrawString(writingFont, breadNoteLabel, breadNoteLoc, Color.Black);
            spriteBatch.DrawString(writingFont, meatNoteLabel, meatNoteLoc, Color.Black);
            spriteBatch.DrawString(writingFont, cheeseNoteLabel, cheeseNoteLoc, Color.Black);
            spriteBatch.DrawString(writingFont, veggieNoteLabel, veggieNoteLoc, Color.Black);
            spriteBatch.DrawString(writingFont, sauceNoteLabel, sauceNoteLoc, Color.Black);

            //Draw the bread, meat and cheeses
            spriteBatch.Draw(breadImgs[breadOrders[orderNum - 1]][FULL_LOAF], breadRecordRec, Color.White);
            spriteBatch.Draw(meatImgs[meatOrders[orderNum - 1]], meatRecordRecs[orderNum - 1], Color.White);
            spriteBatch.Draw(cheeseImgs[cheeseOrders[orderNum - 1]], cheeseRecordRec, Color.White);

            //Draw each of the veggies ordered
            for (int i = 0; i < veggieOrders[orderNum - 1].Length; i++)
            {
                //Draw the veggie
                spriteBatch.Draw(veggieImgs[veggieOrders[orderNum - 1][i]], veggieRecordRecs[orderNum - 1][i], Color.White);
            }

            //Draw each of the sauces ordered
            for (int i = 0; i < sauceOrders[orderNum - 1].Length; i++)
            {
                //Draw the sauce
                spriteBatch.Draw(sauceTopImgs[sauceOrders[orderNum - 1][i]], sauceRecordRecs[orderNum - 1][i], Color.White);
            }
        }

        //Pre: none
        //Post: None
        //Desc: Switch the stations of the play state
        private void SwitchPlaces()
        {
            //Change the play state when the D key is pressed
            if (kb.IsKeyDown(Keys.D) && !prevKb.IsKeyDown(Keys.D))
            {
                //Play the step sound
                stepSnd.CreateInstance().Play();

                //Move to the next station
                playState++;

                //Cycle the station back to the store front if the station number is greater than the cooked counter's number
                if (playState > COOKED_COUNTER)
                {
                    //Set the play state to the store front
                    playState = STORE_FRONT;
                }
            }

            //Change the play state when the A key is pressed
            if (kb.IsKeyDown(Keys.A) && !prevKb.IsKeyDown(Keys.A))
            {
                //Play the step sound
                stepSnd.CreateInstance().Play();

                //Decrease the play state to the previous station
                playState--;

                //Cycle the station to the last station if the station number is less than the store front's number
                if (playState < STORE_FRONT)
                {
                    //Set the play state to the cooked counter
                    playState = COOKED_COUNTER;
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Allow the player to check the available orders and flip through the orders
        private void CheckOrders()
        {
            //Check the orders if the W key is pressed
            if (kb.IsKeyDown(Keys.W) && !prevKb.IsKeyDown(Keys.W) && availOrders.Count > 0)
            {
                //Play the step sound
                stepSnd.CreateInstance().Play();

                //Set the previous play state as the current state and change the current state to checking orders
                prevPlayState = playState;
                playState = CHECKING_ORDER;
            }
        }

        //Pre: None
        //Post: None
        //Desc: Reset the knife to its resting location
        private void ResetKnife()
        {
            //Reset the knife location, hover status, cutting progress
            hoverOverKnife = false;
            isCutting = false;
            cuttingProgress = 0;

            //Reset the cutting zone scores and the current bread zone
            cuttingZoneScores = new double[BREAD_ZONES];
            curBreadZone = 1;
        }

        //Pre: Sub num is the positive integer sub number and sub scaler is a decimal float value of how large the sub is to be drawn
        //Post: None
        //Desc: Set the sub's filling to where the sub's bread is
        private void SetSubFillingRecsLocation(int subNum, float subScaler)
        {
            //Set the sub index
            int subIndex = subNums.IndexOf(subNum);

            //Set the location of the meats
            for (int i = 0; i < meatPiecesRecs[subIndex].Count; i++)
            {
                //Set each piece of meat rectangle as a new rectangle with the updated x and y coordinates
                meatPiecesRecs[subIndex][i] = new Rectangle((int)(subBreadRecs[subIndex][BOT_BUN].X + meatsBreadDists[subIndex][i].X * subScaler),
                                                                  (int)(subBreadRecs[subIndex][BOT_BUN].Bottom - FIRST_LAYER_VERT_SPACER * subScaler - meatImgs[subMeats[subIndex]].Height * subScaler),
                                                                  (int)(meatImgs[subMeats[subIndex]].Width * subScaler),
                                                                  (int)(meatImgs[subMeats[subIndex]].Height * subScaler));
            }

            //Set the location of the cheeses
            for (int i = 0; i < cheesePiecesRecs[subIndex].Count; i++)
            {
                //Set each piece of cheese rectangle as a new rectangle with the updated x and y coordinates
                cheesePiecesRecs[subIndex][i] = new Rectangle((int)(subBreadRecs[subIndex][BOT_BUN].X + cheesesBreadDists[subIndex][i].X * subScaler),
                                                              (int)(subBreadRecs[subIndex][BOT_BUN].Bottom - (FIRST_LAYER_VERT_SPACER + CHEESE_VERT_SPACING) * subScaler - cheeseImgs[subCheeses[subIndex]].Height * subScaler),
                                                              (int)(cheeseImgs[subCheeses[subIndex]].Width * subScaler),
                                                              (int)(cheeseImgs[subCheeses[subIndex]].Height * subScaler));
            }

            //Set the sub filling locations for the sub only if the sub is at the cooked counter
            if (subsAtCooked.Count > 0 && subNum == subsAtCooked[0])
            {
                //Set the location of each type of veggie
                for (int i = 0; i < subVeggies.Count; i++)
                {
                    //Set the rectangle of each veggie piece in each type
                    for (int j = 0; j < veggiePiecesRecs[i].Count; j++)
                    {
                        //Set the rectangle for the vegetable
                        veggiePiecesRecs[i][j] = new Rectangle((int)(subBreadRecs[subIndex][BOT_BUN].X + veggieBreadDists[i][j].X * subScaler),
                                                                         (int)(subBreadRecs[subIndex][BOT_BUN].Y + veggieBreadDists[i][j].Y * subScaler),
                                                                         (int)(veggieImgs[subVeggies[i]].Width * subScaler),
                                                                         (int)(veggieImgs[subVeggies[i]].Height * subScaler));
                    }
                }

                //Set the location of the sauce drops
                for (int i = 0; i < subSauces.Count; i++)
                {
                    //Set the rectangle of each sauce drop of each type
                    for (int j = 0; j < sauceDropRecs[i].Count; j++)
                    {
                        //Set the rectangle for the sauce drop
                        sauceDropRecs[i][j] = new Rectangle((int)(subBreadRecs[subIndex][BOT_BUN].X + sauceBreadDists[i][j].X * subScaler),
                                                            (int)(subBreadRecs[subIndex][BOT_BUN].Y + sauceBreadDists[i][j].Y * subScaler),
                                                            (int)(sauceDropImgs[subSauces[i]].Width * subScaler),
                                                            (int)(sauceDropImgs[subSauces[i]].Height * subScaler));
                    }
                }
            }
        }

        //Pre: Color is the color of the sub's filter
        //Post: None
        //Desc: Draw the entire sub with all its ingredients
        private void DrawFullSub(int subNum, Color colour)
        {
            //Set the sub number
            int subIndex = subNums.IndexOf(subNum);

            //Draw the bread bottom
            spriteBatch.Draw(breadImgs[subBreads[subIndex]][BOT_BUN], subBreadRecs[subIndex][BOT_BUN], colour);

            //Draw each piece of meat
            for (int i = 0; i < meatPiecesRecs[subNums.IndexOf(subNum)].Count; i++)
            {
                spriteBatch.Draw(meatImgs[subMeats[subIndex]], meatPiecesRecs[subNums.IndexOf(subNum)][i], colour);
                //spriteBatch.Draw(meatImgs[subMeats[subIndex]], meatPiecesRecs[subIndex][i], null, colour, meatRotations[subIndex][i], meatOrigins[subIndex][i], SpriteEffects.None, 1);
            }

            //Draw each piece of cheese
            for (int i = 0; i < cheesePiecesRecs[subNums.IndexOf(subNum)].Count; i++)
            {
                spriteBatch.Draw(cheeseImgs[subCheeses[subNums.IndexOf(subNum)]], cheesePiecesRecs[subNums.IndexOf(subNum)][i], colour);
                //spriteBatch.Draw(cheeseImgs[subCheeses[subIndex]], cheesePiecesRecs[subIndex][i], null, colour, cheeseRotations[subIndex][i], cheeseOrigins[subIndex][i], SpriteEffects.None, 1);

            }

            //Only draw the vegetables and the sauces if the sub is at the cooked station
            if (subsAtCooked.Count > 0 && subNum == subsAtCooked[0])
            {
                //Draw each veggie type
                for (int i = 0; i < veggiePiecesRecs.Count; i++)
                {
                    //Draw the veggies depending on the type of vegetable
                    if (subVeggies[i] == LETTUCE)
                    {
                        //Draw each lettuce piece of each type
                        for (int j = 0; j < veggiePiecesRecs[i].Count; j++)
                        {
                            //Draw each piece of lettuce with rotation
                            spriteBatch.Draw(veggieImgs[subVeggies[i]], veggiePiecesRecs[i][j], null, colour, veggieRotations[i][j] / NUM_DIFF_LETTUCE_ROTATIONS, lettuceOrigin, SpriteEffects.None, 1);
                        }
                    }
                    else
                    {
                        //Draw each veggie piece of each type
                        for (int j = 0; j < veggiePiecesRecs[i].Count; j++)
                        {
                            //Draw each vegetable normally
                            spriteBatch.Draw(veggieImgs[subVeggies[i]], veggiePiecesRecs[i][j], colour);
                        }
                    }
                }
                

                //Draw each sauce drop type
                for (int i = 0; i < sauceDropRecs.Count; i++)
                {
                    //Draw each sauce drop for each type
                    for (int j = 0; j < sauceDropRecs[i].Count; j++)
                    {
                        //Draw each sacue drop
                        spriteBatch.Draw(sauceDropImgs[subSauces[i]], sauceDropRecs[i][j], colour);
                    }
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Delete all the variables associated with that sub number
        private void DeleteSub(int subIndex)
        {
            //Play the button click sound
            btnClickSnd.CreateInstance().Play();

            //Remove the last sub state and scores
            subStates.RemoveAt(subIndex);
            cutScores.RemoveAt(subIndex);
            toastScores.RemoveAt(subIndex);

            //Remove the sub number and the type of each ingredient from the list that keeps track of the type of each ingredient on each sub
            subNums.RemoveAt(subIndex);
            subBreads.RemoveAt(subIndex);
            subMeats.RemoveAt(subIndex);
            subCheeses.RemoveAt(subIndex);

            //Remove the list of rectangles for the bread and pieces of meat and cheese
            subBreadRecs.RemoveAt(subIndex);
            meatPiecesRecs.RemoveAt(subIndex);
            cheesePiecesRecs.RemoveAt(subIndex);

            //Add a new list of horizontal distances between the left side of the bread and the left side of the ingredient
            meatsBreadDists.RemoveAt(subIndex);
            cheesesBreadDists.RemoveAt(subIndex);

            //Change the sub index at the uncooked station
            subIndexAtUncooked--;
        }

        //Pre: None
        //Post: None
        //Desc: Update the order selection choices
        private void UpdateOrderSelectionChoices()
        {
            //Set the location of each available order number button
            for (int i = 0; i < availOrders.Count; i++)
            {
                //Set the order number button rectangle for hovering or not hovering
                orderNumBtnRecs[availOrders[i] - 1][NOT_HOVERING].X = orderSelectionRec.X + ORDER_NUM_BTN_SPACER * (i + 1) + orderNumBtnRecs[availOrders[i] - 1][NOT_HOVERING].Width * i;
                orderNumBtnRecs[availOrders[i] - 1][HOVERING].X = orderSelectionRec.X + ORDER_NUM_BTN_SPACER * (i + 1) + orderNumBtnRecs[availOrders[i] - 1][NOT_HOVERING].Width * i;
                orderNumBtnRecs[availOrders[i] - 1][NOT_HOVERING].Y = orderSelectionRec.Y + ORDER_SELECTION_TOP_SPACER;
                orderNumBtnRecs[availOrders[i] - 1][HOVERING].Y = orderSelectionRec.Y + ORDER_SELECTION_TOP_SPACER - HOVER_DISPLACEMENT;
            }
        }

        private double CalculateIngrCorrectness(int maxTypes, int[] ingrOrders, int[] subIngrs)
        {
            //Store the comparison arrays
            double[] orderIngrsComprsn = new double [maxTypes];
            double[] subIngrsComprsn = new double [maxTypes];
            
            //Store the ingrediant correctness score
            double ingrCorrectness = 1;

            //Set each element at -1 for the comparison arrays
            for (int i = 0; i < maxTypes; i++)
            {
                //Set each comparison array with an initial value of -1
                orderIngrsComprsn[i] = -1;
                subIngrsComprsn[i] = -1;
            }

            //Set the ingredient orders in the comparison array
            for (int i = 0; i < ingrOrders.Length; i++)
            {
                //Copy the ordered ingredient in the comparison array
                orderIngrsComprsn[i] = ingrOrders[i];
            }

            //Set the ingredient in the sub in the comparison array
            for (int i = 0; i < subIngrs.Length; i++)
            {
                //Copy the used ingredient in the comparison array
                subIngrsComprsn[i] = subIngrs[i];
            }

            //Compare the comparison array
            for (int i = 0; i < maxTypes; i++)
            {
                //Compare each oredered ingredient and the used ingredient and subtract from the score if it is wrong
                if (orderIngrsComprsn[i] != subIngrsComprsn[i])
                {
                    //Decrease the correctness by the reciprocal of the max types of the ingredient
                    ingrCorrectness -= 1.0 / maxTypes;
                }
            }

            //Return the ingredient correctness
            return ingrCorrectness;
        }

        //Pre: None
        //Post: None
        //Desc: Calculate the placement score of the sub
        private double CalculateNormalPlacementScore(Vector2[] ingrBreadDists, int maxIngr, int ingrWidth)
        {
            //Store the horizontal distances from the bread and the horizontal distances from perfect
            int[] ingrBreadXDists = new int[ingrBreadDists.Length];
            int[] ingrDistsFromPerfect = new int[ingrBreadDists.Length];

            //Store the average distance from perfect and the final topping score
            double avgDistFromPerfect;
            double finalToppingScore;

            //Set the ingredient distances from the bread
            for (int i = 0; i < ingrBreadDists.Length; i++)
            {
                //Set each horizontal distance as the x distance from the bread's top left corner to the topping's top left corner
                ingrBreadXDists[i] = (int)ingrBreadDists[i].X;
            }

            //Sort the ingredient to bread horizontal distances
            Array.Sort(ingrBreadXDists);
            
            //Set each ingredient's distance from perfect in order
            for (int i = 0; i < ingrBreadDists.Length; i++)
            {
                //Set the ingredient's distance from perfect as the absolute valuse of the ingredient's distance minus the space in between each perfect location
                ingrDistsFromPerfect[i] = Math.Abs(ingrBreadXDists[i] - i * buildingBreadRecs[BOT_BUN].Width / maxIngr);
            }

            //Calculate the average distance from perfect
            avgDistFromPerfect = ingrDistsFromPerfect.Average();

            //Calculate the topping placement score by subtracting one minus the ration between the average distance from perfect and the space in between each perfect location 
            finalToppingScore = 1 - (avgDistFromPerfect / (buildingBreadRecs[BOT_BUN].Width / maxIngr));

            //Set the score as 0 if it is less than 0
            if (finalToppingScore < 0)
            {
                //Set the score at 0
                finalToppingScore = 0;
            }

            //Return the topping score
            return finalToppingScore;
        }

        private double CalculateSauceOrLettucePlacementScore(Vector2[] ingrBreadDists, int perfectAmntPerZone)
        {
            //Set the amount of sauce drops in each zone
            int[] zonesNumPieces = new int[BREAD_ZONES];
            double[] zoneScores = new double[BREAD_ZONES];
            double finalScore;

            //Determine the number of sauce drops in each zone
            for (int i = 0; i < ingrBreadDists.Length; i++)
            {
                //Increase the number of drops in the zone that the drop is in
                zonesNumPieces[(int)(ingrBreadDists[i].X / buildingBreadRecs[BOT_BUN].Width * BREAD_ZONES)]++;
            }

            //Determine how much over or below the perfect amount of sauce in each zone
            for (int i = 0; i < BREAD_ZONES; i++)
            {
                //Set the sauce zone score as the difference between the sauce in the zone and the perfect amount of sauce
                zoneScores[i] = 1 - (double)Math.Abs(zonesNumPieces[i] - perfectAmntPerZone) / perfectAmntPerZone;

                //Set the score at 0 if it is less than 0
                if (zoneScores[i] < 0)
                {
                    //Set the score at 0
                    zoneScores[i] = 0;
                }
            }

            //Calculate the sauce evenness as the average of the zone scores and return the value
            finalScore = zoneScores.Average();
            return finalScore;
        }

        private void RotatePointer(int ptrIndex, GameTime gameTime)
        {
            //Set the pointer rotation speed
            float ptrSpeed = (float)(((maxPtrSpeed - minPtrSpeed) * (1 - pointerRotations[ptrIndex] / intendedPtrRotations[ptrIndex]) + minPtrSpeed) * gameTime.ElapsedGameTime.TotalSeconds);

            //Increase the pointer's rotation
            pointerRotations[ptrIndex] += ptrSpeed;

            //Set the pointer's rotation to the inteded rotation if it is over
            if (pointerRotations[ptrIndex] > intendedPtrRotations[ptrIndex])
            {
                //Set the pointer's rotation to the intended rotation
                pointerRotations[ptrIndex] = intendedPtrRotations[ptrIndex];
            }
        }

        private void IncreaseFadeInScore(int scoreType, GameTime gameTime)
        {
            //SUpdate the transparency of the score
            float fadingSpeed = (float)(gameTime.ElapsedGameTime.TotalSeconds * fadeMaxSpeed);

            //Update the transparency of the score
            scoreTransparencies[scoreType] += fadingSpeed;

            //Set the score transparency to the maximum if it is greater than 1
            if (scoreTransparencies[scoreType] > 1f)
            {
                //Set the transparency to 1
                scoreTransparencies[scoreType] = 1f;
            }
        }

        
        private float FadeOut(float transparency, GameTime gameTime)
        {
            //Set the fading out speed
            float fadingSpeed = (float)(gameTime.ElapsedGameTime.TotalSeconds * fadeMaxSpeed);

            //Reduce the transparency by the speed
            transparency -= fadingSpeed;

            //Return the transparency
            return transparency;
        }

        private void TranslateLettuceOrSauceDrop(int pieceIndex, List<Rectangle> pieceRecs, float fallSpeed, int step)
        {
            //Store the current piece rectangle
            Rectangle curLettuceOrDropRec;

            //Increase the true location of the piece
            lettuceOrDropTrueLocs[pieceIndex] += fallSpeed;

            //Set the current piece rectangle to the piece rectangle and update its y position
            curLettuceOrDropRec = pieceRecs[pieceIndex];
            curLettuceOrDropRec.Y = (int)lettuceOrDropTrueLocs[pieceIndex];

            //Set the original piece rectangle to the current piece rectangle
            pieceRecs[pieceIndex] = curLettuceOrDropRec;

            //Remove or stop the falling if the piece is over the bread or at the appropriate stopping location
            if (pieceRecs[pieceIndex].Y >= screenHeight)
            {
                //Remove the current piece not on the bread
                lettuceOrDropFalling.RemoveAt(pieceIndex);
                lettuceOrDropTrueLocs.RemoveAt(pieceIndex);
                pieceRecs.RemoveAt(pieceIndex);
                lettuceOrSauceFallLocs.RemoveAt(pieceIndex);

                //Only remove the veggie rotation and origin if it is lettuce
                if (step == ADDING_VEGGIES)
                {
                    //Remove the veggie rotation and origin
                    veggieRotations[subVeggies.Count - 1].RemoveAt(pieceIndex);

                    //Remove the lettuce speed
                    lettuceSpeeds.RemoveAt(pieceIndex);
                }

                //Increase the wasted sauce drops
                wastedPieces++;
            }
            else if (pieceRecs[pieceIndex].Y >= lettuceOrSauceFallLocs[pieceIndex] && pieceRecs[pieceIndex].X >= buildingBreadRecs[BOT_BUN].X - pieceRecs[pieceIndex].Width / 2 && pieceRecs[pieceIndex].Right <= buildingBreadRecs[BOT_BUN].Right + pieceRecs[pieceIndex].Width / 2)
            {
                if (Util.IntersectsPixel(pieceRecs[pieceIndex].Center.ToVector2(), buildingBreadRecs[BOT_BUN], breadImgs[subBreads[subIndexAtCooked]][BOT_BUN]))
                {
                    //Set the lettuce or drop piece as no longer falling
                    lettuceOrDropFalling[pieceIndex] = false;
                }
                
            }
        }

        private Rectangle GrabIngrPiece(Rectangle ingrPiece)
        {
            //Set the current ingredient piece rectangle at the mouse
            ingrPiece.X = mouse.X - ingrPiece.Width / 2;
            ingrPiece.Y = mouse.Y - ingrPiece.Height / 2;

            //Return the ingredient piece rectangle
            return ingrPiece;
        }

        private void DropIngredient(ref Rectangle ingrRec, int stoppingYLoc, ref Vector2 ingrTrueLoc, ref float fallSpeed, GameTime gameTime)
        {
            //Set the location of the ingredient depending on if it is above or below its stopping location
            if (ingrRec.Bottom < stoppingYLoc)
            {
                //Accelerate the ingredient down onto the bread by increasing the fall speed and moving the ingredient by the speed
                fallSpeed += (float)(GRAVITY * gameTime.ElapsedGameTime.TotalSeconds);
                ingrTrueLoc.Y += fallSpeed;
                ingrRec.Y = (int)ingrTrueLoc.Y;
            }
            else if (ingrRec.Bottom > stoppingYLoc)
            {
                //Set the ingredient piece at the  correct location above the bread
                ingrRec.Y = stoppingYLoc - ingrRec.Height;
            }
        }

        private void DragScooperOrBottle(ref Rectangle toolRec)
        {
            //Set the tool center at the mouse's location
            toolRec.X = mouse.X - toolRec.Width / 2;
            toolRec.Y = mouse.Y - toolRec.Height / 2;

            //Set the tool at the max or min height if its y location is out of its range
            if (toolRec.Y > TOP_SCREEN_SPACE + buildingBreadRecs[BOT_BUN].Height - FIRST_LAYER_VERT_SPACER)
            {
                //Set the tool at the min height height
                toolRec.Y = TOP_SCREEN_SPACE + buildingBreadRecs[BOT_BUN].Height - FIRST_LAYER_VERT_SPACER;
            }
            else if (toolRec.Y < TOP_SCREEN_SPACE)
            {
                //Set the tool rectangle at the max height
                toolRec.Y = TOP_SCREEN_SPACE;
            }
        }
        
        private void ResetScoreScales(int scoreType)
        {
            //Calculate the intended pointer rotations and set the current rotation to 0
            pointerRotations[scoreType] = 0f;
            scoreTransparencies[scoreType] = 0f;

            //Set the location of each score text
            scoreTextLocs[scoreType].X = scoreScaleRecs[scoreType].X + scoreScaleRecs[scoreType].Width / 2 - scoreFont.MeasureString(scoreTexts[scoreType]).X / 2;
        }

        private void UpdateScoreScales(GameTime gameTime)
        {
            //Update each pointer's rotation and score transparency
            for (int i = 0; i < NUM_SCORES; i++)
            {
                //Rotate each pointer that isn't fully rotated
                if (pointerRotations[i] < intendedPtrRotations[i])
                {
                    //Rotate each pointer
                    RotatePointer(i, gameTime);
                }

                //Fade in the scores
                if (scoreTransparencies[i] < 1f)
                {
                    //Increase the transparency of the score
                    IncreaseFadeInScore(i, gameTime);
                }
            }
        }

        private void RandomizeSauceOrVeggieOrder(int numOptionTypes, int orderIndex, ref List<int[]> ingrOrders, Texture2D[] ingrImgs, ref List<Rectangle[]> ingrRecordRecs, int notepadPicLocY)
        {
            //Store the pool for the veggies or the sauces to choose from
            List<int> ingrPool = new List<int>();

            //Store the total picture width of the pictures of the ingredients to be displayed in the notepad so that images can be centered
            int totalPicWidth = 0;

            //Set the ingredient pool
            for (int i = 0; i < numOptionTypes; i++)
            {
                //Add each ingredient number in the pool of ingredients
                ingrPool.Add(i);
            }
            
            //Randomize the veggie orders and calculate the total width for the scaled veggie pictures
            for (int i = 0; i < ingrOrders[orderIndex].Length; i++)
            {
                //Select a vegetable for the order that has not already been chosen and remove that vegetable from the pool
                ingrOrders[orderIndex][i] = ingrPool[rng.Next(0, ingrPool.Count)];
                ingrPool.Remove(ingrOrders[orderIndex][i]);

            //Add up the total width for scaled pictures of the veggies
            totalPicWidth += (int)(ingrImgs[ingrOrders[orderIndex][i]].Width * (float)picturesNoteHeight / ingrImgs[ingrOrders[orderIndex][i]].Height);
            }
            
            //Add the first rectangle for the veggie that will appear in the notepad for taking orders
            ingrRecordRecs[orderIndex][0] = new Rectangle(screenWidth / 2 - totalPicWidth / 2,
                                                          screenHeight,
                                                          (int)(ingrImgs[ingrOrders[orderIndex][0]].Width * (float)picturesNoteHeight / ingrImgs[ingrOrders[orderIndex][0]].Height),
                                                          picturesNoteHeight);

            //Add the next veggie rectangles after the first rectangle for the veggie orders to be displayed in the notepad
            for (int i = 1; i < ingrOrders[orderIndex].Length; i++)
            {
                //Add another rectangle for the veggie that will appear in the notepad for taking orders
                ingrRecordRecs[orderIndex][i] = new Rectangle(ingrRecordRecs[orderIndex][i - 1].Right,
                                                              screenHeight,
                                                              (int)(ingrImgs[ingrOrders[orderIndex][i]].Width * (float)picturesNoteHeight / ingrImgs[ingrOrders[orderIndex][i]].Height),
                                                              picturesNoteHeight);
            }
        }

        private void UpdateSliding(GameTime gameTime, int station, ref float subSlideSpeed, ref float breadTrueLocX, int subIndex)
        {
            //Increase the slide speed of the sub and the true location of the bread by the slide speed
            subSlideSpeed += (float)(PUSH * gameTime.ElapsedGameTime.TotalSeconds);
            breadTrueLocX += subSlideSpeed;

            //Check if the sub is entirely out of the picture, including if the largest topping, chicken, is hanging halfway off the sub
            if (breadTrueLocX - meatImgs[CHICKEN].Width / 2 > screenWidth)
            {
                if (station == UNCOOKED_COUNTER)
                {
                    //Set the sub as in line at the toasting station, the uncooked station as not having a sub, no bread, meat, or cheese chosen at the uncooked station
                    subStates[subIndex] = IN_LINE_TOASTER;
                    subAtUncooked = false;
                    curBreadChosen = -1;
                    curMeatChosen = -1;
                    curCheeseChosen = -1;

                    //Scale the sub and move it so that it is showing on the left of the screen
                    subBreadRecs[subIndex][BOT_BUN] = breadInLineToasterRec;
                    SetSubFillingRecsLocation(subNums[subIndex], subScalerAtToaster);

                    //Add the sub in line at the toaster station
                    subsInLineAtToaster.Add(subNums[subIndex]);

                    //Remove the garbage button from the screen
                    uncookedGarbageBtnRec.X = screenWidth;
                }
                else
                {
                    //Change the state to the customer's table and start the customer table timer
                    playState = CUST_TABLE;
                    custTableTimer.ResetTimer(true);

                    //Set the sub in the center of the screen
                    subBreadRecs[subIndex][BOT_BUN].X = buildingBreadRecs[BOT_BUN].X;
                    subBreadRecs[subIndex][TOP_BUN].X = buildingBreadRecs[BOT_BUN].X;
                    SetSubFillingRecsLocation(subsAtCooked[0], 1f);

                    //Play the success or fail sound effet depending on if the score is at a passing mark
                    if (curAvgSubScore >= PASS)
                    {
                        //Play the success sound
                        successSnd.CreateInstance().Play();
                    }
                    else
                    {
                        //Play the fail sound
                        failSnd.CreateInstance().Play();
                    }

                    //Remove the garbage button from the screen
                    cookedGarbageBtnRec.X = screenWidth;
                }
            }
        }

        private void UpdateToasterSlidingAway(GameTime gameTime)
        {
            //Move every sub that is in the away line from the toaster
            for (int i = 0; i < subsAwayLineToaster.Count; i++)
            {
                //Move the subs to the right of the screen and set it at the cooked station if it is out of view
                if (subBreadRecs[subNums.IndexOf(subsAwayLineToaster[i])][BOT_BUN].X - meatImgs[CHICKEN].Width / 2 * subScalerAtToaster > screenWidth)
                {
                    //Add the sub to the cooked station line and set its state to at the cooked state
                    subsAtCooked.Add(subsAwayLineToaster[i]);
                    subStates[subNums.IndexOf(subsAwayLineToaster[i])] = IN_LINE_COOKED;

                    //Remove the sub from the line away from the toaster and remove its speed
                    subsAwayLineToaster.RemoveAt(0);
                    toastedBreadTrueLocsX.RemoveAt(0);
                    smallSubSlideSpeed.RemoveAt(0);
                }
                else
                {
                    //Increase the sub slide speed by the push acceleration
                    smallSubSlideSpeed[i] += (float)(PUSH * gameTime.ElapsedGameTime.TotalSeconds);

                    //Increase the true location of the bread
                    toastedBreadTrueLocsX[i] += smallSubSlideSpeed[i];

                    //Set the bread and its filling rectangles
                    subBreadRecs[subNums.IndexOf(subsAwayLineToaster[i])][BOT_BUN].X = (int)toastedBreadTrueLocsX[i];
                    SetSubFillingRecsLocation(subsAwayLineToaster[i], subScalerAtToaster);
                }
            }
        }

        private void ResetSprinklingBar(int maxPieces, int perfectAmnt)
        {
            //Reset the sprinkling bar to its right x location
            sprinklingBarRecs[EMPTY_BAR].X = sauceContRec.X;
            sprinklingBarRecs[FULL_BAR].X = sauceContRec.X;

            //Set the width of the full bar to 0 and set the perfect line of the rectangle at the location depending on the perfect amount of the pieces and the maxmum allowed amount on the sub
            sprinklingBarRecs[FULL_BAR].Width = 0;
            sprinklingBarLineRec.X = (int)(sauceContRec.X + sprinklingBarRecs[EMPTY_BAR].Width * (double)perfectAmnt * BREAD_ZONES / maxPieces);
        }

        private void RemoveSprinklingBar()
        {
            //Set all parts of the bar off screen
            sprinklingBarLineRec.X = screenWidth;
            sprinklingBarRecs[EMPTY_BAR].X = screenWidth;
            sprinklingBarRecs[FULL_BAR].X = screenWidth;
        }

        private void RandomizeCustomers()
        {
            //Create a customer pool
            List<int> custPool = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 }; 

            //Randomize the customer appearances
            for (int i = 0; i < totalNumCust; i++)
            {
                //Randomize the appearance of the customers in line
                custAppearances[i] = custPool[rng.Next(0, custPool.Count)];
                custLocs[custAppearances[i]].X = screenWidth;
                custPool.Remove(custAppearances[i]);
            }            
        }

        private void ChooseIngredient(int numChoices, Rectangle[] ingrContRecs, ref int curIngrChosen, ref List<int> subIngrs, int subIndexAtStation, ref string remainPiecesText, ref Vector2 remainPiecesLoc, int[] maxIngrPieces, ref List<Vector2[]> ingrBreadDists, int step)
        {
            //Check for a mouse click on either one of the meat containers
            for (int i = 0; i < numChoices; i++)
            {
                //Check for a mouse click if the mouse is hovering over the container
                if (ingrContRecs[i].Contains(mouse.Position))
                {
                    //Choose the meat if the mouse clicked on the container
                    if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
                    {
                        //Play the select sound effect 
                        selectSnd.CreateInstance().Play();

                        //Set the current sub meat that will be added to the sub
                        curIngrChosen = i;
                        
                        //Set the pieces remaining text
                        remainPiecesText = maxIngrPieces[curIngrChosen] + " / " + maxIngrPieces[curIngrChosen];

                        //Setup the remain pieces location and the length of the array of distances to the bread top left corner depending on the step of the sub
                        if (step != CHOOSING_VEGGIES)
                        {
                            //Set the sub's ingredient as the current ingredient chosen
                            subIngrs[subIngrs.Count - 1] = curIngrChosen;

                            //Center the remaining pieces text in the center
                            remainPiecesLoc.X = screenWidth / 2 - remainPiecesFont.MeasureString(remainPiecesText).X / 2;

                            //Set the length of the array of meat to bread distances
                            ingrBreadDists[ingrBreadDists.Count - 1] = new Vector2[maxIngrPieces[curIngrChosen]];
                        }
                        else
                        {
                            //Prepare for adding veggies depending on whether the veggie chosen is lettuce
                            if (curIngrChosen == LETTUCE)
                            {
                                //Clear the lettuce speeds, reset the sprinkling bar, and reset the lettuce shreds
                                lettuceSpeeds.Clear();
                                ResetSprinklingBar(maxVeggiePieces[LETTUCE], perfectLettuceAmnt);
                                SetupLettuceOrSauce(ref scooperRec);
                            }
                            else
                            {
                                //Center the remaining pieces text between the tomato container and the sauce container
                                remainPiecesLoc.X = (veggieContRecs[TOMATO].Right + sauceContRec.X) / 2 - remainPiecesFont.MeasureString(cookedRemainPiecesText).X / 2;

                                //Set the next layer spacing
                                nextLayerSpacing -= veggieSpacers[curVeggieChosen];
                            }

                            //Add the current veggie chosen to the list of veggies
                            subVeggies.Add(curVeggieChosen);

                            //Add a new list of vegetable rotations
                            veggieRotations.Add(new List<float>());

                            //Add a new list of rectangles for the veggie pieces and set the length of the array of veggie to bread distances
                            veggiePiecesRecs.Add(new List<Rectangle>());
                            veggieBreadDists.Add(new Vector2[maxVeggiePieces[curVeggieChosen]]);
                        }

                        //Switch to adding meat
                        subStates[subIndexAtStation]++;
                    }
                }
            }
        }

        //Pre: None
        //Post: None
        //Desc: Setup the money decimals
        private string RealignMoneyDecimals(double money)
        {
            //Set the tips text
            string moneyText = "$" + money;

            //Add a decimal if it does not contain one
            if (!moneyText.Contains("."))
            {
                //Add a decimal to the money text
                moneyText += ".";
            }
            
            //Add a 0 to the end of the money text if the money text is not rounded to 2 decimal places
            while (moneyText.IndexOf(".") > moneyText.Length - 3)
            {
                //Add a 0 to the end of the money text
                moneyText += "0";
            }

            return moneyText;
        }

        //Pre: None
        //Post: None
        //Desc: Setup the lettuce or sauce
        private void SetupLettuceOrSauce(ref Rectangle toolRec)
        {
            //Reset the fall buffer time
            fallBufferTime = 0;

            //Clear the true locs and is falling array and set wasted lettuce to 0
            lettuceOrDropFalling.Clear();
            lettuceOrDropTrueLocs.Clear();
            lettuceOrSauceFallLocs.Clear();

            //Set wasted peices to 0
            wastedPieces = 0;

            //Set the scooper location
            toolRec.X = screenWidth / 2;
            toolRec.Y = TOP_SCREEN_SPACE;
        }
    }
}
