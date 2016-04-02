﻿#r "../packages/Newtonsoft.Json.8.0.3/lib/portable-net45+wp80+win8+wpa81+dnxcore50/Newtonsoft.Json.dll"

open System
open Newtonsoft

type MarkovPair = {
    Word : string;
    Frequency : double;
    Cumulative : double;
}

let rec split (values : List<string * string>) (list : List<string>) : List<(string * string)> =
    match list with
    | x::y::xs -> split ((x,y) :: values) (y :: xs)
    | _ -> values

let matched (x : string) (pairs : List<(string * string)>) =
        pairs |> List.filter ( fun (xx, _) -> x = xx ) 
              |> List.map (fun (_, yy) -> yy)

let frequency (value:string) (list:List<string>) : Option<(string * float)> =
    list |> List.filter ((=) value)
    |> List.countBy id
    |> List.tryFind (fun _ -> true)
    |> Option.map (fun (key, count) -> key, 100.0 * ( float count / float list.Length ))

let toMarkovPairs list : List<MarkovPair> =
    let (markovPairs, endFreq) = list 
                                |> List.distinct 
                                |> List.map(fun x -> frequency x list)
                                |> List.choose id
                                |> List.mapFold(fun state x -> 
                                    let (word, freq) = x                                    
                                    ({Word = word; Frequency = freq; Cumulative = state + freq},state + freq)) 0.0 
    markovPairs  

let getFreqTable (input_corpus : seq<string>) : Map<string, List<MarkovPair>> = 
    let tokens = input_corpus |> Seq.collect (fun line ->                                     
                                    line.Replace(".", " ")
                                        .Replace("!", " ")
                                        .Replace("?", " ")
                                        .Replace("\n", " ")
                                        .Replace("\r", " ")
                                        .Replace("\r\n", " ")
                                        .Split([|" "|], StringSplitOptions.RemoveEmptyEntries))   

    let pairs = split [] (Seq.toList tokens)            
    pairs |> List.map(fun (x, _) -> x, matched x pairs)        
                    |> List.map(fun (x, y) -> x, (toMarkovPairs y)) 
                    |> Map.ofList      

let ughCorpus = seq[""" How did they get it so wrong How could they remake the beloved s action classic Point Break with such ineptitude and so little affection
It fully deserves to be played on a loop silently in extremesports goods shops everywhere
Whereas the original was filled with humour and gungho machismo this Fast and Furiousstyle update is a joyless experience
We have no emotional connection with these characters nor do we care whether they survive or not
Steer well clear of this flatly disappointing remake as it seems hellbent on stripping out everything that made the original movie so much fun
Pointless Heartbreaking
Its so unbelievably selfimportant with occasionally genuine and hazardous stunt work badly compromised by the annoying ponderousness of it all meaning that we wind up simply not caring whos risking their lives or who wants to get into whose pants
An adrenalinefuelled but empty rehash that flies close enough to Bigelows movie to remind you how much youd much rather be watching the original
Director Ericson Core has ditched the fun of the Patrick SwayzeKeanu Reeves original and replaced it with soulless action scenes
The stunts are impressive The rest is a wash out
This is a highgrade action thriller that  dare I suggest it  redeems the Point Break brand And yes theres definitely room for a sequel
Point Break More like Point Miss
Braceys emotionally scarred Johnny Utah is so wooden he makes Keanu look like Kenneth Branagh
Were left scratching our head wondering what it was all for
Core revels in the beauty of nature and the danger of taming it but showing such is meaningless if the plot is so weak it barely strings the destinations together
Relentless action cannot dispel the toxic levels of claptrap that stink up this pofaced slowwitted remake
This iteration of Point Break effectively serves as a blueprint of exactly how not to reboot an action film
A complete failure Who wouldve thought Full review in Spanish
Point Break aspires to be a decent remake but winds up being one of the most boring action films ever made
If you like to see people pushing themselves to the physical brink parts of the movie will absolutely hold your attention If only the plot was that compelling
Director Carpignano has discovered a genuine performer with charisma and confidence in Koudous Seihon
Both topically and dramatically this is a film that matters more than most when immigration has reached an explosive status with people fleeing misery being blamed for the Paris terrorist attack
The film is unwaveringly attentive to problematizing the dividing line between predator and prey
A piercing character study whose narrow view frustrates complete empathy
Theres a specificity to Mediterranea that at times makes it feel like an actual documentary
Too much directorial obfuscation keeps this powerful refugee tale from being as potent as it could be
This calm hardheaded film never sacrifices its toughness for a swooning mistyeyed moment of hope
Carpignano glosses over much of the sociopolitical context in his depictions of the chain of events
Offers a deliberately muted finely textured account of the ordeals many Africans endure both before and after voyages to Europe in search of better lives
Mediterranea does not solely owe its topicality to recent events like the tragic sinking of an immigrant boat bound for Italy off the coast of Libya that resulted in  deaths it will induce a shiver of recognition for American viewers too
If youre the sort to wonder what it is that drives people to leave their homes to find work in other countries or even other continents then think of the film as an educational experience
Sad story about the struggles of two brothers from Burkina Faso who journey to Italy in hopes of finding a better life
This is a story packed with the sort of heartbreak and worry that would be hard to sit through were it not for American director Jonas Carpignanos touching tender narrative skill
Its a slowburn study in feeling powerless and unwelcome anchored by Seihons performance as a man patient and adept at sussing out and adapting to what others need until he just cant do it any longer
This debut feature from director Jonas Carpignano is often both harrowing and moving though this film festival favoritefalls short of some of the similar and even more tragic stories that can be heard just by turning on the news
Though realized with great empathy and tact the film fails to convey the catastrophic extent of the situation it addresses by keeping its narrative too tightly focused
A timely humanistic immigration film
This is as accessible and as lovable as avantgarde filmmaking ever gets
Offers a loving and unvarnished look at one of Oklahomas more eccentric stars
A poetic exploration of a moment a place and an artist
Whiskery and restless grooving and grotesque the documentarian Les Blanks longsuppressed film A Poem Is a Naked Person plays like your memories of some mad stoned lastcentury summer
A Poem Is A Naked Person is littered with striking moments that fit casually into Blanks study of fame and aspiration
Les Blanks longlost Leon Russell documentary is a beguiling snapshot of a lost era
And this is A Poem is a Naked Persons accomplishment composing a symphonic harmony of seemingly dissociated activities and subjects into one comprehensive elegantly organized and yes even poetic whole
An inside look at the s Leon Russell that is not much of an inside look at all
A perfect example of the downhome informality Blank made famous
A Poem Is a Naked Person is a tuneful peculiarity capturing singersongwriter Leon Russell and his bandmates his friends guest musicians and Oklahoma eccentrics
In essence the real audience for this are Leon Russell aficionados of which there are fewer today than in
Russell embodies history of Southern rock with roots proudly showing  Cultural heritage as living breathing rocking and rolling inspiration is wonderfully preserved
In the s this wouldve been an unusually intimate tour portrait Now its a newly unearthed time capsule the remarkable clarity of Blanks portrait compounded by the distance from which were looking at it
I like Russells music but I learned absolutely nothing about him
A Poem is a Naked Person is a trippy timecapsule celebration of Russell and his music and of Oklahoma and its people
You begin to understand the eccentric characters and amazing weirdness of a now lost America that Blank was celebrating
Blank isnt afraid of the artistic wrath that he may incur and rather than making a Leon Russell film has made what is defiantly a Les Blank film full of all of the idiosyncrasies that one would expect
This film was never released in theaters  it was obviously way too far out and ragged for its own good But it works nicely as a warped time capsule harkening back to strange days
Shot while hanging out with Russell across two years Poem wriggles with weirdness and smells to high heaven of its s roots
Shot in the early s but shelved for  years this portrait of RB great Leon Russell  immediately takes its place among the best rock docs
Therein lies Berardinis critique using naught but his subjects own words he paints a portrait of men who have been dazzled by profits at the expense of their ethics
Though at times not for the fainthearted Tom Swift and His Electric Rifle takes subject matter many of us probably give little thought to at all in day to day life and asks us to consider it deeply
An important account of shifts in policing tactics and the problematic interaction between law enforcement and private companies
Footage of the nowwealthy Smiths being deposed is damning the brothers legal jiujitsu is appalling and the stories of deaths are heartbreaking
Relying entirely on flawed cases and weak lawsuits from the mids Killing Them Safely is a very poor indictment of TASER International the manufacturer of the widely used and mostly safe electronic weapon
A dark comedy about humanitys knack for quickie solutions and our ability to lie to ourselves especially when our noble deeds go awry
Berardinis doc vigorously proves that despite their marketing Tasers are not the answer They are only means to a bigger deadlier problem
Mr Berardinis packed documentary makes its case early and often perhaps too often but its more chilling than your average issue film
With the intensified focus on use of force in police departments the unsettling documentary Killing Them Safely couldnt be timelier
Killing Them Safely is above all an example of excellent ethical fair and balanced journalism allowing both sides to state their case
With adjusted expectations the movie plays with surprising depth inspecting the redemption of a ruined life with care and attention to thespian detail
Solid and dependable Forsaken shouldnt be forgotten But I wish so much of it didnt feel halfremembered
It has a solid story to tell and tells it with no winks and few if any frills Its involving and ultimately exciting
The movie is too visually lovely to forsake but the predictable story and basic plot holes remain unforgiven
Screenwriter Brad Mirman follows the Western template as if it were an exact science
Attempts to be a slow burn thriller building to a climactic shootout are more Back to the Future III than High Noon but smaller character moments are deeply genuinely felt by the actors
Its hard to be overtly opinionated regarding Forsaken because the plodding uneventful little movie never asks more of you than basic consciousness its like the cinematic equivalent of cleaning out dryer lint
True we have seen almost all the elements in Forsaken in a hundred Westerns of the past But theyre assembled with such care that theres room for version
conveys a gritty style and features solid performances but most of the characters and themes seem recycled
So generic are these tribulations and story beats that they could have entered the realm of selfaware camp if they werent played so straight
Oldschool western fans wont find a lot of originality here but if youre looking for a wellexecuted straight genre exercise give it a shot
Forsaken greatly benefits from the poignant teaming of its fatherandson stars  as well as Michael Wincott as an especially elegant and eloquent gunfighter who has great respect for John
Though it borrows liberally from the classic Shane and doesnt really offer anything new this lowkey Western still works thanks to patient storytelling and a batch of strong performances
Father and son team up for a Western in the classic style
In the end there is nothing horribly wrong with Forsaken but there isnt all that much right about it either
Forsaken offers sufficient gun battles standoffs and shothimdead moments to keep fans of superficial Westerns entertained but its revenge story is unfalteringly bythebooks
Because of its intriguing emphasis on the strained fatherson relationship Forsaken ends up being a compelling drama as well as a wonderful addition to the Western genre
Forsaken is still a film to be enjoyed The performances themselves justify that
It should please those who like their westerns the oldfashioned way
In a standard coweringtownneedsagunfighter drama typical themes redemption forgiveness are laid out with little imagination
Exposed is a mess and completely forgettable but perhaps its more interesting as a lost movie with its true form caught between the demands of its financier and the vision of its original creator
In certain mutilated pictures you can detect the lineaments of greatness Consider Orson Welless The Magnificent Ambersons Here thats not the case
Exposed is confusing and very difficult to follow It ends up being a downer mess  despite interesting turns by Keanu Reeves and Ana de Armas
Awfully silly  and just plain awful
The confused heeldragging mystery drama Exposed suggests an especially dour arty episode of Law  Order SVU minus any reasons to keep watching
Cliched thriller mixes cop story supernatural elements
feels like two disparate ideas scrambled together in a way thats never convincing
Has a grimy offkilter charm not seen since the heyday of s exploitation
What a strange and frustrating mess this is
Exposed is a film suffering from blunt force trauma to the head
With Reeves in energysaving mode the troubled cop and corruption routines simmer but never come to the boil
Reeves appears to only have two expressions sad face and needapee face
What is being released is a baffling hodgepodge of a movie full of contradictory elements Bits of it seen in isolation are effective and atmospheric but the plotting is tangled and confused
Its clearly intended as a magicalrealist take on very genuine social conflicts but the result in this edit at least is a trial to sit through
To call it disjointed is an understatement Exposed is unintelligible It feels like two completely different movies inelegantly Frankensteined together
Incomprehensibly disjointed and stunningly dull
A grimfaced Keanu Reeves looks bewildered throughout Exposed and you can only sympathise
Carries a complex convoluted narrative that attempts to cover too much ground balancing a myriad of themes
Its fascinating to watch  not once was I bored  and superior to most other offerings on the big screen at the moment
It has all the Tarantinos characteristics and thats the reason this is a must for the filmmaker fans Full Review in Spanish
Theres politically incorrect humor and violence as well as an attractive cast but its too long to be perfect Full Review in Spanish
The Hateful Eight is a confirmation of an exaggerated and fuzzy Tarantino so determined to surprise us with his wit and shock us with his audacity and ends up sabotaging his interesting argument with excessive characters Full Review in Spanish
Tarantino brings a very bold political film disguised as a western that seems deep but at the same time can be enjoyed without any problems Full review in Spanish
Even though I dont think this is Tarantinos best film the director delivers and satisfies his fans Full Review in Spanish
Tarantino is a slicker and just cares about giving The Hateful Eight an air of quality Full review in Spanish
All these great actors the majestic landscapes and picturesque and mysterious characters wasted A Tarantinos mistake Full review in Spanish
The Hateful Eight doesnt try to make a point about race or the status quo in America today it doesnt even try to do it Its just interested in telling us a good violent and emotional story Full review in Spanish
As he becomes even wiser an orchestrator of scenarios characters and conclusions Tarantinos work turns more vicious in its timebomblike destruction
While not a quantum leap forward in his directing The Hateful Eight finds a new corner for the filmmaker to explore as both a writer and a director
Scenes do not gel into a whole nor do characters compel suspense is absent and early parody and irony have given way to outandout torture porn
The Hateful Eight is easily Tarantinos most fierce and contemporary film to date
The Hateful Eight sees Tarantino stake a claim as his own most passionate auteurist the film is a melting chamberpot of selfreferentiality and recreation
Its lenght and lack of that characteristical Tarantino shine of his more famous body of work make a rewatch of this film a necessary to develop a taste for it Full Review in Spanish
Its good and bad trashy and brilliant flimsy and substantial all at the same time And as for Tarantino Hes maddening frustrating totally unique
Bloated and hilariously selfindulgent
Tarantino is making his points and although its too long it is a spectacle to enjoy a mix between The Canterbury Tales Cluedo and spaghetti westerns
Just when you think you know what Quentin Tarantino will do next you realize just how wrong you are as evidenced by this great addition to the directors oeuvre
Its not exactly a culmination of Tarantinos work to date but it echoes themes and storytelling ideas weve seen him play with since Reservoir Dogs
The expose of brain damage risks to footballers is powerful stuff but the film never flies due to a messy screenplay that loses its focus and awkwardly interweaves an immigrant love story into the mix
I am a sucker for cause films that champion right against might but even allowing for that bias I can recommend Concussion as engaging cinematic grit
Its become all too easy for heavyweights like the NFL to forget the essence of sport win or lose its just a game but one which can physically and mentally scar the real people who play them
Feels like an outline spun by an ambitious Hollywood executive rather than anything resembling real life
Tthe events depicted are fascinating but feel inconclusive so its easy to leave feeling a little unfulfilled at the close of play
Relying in no small part on Smiths reliably firstrate performance Concussion is a thoughtful and interesting story neglectfully executed
The science shines brightest in this confronting true story that brings shocking new meaning to the phrase contact sport
Concussion takes a serious subject  brain damage caused to professional American football players and the collusion of the medical establishment in covering it up  and renders it in fullbore crashingbore Oscarcourting prestige drama mode
Viewers and not just retired footballers can all too easily forget what the movies even about
Smith still does a fine job of communicating the stubborn resilience of Omalu an admirably unapologetic whistleblower so committed to the CTE cause he paid for his lifesaving research out of his own pocket
In terms of leading some discourse about the safety of Americas most popular sport at least Concussion is in the stadium But Concussion doesnt really enter the game
Omalus dedication to his profession and his struggle to make himself heard and understood make for an inspirational story and Landesman effectively lays out the facts and invites the audience to judge both Omalu and his opponents
Smiths quietly tough performance gives Concussion its heart
The NFL may well feel gratified that the movie is as vague and weak as it is
Will Smiths performance is solid but Concussions plodding procedural nature turns the drama into a bit of a dogooder dirge
The film is worth for two reasons its theme and its main actor Full Review in Spanish
To see Will Smith seriously acting is refreshing he should try it more often Full Review in Spanish
The script also knows how to pull back when sentimentality threatens The films salutary ending shows just how its done
Told in fits and starts the film does convey a fair bit of information but it never clarifies what needs to be done about the continuing danger
Im not sure I would ever forbid a son of mine to play football But I would definitely insist he see Concussion before strapping on those pads
A fascinating and entertaining attempt to answer baseballs Big Question  Who was the fastest ever
Stands as further testament to baseballs status as our most chesslike sport and one that even when broken down to its tiniest component parts never loses its magic
For baseball fans it delivers the high heat For the nonfan there may be a little too much inside baseball
Fastball will change the way you watch the game without ever diminishing the sports mystery and grandeur
Theres nothing here that wouldnt have fit comfortably into an hourlong TV special and it starts to drag after a while
The irony of the push to speed up the game of baseball  the amount of time between pitches between commercial breaks between anything that might actually appeal to the iGeneration  is that speed has always been at the heart of the game
Filmmaker Jonathan Hock goes beyond anecdotes to the larger role of the fastball in baseball the way the game changed when the pursuit of velocities exceeding a batters reaction time became a Holy Grail
This appealing documentary makes you understand why aficionados regard baseball as a form of poetry
With Kevin Costner narrating Hock illuminates and entertains as he sketches portraits of the greats from original fireballer Walter Johnson to the indomitable Nolan Ryan
You dont have to be a baseball fanatic or for that matter a historian or a physicist to appreciate Fastball
Hocks documentary has a thrilling pop that should help it strike a competitive chord with anyone even remotely enchanted by our national pastime
Suitably solemn yet irresistibly lively documentary about the myth lore and logistics of baseballs classic pitch
The lively MLBproduced docs choice archival material and full roster of new interviews will delight fans both as a warmup to the  season and beyond
The sunniness of Fastball leaves out a lot but watching it can be as pleasurable as an afternoon at the ballpark
A fascinating and downright lovable documentary feature by Jonathan Hock
If you are not familiar with this level of baseball nerdiness then Fastball will be a revelation and hopefully an entertaining one If you are familiar then Fastball will satisfy on a deep and extremely specific level
One of the worst films to play Midnight Madness in ages
Baskin is Turkeys answer to a Rob Zombie movie which is meant as a compliment in this case
Take it or leave it but I was very happy to have partaken So I suspect will more than a few other pervy geeks
horrorhounds will relish the orgy of the damned that unfolds
surreal uncompromising bestial and eerily beautiful even if it is not despite what some unsuspecting viewers may believe named for a certain popular brand of ice cream Evrenols film sure is one haunting Turkish delight
Baskin excels beyond its vile visceral aesthetic into a puzzle of keyholes doors and fate
Visually Baskin is like a twisted sibling of Fulci and Argento by way of Cronenberg and Winding Refn
Baskin is the classic example of a film that fails because it cant support its carnage Im all for extreme blood and guts but it needs to have purpose
Baskin offers little in the way of narrative involvement or scares but doesnt stint on sustained stylized revulsion
Its initial promise dissipates in a muddle of repetitious phantasmagoria and too little narrative or character development
Baffles and bedazzles before blistering into a masterpiece
The film mostly functions as a tour of familiar horror tropes for much of its running time
Evrenol shoots all of this artfully imbuing everything from a police van broken down by the side of a road to a man having his intestines slowly pulled out with a certain eerie beauty
Its a testament to the power of Baskins imagery and atmosphere and the solid performances then that the shortcomings fall by the wayside for long stretches as we absorb this freaky world in which the protagonists find themselves
Tickling the mind even as it lurches the gut Baskin a stylish shapeshifting horror film from Turkey pulls a baitandswitch
Baskin is a perfectly imbalanced mix of chilly atmosphere heavyhanded symbolism and familiar horrormovie tropes
A torturegore blowout that rises above pure nausea with an intriguing blur of possible realities
The pacing is slack and the splatter excessive but this twisted crossgenre exercise should be red meat to gorehounds
The filmmakers handle their material efficiently but its hard to imagine anyone familiar with the genre finding Bleed fresh or as vividly scary as its predecessors
Bleed is familiar bythenumbers horror filmmaking but its also short and sweet enough to capitalize on cheap thrills
Too derivative and not stylish enough to merit any special championing as indie Bhorror movies go its nonetheless nicely crafted enough to rate a cut above the lowexpectation median
Reflecting influences ranging from The Texas Chainsaw Massacre to Rosemarys Baby to  well you name it  Bleed doesnt exactly break any new ground stylistically or otherwise
Bleed is really nothing you havent seen before but theres still some pretty interesting elements that result from its mixtape approach
A comedy thats neither clever enough nor sufficiently over the top
Despite its flagrant attempts to mimic what works in the comedies by Seth Rogen and Evan Goldberg its never particularly funny
The problem is that a movie like this  limp lazy generic  just doesnt cut it at a time when American indies and television offer sharp witty satisfyingly complex takes on the Facebook generation
Shot like a typically ugly Adam Sandler effort and so clumsily stitched together that it feels as if large plot chunks litter the editingroom floor
Not only is this ensemble unemployment comedy labored and witless but it bears the unmistakable scars of a protracted and ultimately failed struggle in the editing room
Its hard work finding work these days is it not Not according to Get a Job
At its sloppy heart this is meant to be an affirming movie but the filmmakers could have taken a cue from one line of dialogue Dont just feel special Be special
Clearly the economy has given Get a Job a reason to be sour But theres no excuse for being so sexist
The imperfect work mired in storage all this time gets a welldeserved spin
Get a Job is nothing special
Not only is this a movie without any guts it doesnt have much of a brain either
Predictable workplace comedy has drugs sex swearing
After a viewing its obvious why the producers lost interest in releasing it
Get A Job points to either massive studio compromise or a filmmaker who has somehow lost the mastery of his onceauspicious occupation
Get A Jobs primary problem is that it doesnt know if it wants to be a realistic look at millennials and the current economy or go for the cheap gag about the jivetalking pimp renting out a sleazy motel
This longshelved comedy proves a disappointing mix of onscreen talent uneven social satire and juvenile humor
What makes Get A Job so infuriatingly bad rather than the kind of film you hate and then completely forget about is the allstar cast that it has at its disposal and disgracefully wastes
A brutally cynical largely unfunny film fueled by muddled social commentary
Writerdirectors Micah Wright and Jay Lender are kidscartoon vets and show a facility for comedy on a more human level here  as does the nimble cast which ably handles the tonal shift from travel nightmare to actual nightmare
Is the film savaging cable TV Ethically challenged filmmakers Foundfootage horror itself If the intent was to finish off this subgenre by exposing the rote mechanics behind it the mission is only halfway accomplished
Wait you mean you wont be begging for Theyre Watchings cast to be sliced and diced How  refreshing
What we have here is a horror movie with all the expected trimmings Rumors of witchcraft An isolated house way out in the middle of spooky woods where no one can hear you  well you know how that goes
The storytelling becomes muddled in the middle and the suspense doesnt build as well as it ought to but the winking undercurrent keeps the film watchable
Viewers looking for the minimal amount of horror in their horror comedies will be appeased with Theyre Watching but the focus here is on laughs not chills  and that wont work for audiences seeking an equal balance between genres
Here comes one weird crackedout trippy horror tale that kicks into high screams at the minute mark
The movie doesnt do justice to a promising premise
Tired jokes and uninspired gore abound in what amounts to an unbearable experience and thats before the climactic bloodbath has a chance to disappoint with its subamateur special effects
Heres a minute foundfootage horror movie that would have worked better as an minute foundfootage Funny or Die comedy sketch
An hour and fifteen minutes of tedium is too long to wait for two or three minutes of pleasantly cheesy
A contemporary Blair Witch Project knockoff that feels significantly more dated than the film its inspired by
It makes some progress but it unsurprisingly stalls where so many found footage films have failed before
Neither very plausible nor scary this foundfootage exercise is nonetheless entertaining enough for a spell
So much better than last years plodding aimless Part
Part  is supposed to be the emotional crescendo to an epic love triangle but due a complete lack of chemistry between Lawrence and her callow male costars its all hollow and the goldembossed Super Happy Ending feels fraudulent DB
Affirms Katniss Everdeens status as the most significant of the reluctant teen heroes who have battled adult tyranny in the fantasy movie series of the past few decades and no Im not forgetting Luke Skywalker or Harry Potter
action and theme dont always cohere and whats good in the film too often has to be dug out from under a lot of underwhelming excess
Well the Hunger Games finally grew up at the end after all that silliness about the earthshattering importance of fashion statements We finally got one convincing romance to go with a real battle and real politics
This  minute conclusion results heavy and drags considerably due to its lack of what it promised Action emotion unrestrained rage Full Review in Spanish
It reminds us that every happy ending can also be a tragedy Full review in Spanish
While theres lots to recommend the movie the silly premise is the only thing I could focus on for the entire  hours and  minutes  which is inexcusably long for a movie that is actually just half of a movie
Relentlessly bleak from the first frame this final outing pulls no punches as white knuckle action sequences see the casualty rate rise alongside the tension
the last hour of Hunger Games  is good enough that deeper readings of this text would actually hold water in a way that previous attempts to politicize it have failed
Part  feels like it has a sense of direction that was lost after the first movie and its enjoyably more actionheavy
Its frustrating to watch the Jennifer Lawrence be marched from plot point to plot point just getting things over with
I understand that YA books succeed by transposing real teen concerns into lifeanddeath fantasy situations but any connection to the characters that existed by the end of s Catching Fire has been stripped away by these plodding Mockingjay movies
I remember struggling with Suzanne Collins final book It didnt have the same sense of urgency and excitement My thoughts on this movie are essentially the same
In this final film fans will get a satisfying resolution Its an emotional and dark journey illuminated by an otherworldly performance from Jennifer Lawrence
A satisfying conclusion to a brilliant series of films that show us the dangers of the oligarchy
Kudos to Lawrence for helping bring a satisfactory conclusion
The rare blockbuster that finds a compelling middle ground between thoughtfulness and big splashy spectacle
A fittingly serviceable end to a series that always had more potential than impact
provides a satisfying end to the story rather than an inspiring one It preserves the dark turn of the books finale the character of Katniss and the journey of Peeta to recover the compassion destroyed by Snow
First with the telephone then early cinema the magic of wireless radio and finally television Dreams Rewired bombards the senses with a thorough and clever montage of found footage from the s to the prewar era
The documentary isnt advancing an argument so much as simply restating a European socialistic breed of fact
An extended look sans nostalgia at how we used to envision ourselves and our future  and at how those of us alive now at what seems the apex of communication technology will look to everyone watching us in the future
Compilation of silent and sound films attempts to address fears about technology but proves excruciating instead
A lively visually enthralling attempt to gaze into the future by remembering the past
The ethereal essay provides a bounty of poetry in the form of a measured narration by international treasure Tilda Swinton and an extensively labored assembly of  blackandwhite film clips
The film really serves as a tribute to the then newfangled phenomenon of moving pictures  Its most persuasive and unspoken revelation is that filmmaking evolved concurrently in the Old World as it did in the United States
Its a feast for fans of early film but alas the narration is inescapable
As a documentary Dreams Rewired spends a long time circling various points without ever landing on one
The entertaining Tilda Swinton narrates from a highfalutin academic script but its the images that provide the fascination
Science fiction becomes reality in this funny and disturbing collection of filmed technohistory
Dreams Rewired isnt in the business of recovery or even analysis Instead it gestures it implies it signifies
Dreams Rewired is scattered by necessity and intent and it throws off enough sparks to set your brain reeling
The effect is more somnambulistic than stimulating and eventually youre less concerned about Big Brother peering into your home than you are about getting Tilda Swinton out of your head
Solid messages and mild scares are fun more toys for sale
a series of predictable and pedestrian scenarios including a morbidly obvious comparison between the two gentlemens organs of generation that will color your thinking about Patrick Stewart forever
While the pair of them look like they might just perhaps be able to make this highly improbable and dubiously conservative nonsense vaguely amusing they dont
The movie wont earn Ferrell any new fans but its a safe bet if you like his type of humor Full review in Spanish
Watch if you enjoy watching talented people waste their time and also enjoy wasting yours
Thats one problematic comedy
An odd mix of sentimental family warmth and grossout antics this comedy doesnt have the courage of its own convictions which means that its not quite funny enough to keep the audience fully entertained
The second pairing of Will Ferrell and Mark Wahlberg isnt as funny as their first offering The Other Guys but its still funny enough to warrant a look
It has its share of laughs even though they may not be memorable ones
What keeps Daddys Home watchable is Wahlbergs checkmate machismo as the intimidating foil necessary for Ferrells nambypambyism to register Its like watching Andy Sambergs SNL impersonation of tough guy Mark Wahlberg a selfparody of a spoof
Slapstick comedy that really doesnt do anything new for the genre Full review in Spanish
Predictably stupid and hopelessly obnoxious
There is no doubt that Will Ferrell and Mark Wahlberg connect on screen See them interact together has its charm but not enough to make us burst into laughter Full review in Spanish
The usual formula is used to make this a generic but fun comedy Full review in Spanish
Daddys Home insists on staying in common ground and to make matters worse his argument is unable to build paths that culminate in moments of laughter Full review in Spanish
Sadly not even the great performances cant save its awful script Full review in Spanish
Whats the target audience Its really innapropriate for a family film Full review in Spanish
Daddys Home manages to succeed thanks to Will Ferrell experienced comedian that once again stays in his comfort zone to do what he does best amuse others with his characteristic jokes comments and gestures Full Review in Spanish
Somewhere hidden in this film there is a good comedy Full Review in Spanish
This movie feel extremely long and repetitive despite its hourandahalf lenght something deeply worrying given that comedies are supposed to make you laugh which Daddys home fails at Full Review in Spanish
Its symptomatic of the productions limpness that the movie was shot for taxcredit reasons in New Orleans yet the locations have been scrubbed to a funkfree suburban anonymity
James White gets up close and personal in often discomfiting ways but its never exploitative or glib It hits the highs and the rock bottoms and all the damnable stuff in between
Filmmaker Josh Mond is as interested in his heros valleys as hiswell not peaks but the moments when hes less agitated even peaceful
Abbott contributes a smart soulful performance but Nixon keeps threatening to walk away with the movie as the mother who cant get enough of life and whose physical decay is colored by rage defiance and terror
Both Nixon and Mond invest James White with a raw honesty that makes James White a compelling drama about the demands and rewards of family
An accomplished and compelling film by writerdirector Josh Mond James White is also pretty much a bummer
Wisely James White leaves the unresolved troubles and feelings of its eponymous character unresolved
The acting is good in this film but watching it is like a depressing exhausting journey to nowhere
Its Abbott whos the revelation showing off all sorts of previously unseen leadingman potential
Veteran producer Josh Mond makes his featuredirecting debut here It elides as much as it shows but his sketches are mostly deft and always deeply physical
The experience of watching James White is like being shut up in a small dark airless room  a sickroom
Abbotts performance is serious and committed James White is constantly fuming in an interior dispute with the rest of the world
Most films dealing with illness and carer relationships follow the same emotional paths and miss the same points This film by contrast feels fresh and real even to those of us who have been there
Whether you want to spend time with James White depends on your tolerance for yet another film about how hard it is for guys who just feel too much
James White can be a chore to sit through but its never completely without merit
The movie is so engaging there are times when it becomes almost overwhelming But that is a glowing sign of how well the movie is acted and put together
Death does not become him
A lowkey but devastating drama with more raw authenticity than a hundred examples of Sundance landfill
This courageous film gets right up in the face of suffering and it doesnt flinch
Its not easy viewing by any means But it is strangely refreshing for a movie to show us that terminal illness involves agony and vomit and terror despite what Beaches might have told us
One will never wish to go through precisely what White does but losing ones parents is inevitable and the film is a searingly authentic portrait of the process
Writerdirector William Riead offers a highly simplified version of his subjects life
If Mother Teresa were here to see the film she would probably say You made this piece of garbage about me
This Mother Teresa biopic offers Hallmark Channelgrade inspiration of the most sluggish sort
A biopic about Mother Teresa could have easily been a selfimportant slog yet William Rieads The Letters proves a stirring and absorbing if not quite definitive drama
Struggles arent ignored here theyre just surmounted with patience and devotion That may be a good strategy in life but it can be static to watch on screen
Her accomplishments are even more impressive once we learn how fiercely she wrestled with God
Even Mother Teresas harshest detractors might say she deserves a better biopic than The Letters
A drama in which belief is reduced to wellmeaning but inert treacle
Bound to disappoint especially considering the absolutely wasted and bungled potential Quite possibly the most boring and dramatically inert movie youll see all year Full Content Review for Parents  Violence etc  also available
Underwhelming biopic about the extraordinary Mother Teresa
Her work made her one of the most celebrated figures of the th century but The Letters is far too preoccupied with the bureaucratic minutiae of her journey
Good intentions alone cant salvage this aggressively heavyhanded Mother Teresa biopic that perhaps could use some divine filmmaking intervention
This is a revealing study of a woman who was one thing to the world but something completely different to herself with a fine performance by Juliet Stevenson
Teresa is simply portrayed as a dedicated servant of God while whatever internal struggle she dealt with remains told not shown
Ms Stevenson is effective and credible in the role And The Letters is worth viewing for people of all faiths
The Letters is a beautiful and deeply moving tribute to the selfsacrificing Mother Teresa
A wartsandall biopic which reveals Mother Teresa as a tortured soul who felt abandoned by the same God she served so selflessly
The movies writing direction and acting are not alone in blowing Ciaran Hopes overwrought score sounds like a mashup of music from hokey old biblical pictures If a duller less inspired film hits the cineplex this season it will be a miracle
Its refreshing to encounter the occasional movie character who is blessedly devoid of guile For that matter its also a relief to attend a film where were not expected to hiss and throw our popcorn at the screen the moment a Catholic priest appears
A tale of two Mother Teresas On the brink of Vatican sainthood just announced holy healer or hells angel Expect an essentially nuancefree infomercial embrace of the former despite ironic inklings of nun feminism challenging male church authority
Not without its heated confrontations but it feels unnecessary working to depict the downfall of a man whos beaten them to the punch in terms of addressing his own selfdestructive tendencies
While perhaps not as deep as it could have been the movie is nonetheless a compelling examination of an unrepentant cheater
Despite a committed lead performance from Ben Foster Frears drama is an obvious and frustrating depiction of ambition and obsession
The Program feels ultimately more akin to boxticking than characterpowered drama
I had a hard time buying so much concentrated bile and for me the film loses an objectivity that might have otherwise scored points for the destructive nature of competition that can wreck a decent mans corrupted psyche
There is nothing remotely likable or even relatable about Lance Armstrong in The Program
Were left with the wellacted and welltold if familiar story of a man who knew better but couldnt stop himself
The Program is a highly watchable very enjoyable film but never truly manages to get to the heart of the issue
Frears squeezes tension out of these interstitial moments letting personalities rather than facts collide
Foster nails Armstrong right down to his final clenchedjaw TV confession to Oprah Winfrey but The Programs glancing narrative feels less secure
The Program much to its detriment concentrates almost exclusively on the history of the doping effort  There is no mention of his childhood or adolescence or any attempt to analyze his character
The most surprising thing about The Programis how narratively pedestrian it isfeels like territory thats already been welltrod or ridden
It never goes deep on what it was that produced the awfulness that is Lance Armstrong
The Program fails to add anything new or penetrate the soul of so crafty a cheater
For a story about incredibly focused determination  and if nothing else Lance Armstrong had that  the film remains strangely uninvolved
The Program reveals the tiniest details about Lance Armstrongs doping regimen Just dont expect to learn as much about the cyclist himself
The flashily photographed enterprise too often becomes a blur of sound bites and slick aesthetics akin to a Nike commercial
The Program is a solid primer to this fascinating riseandfall saga  one offering farreaching implications about corruption in sports and celebrity culture
Much like Frears other films The Program shines best in its performances Ben Foster disappears into his role as Armstrong
Flat Lance Armstrong biopic has drugs strong language
Toby and co come across as a pale antiintellectual imitation of the collegeaged friends in Donna Tartts The Secret History  a novel that explores the roots of its characters moral recklessness rather than just chalking it up to teenage feelings
Toby is so unselfaware that his journey seems like mere obtuseness what the film has to say about youthful degeneracy is less than zero
Nearly everything about The Preppie Connection  from the highschool class war to the flat explanatory narration  has been cribbed from other better films
This film fails even to evoke the s in costumes soundtrack or other atmospherics
A shallow and profoundly unexciting truelife account of one students brief reign as a campus drug lord
As written Toby is somewhat of an empty slate and Mann doesnt do much to fill in the blanks
A dramatized reallife scandal of s prepschool drug dealing plays like a tepid compilation of fictive cliches in The Preppie Connection
By the time quotes from Joseph Goebbels are used to explain Foxs methods The Brainwashing of My Dad has resorted to the tactics of its targets
A timely and thoughtprovoking portrait of the soulscape of America with its rage lack of civility and rightwing media machinations
Points fingers in all the right directions but fails to dig deep enough
Although Senko ultimately overplays Hillary Clintons vast rightwing conspiracy card her film with its timely Trumpian reverberations nevertheless serves up some compelling food for thought
Rightwing outlets may be exploiting humanitys ugly side but Ms Senkos fraillooking father who died in January at  isnt so much the face of the phenomenon as he is a small and not especially representative sample of it
Jen Senkos workmanlike Kickstarterfunded documentary examines American medias propagandaled phenomenon of extreme rightwing bigotry with a finetooth comb
Through anecdotal and social science research Senkos film also provides muchneeded insight as to why Donald Trumps caustic discourse and demagoguery is catnip for so many people
An entertaining look at why Fox News is setting the agenda for what passes for journalism in the US and a tool perhaps for deprogramming its adherents
A thoughtprovoking examination of the medias effect on Boobus Americana
Impressively tracks a family phenomenon via media mind controlled dittoheads brainwashed staged interventions angry white men and drive time as the geography of destiny But lacks further scrutiny into shadow corporate complicity and a tanked economy
Maybe I could say that if Christopher Guest rounded up his usual troupe and did a verbatim shotforshot remake it would be one of the funniest films of the year
In addition to an overreliance on Skype and silly effects such as laugh tracks  the films examination feels shallow
Paula Pells script is like a distended Saturday Night Live skit overworking one lame joke what if a bunch of fortysomethings threw the kind of party you only see in movies about hormonal teenagers
Fresh funny and heartfelt Sisters makes up for what it lacks in plot with a rolling succession of tearinducing jokes and a gaggle of hardpartying characters that you would actually want to buy a drink for
Theres a darker undertone to the story that is touched upon but not given the gravitas it requires
More wit would be welcome but Fey and Poehler keep things humming
Sisters is an uneven mix of raunchy farce and sincerity thats elevated by the undeniable chemistry between Tina Fey and Amy Poehler
Sisters is the FeyPoehler featurelength teamup many have been waiting for
Though Sisters does briefly achieve the right levels of relentlessly reckless humour at the height of the aforementioned party there are protracted spells either side of the main event where even a mild chuckle is hard to find
Its a bit like Animal House for girls but not as freewheeling
Is this the collabo between our brightest savviest comedy players that weve been dreaming of Not quite Its easy to want more from Sisters if only because Fey and Poehler have already given us so much
Sisters is a knucklehead comedy  sporadically amusing and always happy to resort to dick jokes  saved a little by the appeal of its leads
In what can be taken as an extended and belated apology for their woeful  comedy Baby Mama funny femmes Tina Fey  Rock and Amy Poehler Parks and Recreation team up as sisters for another girlsbehavingbadly slapstick comedy
Tina Fey and Amy Poehler are an unbeatable comedy team That doesnt make them infallible
Theres no shortage of comedic talent on screen yet Sisters plays a bit like a raunchy stand up routine loosely stitched together by a narrative that begs for more than crass outtoshock verbal torrent
Okay there are some solid oneliners including two  count em  tampon jokes and Maya Rudolph is reliably excellent as an uppity spoilsport Brinda But the best gags are few and far between and the movie starts to lag like a lengthy sitcom
While it wont be immortalised in the enduring annals of cinema it did trigger some unexpectedly visceral reactions from me
One of the most disappointing films Ive seen in a while
Agent One Hey we just signed Tina Fey and Amy Poehler to a movie about sisters who visit their childhood home only to find that their parents have sold it out from under them Agent Two Sounds great Whos doing the script Agent One Script
You feel like youre at that party that refuses to die and all the really interesting people are on the other side of the room doing funny things you strain to overhear
Even when Paula Pells screenplay falters the likeability of the central pairing is more than enough to carry the picture
This movie would have been better with a heavier focus on just these two rather than bringing in their SNL entourage
Despite the at times amateurish acting the film is in the end surprisingly touching and a reminder of the fragility of love
Nos film may not lack squelchy spectacle but when it comes to anything deeper it is oddly anticlimactic
After a slow start Nos selfreferential soap proves moreish
Everything is muted in Love When not shooting raunchy sex scenes Nos preferred framing is the mediumcloseup He keeps his camera near his characters but seems unable to elicit the true emotional essence of sexuality he claims to court
Its bold fleshy and audacious at least in theory But it is also numb
Despite being interesting its inconsistent and is both a demonstration of the courage of its director as well as its shortcomings Full Review in Spanish
A great movie but deffinately not for those who get offended easily Full review in Spanish
For a woman that know nothing about whats going on inside a guys head this can be the perfect guide to understanding the opposite sex Full review in Spanish
The blunt eroticism is going backwards always structurally and narratively backwards as in all films of No Full reviw in Spanish
Gaspar No took the fun in sex Full review in Spanish
Is interesting that Gaspar No still in the search to make audiences feel uncomfortable but curiously Love is may be one of his least transgressive works Full Review in Spanish
In theaters it has found rather indifferent response Full review in Spanish
You can watch Love in D but it wont add any dimensions to the characters or this script
An interesting way to talk about the different aspects of being in a relationship including sexuality Full review in Spanish
One of those films you have to see to believe but only for those not easily offended Full review in Spanish
Exciting sexy moving and painful Full review in Spanish
The problem here isnt really with the porn but with everything else which features the weakest dramaturgy this side of Deep Throat
The production values may be appreciably higher than your average porno but in terms of the quality of its plot dialogue and performances Love is not that far off
Karl Glusman has a future in Hollywood
Although masterfully acted by its three main actors the only new offering in this film responds to the natural need of Noe to cause a shock in the audience Full Review in Spanish
An informative and infectiously danceable history Latin pop history lesson
A joyous mix of social history and musicology
This is a documentary not just for the latin community or the Bronx community but for lovers of music artists of all kinds and folks who gain some sense of release from music they love
Slick and mildly provocative but overlong with excessive expository information Steve Jobs makes for a very captivating subject
The more interesting question Gibney poses and poses well is why we need to create a hero of someone who no doubt changed the world but who was more iconoclast that saint
If youre expecting solid answers youll be severely disappointed Ditto if you come expecting a hagiography In fact Im betting worshipers at the church of Jobs will be livid
For those who dont yet know of Jobs dark side Gibneys documentary will be a useful eyeopener but those looking to understand what made Jobs great in almost equal proportion to his nastiness will remain in the dark
Steve Jobs The Man In The Machine is a conversation starter loaded with controversial data about the lesserknown life of geniustyrant Steve Jobs but it asks a simple question of why that it cant even answer itself
Alex Gibneys twisty engrossing documentary Steve Jobs The Man in the Machine approaches its subject from an oblique but highly productive angle
While too long and with little new light on Jobs failings as a human being the questions it raises about his cult status and the way he helped change the world make it worthwhile
An attempt to understand the cult around a figure thats virtuous and revolting at the same time Full review in Spanish
Despite the movies journalistic substance the pleasurefree banality of its style gets in the way of a view of Jobs himself whose work is as much aesthetic as it is industrial
Coolly performs vivisection on a man a company an industry and a way of life
It simply isnt that easy to write off the Jobs extraordinary drive to succeed at all costs The difference between Gibneys documentary and other posthumous profiles of Jobs is that it doesnt necessarily maintain a reverential tone toward its subject
A thoughtprovoking and ethicallycharged documentary about the Apple computer entrepreneur
If GIbneys questions are not precisely yours his premise  that documentaries must ask questions rather than presume answers  is bracing reimagining not only what documentaries can do but also what belief can do
The picture is made with the consummate skill of the predominant documentarian of our age getting as close as conceivably possible to the essence of Jobs
This isnt a love letter or a takedown its a procedural justthefacts biography of a man who made a big impact on the world
Thoughtprovoking documentary on the iconic legendary Apple cofounder
Gibney doesnt offer a big final Sorkinstyle statement Instead he grapples with his own ambivalence about Jobs and his enormous social and technological legacy
Whether or not you care about Apple products or Steve Jobs Gibneys documentary offers a story well worth watching
A chilling portrait of an icon who remains revered for spearheading so many technological innovations despite his general contempt for humanity and his utter lack of people skills
Gibney is never able to join or understand the choir of millions singing the praises of Steve Jobs Perhaps because of this the documentary he has created seems a lot closer to the truth than anything else Ive seen about Jobs
Right from the start this involving documentary asks much of its audience and poses questions that are unnerving yet engrossing
What Our Fathers Did A Nazi Legacy wields a power that towers above many other small movies It may not be the large definition of cinematic but it is still a true film
the sensation of seeing men responsible for so much death relaxing with their families is unsettling while at the same time highlighting some small vestige of the humanity of both fathers
A troubling study of denial wartime responsibility and the challenge of dealing with a monster in the family
The men meet visit historical sites with Evans begin to battle I like you but I dont like your brains and the thoughts in your brains The contrast is bracing
Most troublingly images of Nazis in modern Ukraine in What Our Fathers Did A Nazi Legacy suggests that not being able to acknowledge that history paves the way for it to happen again
What starts out as a genial documentary about two sons of high officials of the Nazi Party soon turns chilling in the gripping and compelling What Our Fathers Did A Nazi Legacy
The film is not just about a very specific and difficult conversation Ultimately it is also about the failure of conversation itself
How do you cope with knowing your father was an architect of evil In this anguishinducing documentary two men struggle with that question in very different ways
What Our Fathers Did is a movie about historical and filial responsibility about repudiation about acceptance about the pain we inherit and the pain that continues to be doled out
My Nazi Legacy becomes horribly gripping
Wchter is a frightening example of how the denial common to fascist regimes can endure long afterward
Its a film that attempts the impossible to make us understand what its like to confront the fact that your father was responsible for the deaths of thousands upon thousands of innocent Jews during the Holocaust
A hard unsparing watch but rewarding in the way it pushes each man to find common ground in the darkest corner of history
Essential documentary viewing
A horribly gripping film
It entirely upends what I confess were my own preconceptions about what such a film would be that is a placid consensual study ruefully brooding on the sins of the fathers This is far more challenging  and more disturbing
What is it like to grow up as the son of a senior Nazi with atrocities on your family conscience In this powerful documentary the British lawyer Philippe Sands meets two men living in the shadow of the Third Reich in very different ways
A valuable examination of personal responsibility v familial loyalty set against events that should never be taken for granted
A fascinating doc about a couple sons of Nazi war criminals one ashamed of his fathers legacy the other stubbornly proud
Too much of Noma is composed of gorgeous pillow shots which grow static and fussy appearing to exist almost apart from the subject matter
Deschamps never ventures below the surface of Redzepis wildly successful experiment and while the pictures are pretty no one judges food on appearance alone
Slick but always sharp in the best contemporary European nonfiction fashion
Ive never seen a restaurant documentary that seemed less interested in showing the joy of food
Most scenes feel stagey with Redzepi feigning intimacy but sounding like hes auditioning to be the next Gordon Ramsay
The story of Ren Redzepi and his awardwinning restaurant Noma succeeds in spite of any directorial missteps
Less a documentary than a glittering souvenir but its still a record of a legend
Noma My Perfect Storm is crafted with exquisite care in the vein of its subject though it occasionally feels overly precious
Despite some mouthwatering shots of the presumably delicious menu items the approach combines a strange mix of ingredients that isnt for all tastes
The film forgoes narration for a naturalistic style putting viewers in the place theyre most curious about  the kitchen
The end result is a revealing portrait of an artist wholly dedicated to his art
Food is not an inherently cinematic subject being fundamentally about the sense of taste rather than that of sight But in its own terms this doco isnt too bad at all
Unfortunately you learn more about his struggles and his food in multiple episodes of Anthony Bourdains TV series No Reservations and The Mind of a Chef than you do in the  mostly flaccid minutes of Noma My Perfect Storm
Theres no reason this should run almost two hours
Ignore for a moment the generic title and the laborious first  minutes and you can appreciate this mostly heartfelt mostly sincere comedydrama about trying to outrun your smalltown past
Enjoyable deceptively simple story
Brooklyn lacks any real conflict The ingredients are what make it atractive however outstanding manufacturing and notable acting work by the its leads are not enough to save it from Crowleys bland directorial work Full review in Spanish
Saoirse Ronan creates an unforgettable character Full review in Spanish
A beautiful story about love and selfdiscovery a great production with great direction Full review in Spanish
It tells one womans tale beautifully The rest of her new countrys story however is left unfinished and unseen
Brooklyn is an excellent film for many reasons While it may take its title from one small part of the world it works so well because of the universal nature of its themes
Part of the films charm is the way that Irish director John Crowley manages the mood without compromising the momentum His direction is impeccable
Brooklyn doesnt just look back wistfully at the past it also transcends the period setting with powerfully timeless questions Where do I belong What can I make of my life
A subtle drama that work because of Saoirse Ronans acting work Full review in Spanish
Sincere emotional and even touching but its far from being a great film Full review in Spanish
While shes surrounded by a name cast this is Ronans film and while shes made big impressions before from Atonement to The Grand Budapest Hotel this is her most assured and moving characterisation yet
The immigrant story is one that has been told countless times over but director John Crowleys moving and funny Brooklyn brings a degree of empathy to this particular tale that is rare
A soft movie when it comes to immigration themes that allows her leading lady to shine Full review in Spanish
An impeccable romantic and realistic film Full review in Spanish
The film is certainly lovely and wellacted but the nerve it has obviously hit is not immediately obvious Perhaps its the films very modesty  its lack of pretense grandeur and histrionics  that accounts for its appeal
Everything seems to be in place without being memorable even once Full Review in Spanish
Brooklyn is a well acted and visually competent drama  but also an old school movie Full Review in Spanish
A film as understated but radiant and layered as its protagonist Brooklyn succeeds with a simple storyline whose emotional impact aptly hits home
Brooklyn is a superfluous romantic film thats entertaining but nothing more its dramatic parts arent captured with enough intensity to break down the audience Full Review in Spanish
This is Saoirse Ronans film Shes given a huge amount to do and asked to transform before our eyes from a callow floundering girl into an assured and confident woman She does so brilliantly and talk will turn to Oscars
It doesnt sound exciting but it is really well done with a great cast understated but powerful its genuinely affecting atmospheric often funny and very accessible
I dont know that McKay should go on being Hollywoods fiduciary moralist but hes clearly on the side of the angels and of the entertainers
Adam McKay gets that a lot of celebrities overact to simplify a embroiled story Full Review in Spanish
Smart and snappy this comedy is one of the scariest films of the year using humour to outline the  economic collapse from the inside
Wonderfully played although our heroes are far from noble and directed with great energy this is dreadfully enjoyable film
Theres no doubt that its the actors performances that carry the film to the awardnominated heights its been reaching as of late But despite all the praise for Bale who does indeed do a stellar job its Carell who shines brightest
A pseudodocumentary dressed up like a human interest ensemble when it should have felt more like a supervillain heist movie
At the end of the film I was hardly any the wiser as to the reasons eight million jobs and six million homes were lost in the US even though the skill of the actors and the films ambition remain impressive
McKays adaptation of Michael Lewis book presents the subprime loan crisis as a screwball comedy But perhaps the only same response to the loathsome skulduggery behind the  financial crash is to laugh at it
Adam McKay is passionate about the subject and The Big Short is exciting passionate filmmaking
An immensely entertaining and worthwhile document of Americas modern horror story
At first the tocamera segments can be discombobulating but over two hours the film coalesces into a brilliantly accessible scathing account of the financial crisis and its continuing aftermath
Itll make you angry but in a good way
It exposes the vast degree of lies and manipulation of the financial world in a far more entertaining way than any drama would
The film tries to take the socialist political stance of exposing the greed of the big American banks yet it has such a difficult time taking a measured stance towards its leading characters
Even if it is funny as it should be in parts it also has the capacity for drama and seriousness when dealing with the consequences of the crisis which makes this a smart piece of work Full Review in Spanish
In some ways its a flashier version of another Lewis adaptation Moneyball which made another dull topic  baseball statistics  surprisingly interesting
The very best that a satire can hope to achieve
The Big Short is the film that we needed at this time
Its a disaster movie  where the impending and continuous boombust cycle of capitalism is the oncoming disaster
Hanks as a stranger in a strange land gives us equal portions of laughs and insights into the worlds of both adults and adolescents Big also offers up a very funny satire of corporate ladder climbing
This elegant and perhaps very restrained presentation does just that presenting the case Discussing the rest is up to us Full review in Spanish
Carol is a meticulous lowkey reward for audiences
Focusing on the details that go from set design to excellent performances Carol is an exquisite film Full review in Spanish
Brilliant adaptation revised and refined from the romantic novel by Patricia Highsmith One of the best films of the year Full review in Spanish
Carol is a declaration of love to another film era but from a thoroughly modern perspective Full review in Spanish
Every piece fits perfectly to tell a story about love and passion but ultimately it feels like youve already seen this film Full review in Spanish
Carol is the most romantic movie of the year and through Rooney Maras performanceand Haynes careful directionit might also be the most cathartic
Its a delight to scrutinize every inch of Lachmans deep focus compositions and try and take in the abundance of exquisite detail
A love story told with looks and subtleties hand with hand with an extraordinary cinematography work Full review in Spanish
An elegant profound meassured and extraordinary crafted film Blanchett and Mara are hypnotic Full review in Spanish
In spite of being Hannes less ambitious cinematic film Carol offers the experience of putting you in the shoes of its characters and feel what they feel Full review in Spanish
Carol is a n excellent film full of warmth and romance
This is pure cinema which through its artistry opens a window into the souls of its characters and admittedly opens the closed windows of its viewers as well
What Carol captures more specifically than the thrill of a romantic encounter is the act of remembering only to forget
Carol is a movie worth watching to understand its main theme without fear of censorship and most of all to realize and understand that love is love regardless of genre Full Review in Spanish
Carol is a truly striking cinematic achievement artfully exploring themes that resonate still with powerful force today
The rhythm is slow but the film is never boring this is a story worthy of seeing in the big screen Full review in Spanish
As he did with his gloriously realized  domestic drama Far From Heaven director Todd Haynes once more brings a story of how in the pursuit of dreams even happy endings can come with collateral damage
The film still makes the viewer swoon its heady mood of love and longing generated by the briefest of glances and gestures
A perfectly acted perfectly sculpted perfectly rounded exploration of love and strife in the notsoperfect s
Sometimes I can almost hear the studio executives talking before a film gets the green light
Delivers precisely what is expected to its fansThe superhigh quality of the animation which places Alvin Theodore and Simon in the real world remains a real highlight in these innocuous adventures  it really is an unrecognised art
Its harmless enough but harmless garbage is still garbage
The Road Chip hardly qualifies as great cinema And really no adult goes to these films because they want to But it is a perfectly serviceable accompaniment to a choc top and bag of popcorn
Tediously silly slapstick revisit with the rollicking rodents
A movie that kids would love and adults can watch without getting bored Full review in Spanish
Even though the film is marketed as a road movie for kids it barely has any road in it and thats just the least of its flaws Full review in Spanish
A good movie for kids that adults can also enjoy Full review in Spanish
Fun time at the movies with lots of color and music totally forgettable but enjoyable Full review in Spanish
A sugarsweet nutty kind of chipmuck but cinematic excrement all the same
You might want to stick sharpened knitting needles in your ears
Instantly forgettable but inoffensive fluff you know for kids And inoffensive is better than can be said for many movies aimed at children
The franchise squeaks past with a soso sequel that barely improves on what came before Our only hope is that at some point theyll have to hibernate
Alvin and the Chipmunks The Road Chip is the fourth agonising instalment in the franchise that charts the adventures of three enormouslypunchable heliumvoiced rodents
If youre looking for something to amuse the kids on a cold windy afternoon then Road Chip is bearable enough  just But a fifth film would really be a stretch
One amusing airport security sketch aside this series must surely have had its chips
Ultimately fans of the franchise wont be disappointed while the rest of us will be quietly hoping that Homeland Security step in a finish the job
The absence of longstanding series stalwart David Cross is a shame Tony Hale cant quite fill the gap but Jason Lee is back making it business as usual
The final product quite amiable nonsense
Normally dependable comic actors are wasted in roles
An engrossing rebuttal to the sense that only Hollywood can do big kinetic entertainment
Writerdirectorstar Paul Gross spent a lot of time over in Afghanistan as he prepared to make Hyena Road and the verisimilitude of its combat sequences both on the ground and back at command centre is one of the payoffs
The film acknowledges political and moral quandaries in Canadas peacekeeping mission in Afghanistan but then applies the most disingenuous tropes from oldschool combat films and westerns to gloss over the grey areas
The conflict between acting strategically from the comfort of a military base and acting morally on the ground plays well in Hyena Road
Gross script realistically depicts the uncertain and frequently shifting alliances that characterized the Afghan conflicts security situation
Hyena Road has an agreeable modesty to it that almost makes up for what can sometimes be a very pedestrian treatment of contemporary warfare
The goal of the movie is truthtelling rather than flagwaving but it also succeeds as impactful storytelling
The war story featuring good people making the best of a bad situation may feel very familiar but it is a better kind of familiarity that also characterizes Hyena Road
Gross combat scenes are the appropriate kind of chaotic explosive rushes that tend to feature the soldiers trying to figure whats going on as much as actually fighting
The strategy was sound but the script proves clunky on screen because its clad in a heavy armor of thought and doesnt leave enough gaps for the subtle nuances and offhand gestures that make characters feel entirely real
Canadas own waronterror film cant get beyond embedding us among the troops GoProstyle shots military code and jargon to strike at any deep dark truths about the recent war in Afghanistan
Its one of the better films about the Afghan conflicts but it struggles to strike a balance between connecting with real soldiers experiences and contributing something interesting to the genre
While its not American Sniper it still seems like a missed opportunity
Paul Gross situates the films events somewhere between violent militaristic fantasy and gentler antiwar lament
Viewers are graced with a few disappointingly subdued action scenes two humdrum romantic subplots and a virtual museum of bad acting
The explosions are so wonderfully photographed if only we actually cared about the characters exploding
Gross  is ultimately unable to satisfy the rules of dramatic engagement
Hyena Road is a skillfully filmed reminder if one is needed about why Afghanistan earned its status as the graveyard of empires
Hyena Road runs two hours long but its entire narrative could be wedged into a halfhour documentary
Gross manages to craft a feature thats horrifying and strangely inviting at the same time delivering solid characterization to go with all the chaos
An entertaining story with an agile script easy to understand full of innocence and lots of warmth as well as comical situations that will surely make you laugh Full Review in Spanish
A film that attempts to show us the importance of being kind and noble embodied in Charlie Brown Full review in Spanish
An ideal movie for this time of year pleasing fans and also manages to present these beloved characters to a new audience Full Review in Spanish
Its mercifully unmodernised and pretty faithful to Charles M Schulz original comic strip but its also rather flat and not especially funny And theres too much padding it could just as well have been a short rather than featurelength
The Peanuts Movie is both modern and traditional pleasing on all fronts which must have been hard to achieve
Kids will be able to follow the simple storyline and will enjoy the misadventures of the goodnatured wellintentioned Charlie Brown
Snoopy Charlie Brown and friends finally arrive on the big screen in a movie that sticks close to the gently comical tone of the comic strip that launched in  and the vintage TV shows from the s and s
Can be a bit frantic and antic the endings more schmaltzy than Schulzy But the sadsack whatsthepointofitall adult humour sneaks in Pretty much the least disappointing featurefilm adaptation Americas most famous comicstrip could get
Anyone who possesses even a passing familiarity with Charlie Brown and the gang from Peanuts should find some appreciable measure of delight with this superfaithful feature film adaptationa perfectly fine perfectly safe bigscreen translation
An uplifting kids film without any of the cinisism found in most movies nowadays Full review in Spanish
What other films for children teach in an hour  about life the universe and everything  Schulz could teach in a line and this film reflects that Its undeniably about decency goodness and love And Snoopy
Emotional and lots of fun a great blast from the past and a new discovery for kids today Full review in Spanish
A charming film made especially for those who grew up in the comics Full review in Spanish
A film that works better as an homage rather than recreate the greatness of the original cartoon Full review in Spanish
If you are looking for an agreeable and entertaining school holidays film for younger children this is it Fans of the comic strip should get a kick out of it too
Its a sincere film for children squarely in the tradition of the sweet simply drawn Peanuts cartoons from the s
The Peanuts Movie manages to maintain the soul of the comic strip even as it upgrades the visuals
A flawless noble and tender family film Full Review in Spanish
True to the essence of its characters and animated in a more tridimensional way Peanuts is funny and has a great message for every age group and despite not having the most original plot it will satisfy longtime fans Full Review in Spanish
The movie knows where it lives as a precious piece of nostalgia in the minds of grownups who cuddled up to Snoopy as kids
This is dreary stuff
A flat sea shanty
A host of pictorially arresting even painterly images cant make a satisfying whole out of In the Heart of the Sea Ron Howards film that doesnt dig very deep its penetrating title notwitstanding
Howards film is predictable yet thats not to say its not enjoyable If you can get past Hemsworths mishmash of an accent part New England local part booming Thunder God there are plenty of impressive turns to be seen
The film delves into issues that discards little too fast or invents too late Full review in Spanish
In the Heart of the Sea is a technical marvel demonstrating that Howard is still a master of making movies look and sound stunning Unfortunately the screenplay was lost at sea
It has the basis of the whole tradition of sea adventure films Full review in Spanish
The story of the ship at the mercy of the great white whale told with amazing special effects grandiloquence and some common places Full review in Spanish
Unfortunately the films script is far too creaky to make the most of the mens runins while the all too obviously fake computergenerated effects suck the tension out of the showdowns between ship and beast
Youll be better off rereading MobyDick than watching this soggy adaptation of the source material
In The Heart of Sea show us the mastery of narrative Ron Howard has achieved through the real story that inspired Moby Dick Full review in Spanish
a clumsy behemoth inspiring a kind of dull awe at all the resources mounted for its realization
The images here lash and last the story creaks and groans Anthony Dod Mantles cinematography is brilliant its as if were looking back at this past through a watersloshed spyglass darkly The simpler storys too rote and modern though
Visually the film is great but the premise is forgettable and doesnt offer much Full review in Spanish
The whale moments are great but outside of them the film uses too much unnecessary CGI which takes away a lot of the films more real moments
Lacks in base thrills devoid of human drama and frustratingly crafted to wash away any inherent interest that might somehow have slipped through unmolested
The screenplay goes around in circles without creating an enriching experience the characters have no arc and what few moments could have been dramatic end up boring and full of cliches Full Review in Spanish
Ron Howards In the Heart of the Sea is a beautifullymade film that features stunning visual effects but its ultimately let down by a bland screenplay thats unable to bring out the exciting and epic nature of the story
Theres hardly a genuine moment and a framing device only adds another layer of dramatization to something that already plays like a bigbudget History Channel reenactment
The films most affecting scenes are of Herman Melville interviewing a survivor of the wreckThey sit in a small living room huddled as close as the survivors in a lifeboat  and as bound by the story of the Essex as was its illfated crew
You say goes completely off the rails like its a bad thing
A riff on the Hollywood conventions of a story we know very well already with little new to say James McAvoys mad scientist is fun to watch though
Unfortunately  just like the monster in question  the latest offering from director Paul McGuigan Lucky Number Slevin is a bit of a lumbering mess
This is not your dads Frankenstein more is the pity and time would be better spent watching the marvelous  original
Like the spark of life itself its hard to identify the elusive missing ingredient that prevents this ragbag of potentially likable body parts ever earning the accolade Its alive
It is a strange mix of intentional and unintentional laughs
The film repeatedly loses its charge falling back on dull franchise worldbuilding for sequels that will probably never be made What a shame
This new version adds little to nothing to the story we already know and have seen so many times Full review in Spanish
Victor Frankenstein lumbers and lurches rather like the monstrosity that the title character jolts to life with a bolt of lightning from the heavens
Old Meat
Add the film to the disturbingly long list of dreadful adaptations of a source that deserves better
The only time the movie comes to life so to speak is during its climax when Victor succeeds in bringing his creation to life and must deal with the subsequent rampage After slogging through  minutes of tedium to get there its hard to care
The screenplay for this movie is tedious boring and hard to believe Full review in Spanish
Victor Frankenstein is a spectacular movie Full review in Spanish
Unfortunately the end result has neither the subtlety of Sherlock nor the intellectual rigor of Mary Shelleys novel  its a hammy clunky misfire which largely squanders a strong core concept
In spite of being a great production wih a great cast the movie falls appart due to a bad script Full review in Spanish
It has so many illogical elements that it becomes an involuntary comedy Full review in Spanish
Any time you watch a reimagining of a story in the public domain you do so at your own peril
This schizophrenic lump of stitchedtogether cinematic remains hardly deserves the moniker alive
What slightly elevates the movie but doesnt quite redeem due to its almost  hour leght are its costumes and overall production design Full Review in Spanish
Tyler Labine carries a lot of the film against his straight man Crawford like the grizzled spawn of Seth Rogen and Jack Black  a jovial head in parkas and earflap toques
In Mountain Men Labine and his cast achieve a hardtostrike balance of comedy and heartfelt drama while maintaining a subtleness that respects your adult attention span
The script has plenty of salty language and dialogue that rings authentic The story is wellconstructed with a conclusion that will leave you smiling And the scenery in and around Revelstoke BC is awesome
Mountain Men delivers big heart some big laughs and authentic character development observed through sharp but always natural dialogue
Fleet and likeable
Though Topher forges ahead bravely its all too clear that hes headed toward an underwhelming conclusion
Writerdirector Cameron Labine finds a trail that feels reasonably fresh due in no small measure to the warm comedic talent of costar Tyler Labine
There are three good laughs in Mountain Men and two modestly dramatic sections That averages out to a decent moment every  minutes
While some of the interpersonal revelations and inner character struggle feel decidedly familiar treading the waters of the male comingofage tale the setting is novel and the added dramatics of their adventure freshen the story
A refreshingly heartfelt warm and tender film
hints at an intriguing character study yet despite some solid performances the script derails its momentum with generic and predictable thirdact twists
Richard Gere gives an annoying performance in The Benefactor  and it works
An unconvincing melodrama indicting moneys power to manipulate as the root of all evil
As a memorable work of cinema it misses every important mark by a mile
A runofthemill story of a junkie
If the conclusion is a little too sunny thats a small flaw in an otherwise compelling film about the hazards of trying to buy emotional connection
A compelling portrait of a man on the verge Suffers from a sense of rigidity that somehow both fits its themes and stymies a greater sense of realism
Despite great cast melodrama has little to say drug use
Has Richard Gere been hitting the bong
Theres a richness to the cinematography that along with the central performance hearkens back to a character study from a few decades prior
The Benefactor is both a bad film and a thoroughly inexplicable one
Gere is committed but his character becomes increasingly annoying with a onenote tortured past and the script leaves him and his underwritten costars stranded with a pat ending
The Benefactor is a character portrait in search of a movie
Renzi provides a platform for Gere to act in barnstorming fashion but cant work out what to do with any of the other characters They tend just to stand embarrassed on the sidelines as Gere holds forth and steals every scene in which he appears
The price of Geres good looks is his acting skills dont quite match them
Fanning criminally underused and James have so little meat to their roles they end up as ciphers while Gere has a ball as a drugaddicted tycoon on the verge of a nervous breakdown
The Benefactor loses whatever anarchic spark it may have had leaving us with an increasingly empty symphony of its stars repertoire of heavy breathing blinking and outofcontext laughing
Gere is very good as the largerthanlife Franny but the film is too slight to do his performance justice
Gere is watchable but constrained by a rushed screenplay that never gives us a proper handle on the man The potentially intriguing character dynamics fail to splutter into life
Presented in a rather unique way without feeling contrived when doing so
After this production every high school in America is going to want to take a shot at staging this Grease is the word
The ambitious  million production of the Rydell High School musical was as impressive as it was fun
The threehour production almost a third of it seemingly commercials was filled with marvelous moments like this theatrical reveals cinematic dissolves television tempos
The show wasnt perfect but it was great fun with touches of film nostalgia and musical theater earnestness
Regardless of its flaws Grease is a reason to look forward to the next round of live musicals on TV
This was a show that was more about individual moments than about building a story
It had all the infectiousness and emojibig smiles you need from three hours of live musical entertainment
Thanks to exceptional work from director Thomas Kail and several sterling supporting performances much of Grease Live was as sweet and tasty as a root beer float
An ambitious exuberant and eyeboggling adaptation of Grease
With this hectic ambitious and hormonal Grease Fox proved it could up the ante in the new miniindustry of musicals on TV
Fox raised the bar on TV musicals Sunday night with its ambitious wildly energetic and mostly entertaining Grease Live
It was a seamless vibrant energizing hit
Despite its innovative direction and talented cast Grease Live fell victim to its bland source material  and equally bland leads  leaving it unable to truly top other iterations of the modern TV musical
The show successfully managed to combine all the nostalgic elements of Grease that everyone expected while creating a new experience
Grease Live opted for a more cinematic approach connecting the dots between the livetotape soap operas of the s and the live music videos you see at contemporary MTV awards shows
Even within the constrains of a live televised eventwhich lets be clear Fox DOES know how to blow out when it wants toI couldnt help think that Grease Live was just a dud
High school itself is the nineteenfifties of our imagination always easier in our memories than it was in reality With Grease we can pretend we still want to go back
If Grease Live had more moments to work with like There Are Worse Things I Could Do it might have been truly special  but there was only so much it could do when the musical itself didnt have anything else to give
If you hate musicals and hate Grease Grease Live sure wasnt going to change your mind all of the sudden But for the multigenerational fanbase who admire this show there was plenty to love about Grease Live
Thrilling live musical is fun has iffy messages galore
Griffin relishes working in this lurid sanguinary Italian style and his enthusiasm is contagious
Funny animated sequel has cartoon jeopardy potty humor
Working in a surreally inflected vrit style  with few title cards or identifications other than what is spoken on screen  Mr Sauper also has a knack for catching his subjects in unguarded moments
We Come as Friends is a travelogue through hell one that weve come to know far too well wherever it is on the globe
A deliberately vague portrait of Sudan as a country beset by selfinterested neocolonialist outsiders
A a collection of stolen sights often dripping in gallows humor that builds to a subjective portrayal of a true dense rot
Sees Sudan as the epicenter of neocolonial competition between the US and China and can already see its about to lead to more war  in his pointed political travelogue
A riveting documentary that will bring your blood to a boil when you see how Africans are being exploited once again
Prepare to be emotionally engrossed and enraged by this alarming and searing expos on neocolonialism in South Sudan
We Come as Friends has been beautifully filmed with Saupers Godseye views of his planes wing and the African landscape below resembling something SaintExupry might have conjured
We Come as Friends aims a cinma vrit lens on a place where many promises are made but few are kept
Hubert Sauper goes where other people dont go sees what the crowd doesnt see and creates unsettling provocative political documentaries that are unlike anyone elses
The ongoing tragedy in Africa is too nefarious too complicated for any one film to do it justice but We Come as Friends opens a wide window into this mansion of horrors
The filmmaker uses his little plane to give us a birds eye view of a country struggling with keeping its independence against the odds
This beautifully shot documentary is an in depth examination of Sudan from myriad points of view
We Come As Friends Hubert Saupers teeming BreughelandBoschpursuing documentary portrait of chaos after colonialism in battletorn South Sudan is more eyewidening surreal sorrowful and anarchic than his earlier Darwins Nightmare
In case you thought that bad people werent still doing bad things in Africa Hubert Saupers disturbing documentary We Come as Friends will disabuse you of that notion tout de suite
The action always feels as if its unfolding in present tense the avantgarde score and disorienting extreme closeups conveying a sense of nervous spontaneous energy
A fascination with serendipity irony and absurdity like that in Werner Herzogs documentaries propels Friends into unexpected territory
Light on its feet yet deadserious in tone this excellent doc alternates micro to macro ground to air
Deeply unsettling and saddening a brief glimpse of a tanned George Clooney only solidifying the tragedy This is a pretty powerful work of nonfiction
Sauper and his twoman crew fly over a land thats becoming as alien to its indigenous population as it was and still is to those who fancy exploiting it
Theres a lot to like on a conceptual level but the execution is onenote and monotonous
Few would argue how powerful and effective this last adaptation turned out to be Full Review in Spanish
Too bad it also transmits a certain narrative tiredness because it follows too closely the original play Full Review in Spanish
Kurzel has put together an adaptation worthy of The Scottish Play but less powerful than the sum of its parts suggests Full Review in Spanish
Fassbender and Cotillard deliver mesmerising lead performances which are both compelling and realistic
Blunt dreamy and visceral new adaptation of the immortal work of William Shakespeare technically very lucid and brilliantly defended by Michael Fassbender and Marion Cotillard Full review in Spanish
It capture the spirit and the poetic greatness with remarkable eloquence Full revies in Spanish
This film is heavy into style and it was a style that will have selective appeal But the story is strong enough to hold the audience captive with what is as comes as no surprise a good
Fassbender and company do justice to the Bards Double Double Toil and Trouble classic
This is a powerful well acted and directed Justin Kurzel adaptation which gives unusual depth to the character of Lady Macbeth It gives additional force to the plays message about the evil and corruption which can result from great ambition
Striking visuals guide the way more breathlessly than any footnote could
Not all of your favorite scenes may be here but the ones that are will satisfy
Bold insightful and bracingly cinematic
The twohour film is full of synthesized sound and fury and what it signifies is not worth watching
A worthy Macbeth refresher course highlighted by Michael Fassbenders feverish Macbeth and Marion Cotillards simmering Lady Macbeth
Macbeth lives valiantly and dies soddenly by its decision to substitute so much of the Bards poetry with beautiful but empty visuals
Perhaps the fiercest cinematic translation of Shakespeare to date
The present edition in spite of an impressive cast is a blood bath that is more violent than any before but to no avail
Live by the sword die by the sword as the old saying goes and what a fantastic homage this is to one of historys best dramaturges Full Review in Spanish
Michael Fassbenders grave Macbeth is immaculate from the start he is forged on the battlefield and never seems to leave it
A bleak daring drama from Ukraine
If theres one place where love has no place its here
All of it makes for a unique cinematic experience an ambitious work that relies on its audience taking a leap of faith Those that do will be rewarded
There is nothing else like The Tribe at once a searing singular vision of a particular time and place and a brutal metaphor for the wounded human condition
The lack of spoken words or score also heightens the natural sound to a magnified impact in a film thats hardly lacking impact
An intriguing unique story done a disservice by some frustrating directorial choices Leaving it untranslated means we only get the broad strokes where I want details
Despite thelongueurs The Tribe is certainly a dark and powerful portrait of the grim goingson at a school where violence plays a far greater role than education told in a fashion that cant help but fascinate
It must overcome its gimmick or become a footnote Slaboshpitsky succeeds by using a language that crosses all social and cultural barriers violence Also sex
This is a challenging film thats not for everyone Yet theres no denying its brilliant concept and its raw cinematic power
The Tribe is constantly riveting and even if there is one other movie without spoken dialogue this year Shaun the Sheep Movie you still wont see anything else like it
What sets The Tribe from any other youth crime film is the way its told challenging the viewer every step of the way Full review in Spanish
The end result is like nothing else out there and while the violence that accompanies the characters fates requires a strong stomach this is a compelling experiment in pure cinema thats worth experiencing
The Tribe revels in the distance it leaves between its audience and its characters but in placing viewers on the outside it also creates an experience thats almost perversely empathetic
Its a movie Samuel Beckett would have loved exploding with language but existentially acknowledging both how little is communicated and how much humanity we share regardless
The Tribe shows that you dont need dialogue to communicate
While The Tribe often makes for troubling confrontational viewing it is ferociously engaging Stripped of dialogue this is cinema of highwire purity muscular precise emotionally complex  and surprisingly easy to follow
The movie features no music and no words yet some moments are so powerful and visceral that I still caught myself covering my ears
Whether Slaboshpytskiy can achieve anything so innovative after The Tribe may be an open question but on the basis of this debut his ability to handle powerful drama looks extremely assured even when its excruciating to watch
Easily the most intense movie experience of the the year The Tribe is an unsettling examination of how the oppressed can become the oppressor
This is pure cinema where words are not necessary the actions of the characters speak for themselves And they speak oh so loudly there is no way for the audience to cover their ears
Thanks to witty writing and excellent performances Break Point turns out to be one of the funniest sports comedies that weve seen in years
Jeremy Sisto and David Walton are well matched in this breezy likable tennis comedy
A mostly laughfree paintbynumbers approach to a pair of former pros vying for relevance as they enter kicking and screaming into their mid s
Easygoing and always likeable but hardly packed with laughs
The script plays like a random automatic serving machine Ideas bounce all over the place only to be quickly followed by more
Karas doesnt exactly reinvent the wheel as he puts this odd couple through the paces of getting in shape and reconciling old wounds but hes helped by some laughoutloud quirk
Break Point has its difficulties with storytelling and tone but it carries most of the way finding a rich sense of humor on and off the court
Its the kind of amiable but predictable trifle that nobody ever seeks out but that will mildly amuse everyone who happens to stumble onto it when it hits cable and the streaming services
Theres just too much dja vu at play on this court
Always nice to enjoy a little comfortfood movie in which almost nothing surprising or particularly fresh happens but were happy to spend time with the characters and we wish them the best as the credits roll
The affable shaggydog tennis comedy Break Point is bristly and charming just like its star and producer Jeremy Sisto who plays the affable shaggydog Jimmy Price a washedup semipro tennis player
Its a mildmannered film strange considering the ferocious nature of tennis itself the sport that supposedly holds Break Point together
Quietly moving comedy about tennis brotherhood language
Break Point looks good and it has a big serve but its hard to love wholeheartedly
Break Point may not be a great film but it certainly has a lot of heart This is a sweet flick
As far as tennis comedies go this one sits somewhere between WIMBLEDON and BALLS OUT Make of that what you will
Break Point is light and bouncy not unlike a tennis ball being lobbed back and forth You always know where its going yet theres a distinct pleasure in the fastpaced way it gets there
Break Point has its moments Its passable light entertainment but ultimately comes up short when reaching for deeper comedic or dramatic flair
Even if you manage to disentangle the many twists relatively early on there is still pleasure to be had here in witnessing how Pastolls bilingual screenplay allows crucial giveaways to get lost in translation
Interesting yet overlong melodramatic and fatally predictable Road Games is a backwoods horror film with a sheen of class respectability and Barbara Crampton These things go some way to save the day
Writerdirector Abner Pastoll delivers an accomplished tense and unpredictable thriller that makes the most of a strong cast
Nothing is quite what is seems on this psycho road trip and crazy revelations are eventually spilled
Its reasonably entertaining if not a tad by the numbers
Strippeddown thriller benefits from its setting
Fueled by strong performances and some genuinely riveting twists and turns its a solid genre film that delivers the goods in a largely fresh way
A wellacted but formulaic thriller
The cast doesnt quite succeed in keeping the suspense fresh throughout the storys left turns
Yes you can enjoy Road Games even if you arent a gorehound I wish I could recommend Road Games beyond that though since all roads lead to a twist ending that you can kindasorta make out from a mile off
Road Games is far too slight and simple for a premise that starts with a twisted promise Its more a board game than a chess match
A twisty little ride through the French countryside where everyone is suspect of playing hide the cheese knife
Though the inspiration here is clearly Hitchcockian the movies vibe is so tongueincheek as to be weightless
Despite that late inning stumble Road Games still provides enough suspense thrills and dark humor to leave genre buffs more than satisfied
Rather like watching a car wreck on the opposite side of a motorway
For all the amusingly fatuous remarks heard here  and Miss Spheeris has a great ear for these  the overriding dimness of most of the fans and musicians is frightening
This is a wellmade observant documentary with attitude to spare and plenty of justifiable laughs at the expense of its subjects
Theres so little respect for the music that we never see or hear a number from beginning to end and we rarely hear any of the musicians speak more than a few seconds at a time
Fascinating
benefits substantially from the periodic inclusion of electrifying moments
Tommy Oliver hits all the familiar notes in unfamiliar ways focusing on the impact of violence and exploitation
is a hamfisted morality tale about love marriage and the fallout of the s crack epidemic as though told by someone whose intel on all three came primarily from pulp sources
The film feels coolly detached because the story and characters are underdeveloped
Suitably lowkey but sometimes underrealized this drama is fueled by its workingclass milieu and a heartwrenching performance by Hill Harper
Meandering and inconsequential
Sorrentino wants to say something profound about illnesses that bury loved ones alive But the pompous lines kill the mood
Caine and Keitel are great together and Sorrentino delivers some typically gaspinducing visual flourishes But its also unmistakably indulgent and save for a few scenes doesnt quite deliver the insightful meditation on ageing it promises
Youth largely consists of a bunch of people rambling around a resort doing nothing But I cant think of a better bunch of people to ramble around and do nothing with
Youth is as psychologically savvy as it is beautiful
Sorrentinos Youth is sublime
It manages to be both pithy and pretentious
Paolo Sorrentinos script and direction are indulgent and extravagant his words and pictures thoughtfully considered for comedy character and quiet provocation
Caine is terrific  inscrutable and distant but evidently there are depths behind his oversized hornrimmed glasses
While Youth may not be his worst film it is his most pretentious and bombastic
Does Sorrentino attempt to tackle too much in this film Possibly though I would rather see a director experimenting with too many ideas than scraping the barrel with too few Youth is a rich and rewarding experience
Absurd but at the same time profound this is a rare movie find I have seen it twice and plan to see it again
Overwrought ninetenthlife crisis drama not even a great cast can create sympathy for the artistic and existential turning points on arty display
Youth might not be the most bedazzling of Sorrentinos films  then again how could one ever outdo The Great Beauty But it is indeed the directors most compassionate and affecting film to date
This new attempt by Italian director Sorrentino gets lost in translation Full review in Spanish
Sorrentinos lush cinematography captures the decadent beauty of wealth at the Swiss Alps resort but also the claustrophobic disconnected quality of spaces occupied by the hyperprivileged
How the hell did Paolo Sorrentinos latest not dominate awards season Spotlight and The Big Short are timely films Youth is timeless
Sorrentinos work isnt to everyones taste his films can be a bit langorous and obtuse so it depends on whether you find the payoff worth it If you do this is excellent
A decidedly mixed bag of a film varying wildly in quality on almost a scenebyscene basis
Artfully dreamlike and boasting towering performances by three legendary actors Youth is a mesmerizing meditation on age beauty and friendship
Despite the surface sheen and some enterprising plot twists it doesnt entirely convince
This British scifi punches well above its budget visually its a shame the drama cant match it
This doesnt come together but Trefgarne has clearly got talent
Theres simply too much going on to establish characters More upsettingly  being that this is a scifi film  its impossible to tell what the cool parts are supposed to be
Its obvious a lot of care time and creative passion went into it Its a relatively low budget project that does a great job pretending its not But behind the aesthetics there isnt much else
A strong if not genrechanging feature debut for actorturnedwriterdirector Justin Trefgarne
Narcopolis starts off intriguingly and ends solidly Its everything else in between that isnt particularly compelling
even if it doesnt ever rise to legendary status itself Legend is a fitting portrait of the twin gangsters who while they had their moment in the s have already begun to fade from view
What could have been a blazing entry into the pantheon of great British mob dramas is instead a dish of stale pudding
Its a movie that clearly states its goals but that doesnt accomplish them
As Reggies emotionally fragile wife Emily Browning is an oasis of sympathy amid the squalor the only person in the movie we really give a damn about
Hardy is an ensemble all by himself
Tom Hardy impresses mightily as hesplits up with himself Yet even with his skills Legend often misses the mark
While Legend definitely has its drawbacks you basically overlook or ignore them because of Hardy His characterizations are superb showing the contradictions and nuances of brotherly sociopaths who are laws unto themselves
A stupendous exercise of the criminal tale of period reconstruction of giving new life to a whole tradition Full Review in Spanish
Hardy is Legends saving grace valiantly dual acting in the roles of the very different twin brothers
Hardy and Emily Browning give excellent performances in this film cowritten and directed by Brian Helgeland  The story is compelling and the characters are interesting It is a solid historical drama
Tom Hardys double performance is worth the admission price Full review in Spanish
An average movie with an amazing performance by Tom Hardy Full review in Spanish
Even though the efforts to make the twins likeable and endearing characters are there we cant really empathize with them and the film feels just like another glorification of violence for violence sake Full review in Spanish
A dynamic tale where Tom Hardys acting chops become the center of attention Full review in Spanish
What stands out are the good performances that possibly just would like to fans of the gangster movies Full review in Spanish
Stretches a little too long but its a pretty good gangster film Full review in Spanish
Tom Hardys excellent performance is sadly not enough to pick up the work of director Brian Helgeland who is better suited writing great stories behind cameras Full Review in Spanish
The perfect setting and the outstanding work of Tom Hardy are the reasons to see the film Full review in Spanish
Tom Hardy sustain the film playing the notorious Kray twins Full review in Spanish
Legend is entertaining has good rhythm and makes good use of period music The problem is that it could have been so much more considering the material it was based on Full Review in Spanish
Its hard to imagine a world where Eddie Redmayne wont nab the Best Actor gong for the second year running
Eddie Redmaynes performance as Einar Wegener The Danish Girl is revealing heartbreaking and believable
Freshfaced British actor Eddie Redmayne here provides another sterling example of just how deeply he can immerse himself into a role
The films story is unique and brave and the two commanding performances give it a gripping emotional weight that is very affecting
That we never really glean who the Danish girl is is quite fitting since the filmmakers also havent the slightest idea who their characters really are
The acting is what makes this film
Numbing translation to film of a vital drama about the first transexual in history Full Review in Spanish
I wish the filmmakers had dwelled in the strange nature of Gerda and Lilis sexuality as they came to understand it together Instead it devolves into something a lot more sentimental and easy
Vikander delves beneath the surface to explore a truly fascinating woman And Redmayne is simply wonderful totally convincing both as Einar and as the Danish girl of the films title
Helped considerably by Vikanders strong playing and a lovely turn by Belgian actor Matthias Schoenaerts late of the remake of Far From The Madding Crowd as Einars old friend Hans Axgil this nevertheless hangs upon Redmayne
Its a subject that isnt often explored in mainstream cinema and so hopefully this film makes at the very least a small difference
The Danish Girl is a tale that however delicate humane and elegant is also excessively hackeneyed and ends up boring an audience that looks for more Full Review in Spanish
An earnestly told splendidly visualized film that could please the masses but will likely electrify few
Its commendable that Hooper The Kings Speech has brought Einar Wegener to a multiplex near you But this restrained elegant drama seems at odds with its pioneering subject
Tom Hooper still doesnt show any sign of evolution as a director Full review in Portuguese
Both Eddie Redmayne and Alicia Vikander shine on their own but their interaction and relationship on screen is the real enjoyment for the audience Full Review in Spanish
Loved The Danish Girl I hated Lili
Because of the simplicity with which The Danish Girl treats such a controversial subject matter the movie feels like a wasted opportunity to portray a character the deserves admiration for their courage Full Review in Spanish
The Danish Girl fails to give its full potential but it is good enough to recommend it with confidence Full review in Spanish
Vikander is excellent Redmayne is tremendous and Hooper does a great job harnessing the performers to get to the emotional truth of the story
A memory lane roach motel melancholy lapse into Greed Decade doom not unlike now as this bad boy veers in a slowly simmering psychologically dense portrait between victim and villain While toying with audience senses and his chosen targets as well
A yearold sociopath stubbornly fails to become scary in this stillborn thriller
By the time the film goes out in a blazing inferno of hellraising loneliness and tops itself off with the best final linetocredit song combo since Killer Joe The Boy has reached a point of stupid fun
The scariest aspect of The Boy is the extent to which Macneill makes it possible to sympathize with the troubled protagonist  even as its haunting final shot hints at the horrors yet to come
The Boys most disturbing facet is the possibilities it imagines
The director tries to generate a pace that his dramatic efforts fail to match
While its admirable that director Macneill and his coscripter Clay McLeod Chapman opted to emphasize mood and psychology over the storys more exploitable elements it nonetheless results in a listless tedium
While the score goes out of its way to make his every action feel sinister the picture doesnt fulfill its horrific potential until the third act
Craig William Macneills film is a sporadically frightening slow burn with a fatally overlong fuse
The film feels overly long and while lingering shots of the mountain scenery do help convey the isolation of the deserted motel too many of them feel repetitive
Mr Macneill and his coscreenwriter Clay McLeod Chapman have developed a feature stunning to behold if somewhat unpersuasive in narrative
Chapman and Macneill have a truly chilling character whose evolution will be both fascinating and frightening to watch unfold
The Boy is a very slow burn one that successfully works to the narrative at hand but isnt particularly enjoyable to watch
The Boy is a title that makes this movie sound innocuous A more fitting header would be Portrait of the Serial Killer as a Young Boy
Not since Henry Portrait of a Serial Killer has a movie gotten inside the head of a killer with such coldblooded artistry
It makes no bones about the fact that there is something critically broken in Ted The question for the audience is whether it is nature nurture or a mixture of both
It succeeds in conveying the dark edge of an effective thriller but it lacks the human sentiment  the poignancy the devastation  that wouldve made it soar above less heady genre fare
An austere and chilling portrait of Americas abandoned margins The Boy is a slowburner that builds and builds to its climactic conflagration and offers a dark disturbing flipside to Richard Linklaters Boyhood
In the march to the end Miss You Already grinds its gears through the late stages of cancer following a predictable and cliche path Each scene is a yank on the heartstrings a poke in the tear ducts its intents and machinations plainly obvious
A whimsical dramedy that induces the same feeling as eating a cup of Pinkberry or sharing a bottle of chardonnay with a friend and watching E
Miss You Already possesses a chemistry thats worthy enough for those in the mood for a downishnote friendship drama but its an experience Ill probably never subject myself to again  a strange sign of approval but approval nonetheless
It winningly reflects how to utilize quiet understandings and yes very loud laughter
Despite the earnest approach from its two stars who also serve with Christopher Smith as producers there isnt anything fresh in the story
The script from firsttime feature writer Morwenna Banks lacks grit and complexity attributes Hardwicke also disappointingly eschews in playing it safe with trite scenes
This heartfelt tribute to female bonding is steered in predictable directions more eager to jerk tears than explore new ground
Is this really an honest portrait of a beautiful and intense friendship between women Whats intense about it or beautiful or honest
Theres a jagged energy and a rawness to it Miss You Already jerks its tears honestly
The script has the excruciating familiarity and predictability of a galpal tearjerker weve seen a hundred times already all thats missing is to have Barrymore burst into a chorus of Wind Beneath My Wings
The movie has the tone of a liveaction Cosmopolitan magazine pushing the viewer to make judgments about people and behaviours urging you to invest in the drama from an ego point of view first
Theres a toughminded drama struggling to break through the movies glossy veneera contemplation of the black hole of death that sooner or later becomes the center of life
A jarringly jolly weeper whose save it from the saccharine
Collette and Barrymore capture the bond between two women who love each other unconditionally for better or worse
Its a proposal about feelings and finds its audience in every person who want to be moved have a good time and even reflect a little Full Review in Spanish
Screenwriter Banks redeems herself with a welljudged final act that tugs our heartstrings without feeling like were being shamelessly manipulated into reaching for another tissue
Its the gallows humour delivered with warmth by Collette Drew Barrymore Dominic Cooper and Paddy Considine that prevents this becoming unbearably maudlin
An intelligent script that mixes drama and comedy in a subtle way making this a visual and emotional delight Full review in Spanish
A film that doesnt have a perfect story but achieves its goal of showing real life in a moving way without being preachy Full review in Spanish
Miss You Already recontextualizes beats of the cancer movie subgenre into a story about the bulletproof power of female friendship
A fascinating portrait of a man some would call driven and others may call psychotic
Fighting bulls Its a tough job but does someone really have to do it
This documentary captures what may be the last of Antonio Barreras bullfighting days at a time when the sport itself seems about to fade into history
Its rather hard to feel wholly sympathetic towards the quixotic bullfighter who at one point declares Ive never had a relationship even with a woman as intimate as the one I have with a bull
Fascinating and oddly inspiring Gored is too focused on Barrera the myth to ever really focus on Barrera the man
Trivedi and Naqvi put together this multitentacled story using a daunting variety of footage news reports and archival film mixed with interviews It paints an extremely grim picture
You cant help but be impressed if aghast at the lengths some matadors will go to please an audience
even at a mere  minutes this feature feels padded and overlong  and the ingenious way that the writerdirector contrives to stage events before Emmas various devices also sadly palls through repetition In short needs more slashing
Sucker is likable geeky and gently funny rather than hilarious
Spall is the films greatest asset but Lucs performance is a more harmonious fit tonally Like the film he is pleasant unprepossessing and nothing to write home about
Sucker errs by not spending enough screentime devising and carrying off creative cons instead it invests too much in substories that are never fleshed out nor as engrossing as the cons these promising characters could have pulled off
Undemanding and ultimately irritating but gentle conman comedy from Australia
Whereas Lost in Thailand felt like a homage to Stephen Chows brand of slapstick Lost in Hong Kong looks to be an allencompassing love letter to Hong Kong filmmaking
Perhaps because it tries too hard to be too many things the movie loses its punch
Sappy crowd pleasing Hong Kong comedy
Sturdy wellacted but a mostly dull and unsurprising You cant go home melodrama
A Country Called Home never goes allin on the implied cornpone of its titlebut it never really goes allin on anything else either
Theres heart here for the taking but Axster turns a complicated domestic and emotional crisis into an episode of a network television drama
Lacks the clear vision to know how to distinguish itself from a hundred other similar movies
Theres exactly one fascinating original character in Anna Axsters wellmeaning but bland debut feature A Country Called Home Unfortunately the movies not about him
The rigorously dull A Country Called Home hates flyover country more than Bill Maher
The third act in particular becomes an awkward mixture of broad comedy and pathos
Its refreshing to see Poots in a role such as this one that lets her easygoing charm shine through
The gentle drama A Country Called Home is in sync with its smalltown Texas setting Unfortunately much of it is also as flat as the terrain despite the efforts of an engaging cast led by Imogen Poots
While I found it sincere I felt like it meandered too much and suffered from anxiety over what kind of a film it wanted to be
A Country Called Home is a decent enough trip through the back roads of Texas connecting with a partly dysfunctional family Its just not something that most viewers will want to write home about
There is not a lot in the plot that is surprising or even original There is some wit and there is a not too unrealistic look at strained family relationships
Parents and grandparents shouldnt be surprised if afterward youngsters are sufficiently curious to Google info about the reallife Apollo moon missions that the movie playfully references
Works hard in an effort to recapture the fizzy thrills of actioncentric Pixar flicks like The Incredibles
For some Capture the Flag will just about scrape by on its honourable intentions and brisk delivery however what seems on paper like a vaguely educational distraction is merely another exercise in colourful chaos
Several of the characters are so grating you long for a lunar disaster to strand them as far away from planet Earth as possible
Capture The Flag has enough visual energy to get to the moon and back Alas the script blows harder than a solar wind
Its a feeble storyline and the film is only partially lifted by some lively action sequences and one or two Gravitylike scenes of the astronauts drifting in space
It may lack the finesse and charm of animation from Pixar Disney or DreamWorks but its a decent yarn for younger kids who want something to watch on a rainy day or school holiday
Aside from a fleeting postMinions Kubrick gag theres little here for the oldies and nothing that the Pixar generation wont have seen done before and better
So mindnumbingly stupid that any investment in the characters is next to impossible
Capture the Flag sits comfortably at the classier end of childrens fare for the start of
There is an inkling of a better film here but something appears to have got lost in translation making this quite the forgettable voyage
This colourful animation tries a little too hard to get down with the kids
A good family film with a positive message Full review in Spanish
One of those films kids can see over and over again and adults wont have to suffer when seeing it Full review in Spanish
Great animation too bad the story isnt that great or original Still pretty decent Full review in Spanish
It touches on the subject of keeping families together successfully Full review in Spanish
The film entertains and will certainly be enjoyed by children and adults with knowledge in films and pop culture Full review in Spanish
A good effort that achieves its low goals Full review in Spanish
They just dont make this kind of romantic trifle anymoreand for good reason
A combination romance farce and road movie that whiffs in all three departments
Lackluster motherdaughter comedy has teen drug use
The film ultimately boils down to people bludgeoning one another in unimaginative closeups
Like the house the movie is something of a shifting puzzle box if only the backstory that turns its gears werent so rotten
As for the homeinvasion angle there isnt quite enough to set it apart from the pack Much like the title Intruders is just a little too familiar Youre Next this is decidedly not
Intruders blends a few horror subgenres to create something complex and engrossing all while wearing influences from Adam Wingard like a badge of pride
Intruders ultimately comes across like basiccable schlock or is it Netflix schlock now slightly redeemed by the germ of a great idea even if said idea never truly germinates
Intruders doesnt provide a pounding viewing experience but its sharp where it counts holding to a rhythm of torment and confusion that keeps the picture engrossing and repeatedly unnerving
Intruders is tautly directed by firsttime feature filmmaker Adam Schindler
Intruders a distasteful thriller with a bludgeoning sensibility and little common sense turns a cozy family home into a clockwork house of horrors
The actors alone cant sustain Intruders for its full  minutes but for the most part they follow Starrs lead carrying a film thats both menacing and magnetic
The result is a promising film that leaves a bad taste in your mouth like a meal wellpresented on the plate that just doesnt fill you up
An efficiently engineered suspenser with solid performances and a tight pace
Wellacted and initially suspenseful before it turns increasingly implausible lazy and uninspired as it piles on the twists in unsatisfying ways
A highly competent suspenseful and fun thriller that never overstays its welcome Do it a favor and open the door
The film doesnt know if it wants to be an overthetop sensationalized shocker with cartoonishly evil villains or a more realistic gritty thriller with human characters and the end result lies in a muddied thrillless exasperating middle ground
Intruders at least tries to do something different and it does manage to keep things ticking over smoothly enough
Of the numerous DC LEGO films Cosmic Clash is one of the better entries in the franchise Just be prepared to make a trip to the closest LEGO retailer afterwards
Heroes timetravel and fight evil in funny inventive tale
An engaging and thoughtful snapshot of Deans life enlivened by a pair of superb performances and stunning production design
Beautifully written and directed this factbased drama is an odd mixture of excellent acting and notquiteright casting
As a portrait of James Dean Life only manages to capture his soft stolid side
Its odd that Corbijn a gifted still photographer in his own right has so little to say about the relationship between shooter and subject or the impermanence of celebrity
While the script is sometimes too heavy footed on the whole Life has an unassuming quality that wears well over the course of its two hours
Life misses the mark by perhaps a sixtieth of a second but thats enough
It ends up demystifying Dean perhaps by accident but no less regrettably
I loved Ben Kingsleys overthetop work as studio head Jack Warner who in one scene explains the lay of the land to Mr Dean in a manner that would inspire envy from Don Corleone This guy isnt messing around
Dig if you will the pictures but you dont need Life as a stargazing aid
The actors and their exchanges ring true and by the time the film reaches its lonesome conclusion the resonances are eerie
A moody leisurely and occasionally frustrating piece of work
Flat James Dean biopic has swearing some nudity
Both Pattinson and DeHaan could have used more to do but both actors put in performances that elevate the proceedings
Corbijn is great at taking reallife flesh and blood people and alchemically rendering them in striking dimensional images that transcend and mythologize the reality but Life shows him fall some way short of achieving the reverse
Theres not much to go on here
Pattinson and DeHaan are both strong portraying real people with a mix of imitation and individuality
It really is the work of Pattinson and even more so DeHaan that makes Life a success
As the films emotional anchor Stock isnt nearly as fascinating by comparison
For Dean aficionados this is a wonderful way to get the backstory on some of the most iconic images we have of the star
Life turned out to be as bland as its title
Its an entertaining romp and one of the funniest Christmas comedies weve seen for years
As with the appalling Pyongyangset The Interview of last year The Night Before relies on swearing stereotypes causing offence and adolescent sexual jokes for its laughs
Like your old Christmas jumper Rogen is feeling a little worn
Id go so far as to say that The Night Before might turn into one of those yuletide favorites the whole family will go on to enjoy
Loaded with party drugs and with Rogen putting in yet another of his rote manchild performances  really Seth enough already  its a pleasant enough timekiller though all the improvised scenes with overlapping dialogue are a little hard to make out
Top marks to Jillian Bell and Michael Shannon for briefly raising the overall tone brickbats to James Franco for bringing it right back down again
There isnt enough story to sustain the running time and it lacks howlingly funny setpieces
Its A Wonderful Life meets Pineapple Express in this stoner Christmas comedy
Its bogged down by too many derailing tangents but the three appealing leads have a wonderful chemistry and it gets close to the spirit of the season
In The Night Before which Levine directed and cowrote sweetness and crudity mingle from the outset
A film about how time changes friendships with hilarious jokes and humor to along with it Full review in Spanish
Unfortunately the moments designed as comic relief become the main focus of the film giving us cheap laughs that stray away from the main plot Full review in Spanish
Michael Shannons name isnt on the movie poster but The Night Before wouldnt be as memorable or funny without him
This blending of the stoner bromance with the Christmas comedy works surprisingly well layering grossout humour with holiday sentimentality
A hilarious Christmas comedy that deftly mixes drugfueled humor with holiday sentimentality
Theres a glimmer of genuine sweetness beneath the tomfoolery in The Night Before which occasionally casts a warm glow over characters as they learn valuable lessons about the power of friendship to overcome every obstacle
Such a ragged staggering letitallhangout brocomedy that the women feel parachuted in Strains to be a Christmas movie for s babies now all grown up but umbilically attached to their smartphones
Theres small pockets of convincing dialogue between the friends but this clashes with random chaotic set pieces
The Night Before might well have called Christmas Carol on drugs Full Review in Spanish
In the midst of an otherwise bythebook boyswillbeboys tale its one hell of Christmas cracker surprise
Guy Maddin delivers another of his wild and whimsical fantasies tinged with camp and couched in the film grammar of silent cinema
The Forbidden Room is an absolutely manic joyous romp through a hilariously warped revision of seemingly the entirety of silent film an endlessly inventive celebration of the limitlessness and sheer dexterity of cinema from the first frame to the last
The tinted corroded images ripple pulsate and disintegrate They burn up then reconfigure as something else entirely The surface of the movie is liquid the actors seem to be drifting in an ocean of photographic developer fluid
It is sometimes brilliant and sometimes boring but even the boring parts have an eccentric sparkle
The Forbidden Room is a fun ride for cineastes and was much fted at festivals Whether it will appeal to mainstream taste with its insane mashup of B moviestyle parody silentmovie intertitles and jungle vampires is anyones guess
Maddin remains a unique treasure and The Forbidden Room is one hell of a trove
Exquisitely designed this cornucopia of melodramatic fragments and movie pastiches will enchant Guy Maddin fans
A visual phantasmagoria with a builtin propulsive energy
A grueling marathon of cinematic masturbation a mindnumbingly empty exercise in selfconscious style with absolutely nothing to say
Its an exercise in high kitsch which enraptures at first but becomes increasingly enervating the longer it lasts
If youre up for The Forbidden Room it fulfills its oneiric promise by leaving you with the vague impression that you dreamed it  and dreamed its dreams within dreams  even if you know you didnt
The screen pulsates like infernal internal organs or bubbles and mutates like melting celluloid jammed in a hot projector
Once again Maddin draws his inspiration from cinemas transitional phase between silent and sound and he is abetted wonderfully by production designer Galen Johnson and cinematographer Stphanie WeberBiron
An exhausting and overwhelming film to experience but theres also something exhilarating about its mad energy and boundless invention
What Maddin  Co have invented here ranges from Freudian horror to childish naughtiness
Theres a strange comfort in knowing that Freuds fascinating topography of the mind will never be completely discredited while Maddin is making deliriously absurd movies
Dense and lacking the playful quality of his more straightforward work this represents a new multinarrative direction for Maddin
Whilst The Forbidden Room is overlong and messy it is still a lightning bolt of creativity from one of the most enigmatic and compelling filmmakers working today
The bad part is that at over two hours long the film is not that good
Canadian iconoclast Guy Maddin has been making strange surreal films that evoke the images and storytelling traditions of silent movies for decades The Forbidden Room  is like a compendium of his obsessions and cinematic fetishes
Some generally witty lines are trampled by many more that are neither clever nor funny
Amy Ryan has a nice turn as Dons naive assistant but her humanity is more than this movie can handle
A joyless celebration of welltrod stereotypes in an even more joyless madcap lampoon
Sam Rockwell applies his usual deft touch to the title character whose bornagain ministry is founded on his dubious excavation of religious relics in Israel but whose charlatanism serves a sincere and abiding faith
The humor is very hitandmiss both because its wielding a rather blunt satirical weapon and because the sheer number of people Don fools becomes hard to swallow But when Don Verdeans barbs land they work
Hess eccentric characters are neither likable enough to root for nor ridiculous enough to earn big laughs
There is an absence of meanness in the Hesses comic worldview that makes their films almost impossible to dislike
There is a redemptive message in Verdeans eventual downfall but its mostly lost in a storyline that tries way too hard to be outlandish
Even with the likes of Amy Ryan and Will Forte providing capable backup it all grows ancient fast
The film employs and immediately squanders the talents of legitimate comedy allstars Amy Ryan Danny McBride Jemaine Clement and Will Forte
There is little that is worse than satire that tiptoes around its subject as if risking offense were a sin
Don Verdean is a pleasingly entertaining sometimes hilarious quietly satirical take on what makes us tick as a subculture
Parts of Don Verdean are executed with real imagination The rest of it falls asleep
Don Verdean is the sort of comedy which presumes its own hilarity long before it gets around to telling any actual jokes or staging anything that might otherwise be considered funny
I think I chuckled about three times
A poor attempt to spoof evangelicals and their mindless desperate followers
Don Verdean may only appeal to hardcore Hess fansanyone else will likely never once crack even a smile
Don Verdean could be more sharp more focused and its easy to be disappointed by the films low ambitions
There are some scattered laughs but considering its broad targets the film is too detached from reality to consistently hit the mark
Should have been broader should have been funnier Did they lose their nerve
It has enough original and strange elements to feel like its own entity and stand out Full review in Spanish
The most beloved fighter in cinema does not stand anymore in the ring Full Review in Spanish
If Rocky was a KO Creed is a TKO
Sylvester Stallone reminds us why we fell in love with Rocky in the first place Full review in Spanish
A powerfull emotional and well told story Full review in Spanish
The plot is pretty simple but it has a good start and a clear end also has moments that easily remind us to Rocky Full Review in Spanish
While this film is basically Rocky VII its also much more than that and perhaps the best in the series as it tells a standalone story with energy and skill
Coogler manages to make one of the best films in the series showing a Rocky full of courage and elegance Full Review in Spanish
In this revival there is not only talent but also affection and intelligence Full Review in Spanish
Creed reimagined a classic while paying tribute to a character that remains after forty years Full Review in Spanish
Creed may not reunite generations like Star Wars did but has fiber and muscle to keep fighting Full Review in Spanish
The most romantic story in sports films the industry has ever contributed to pop culture Full Review in Spanish
A powerful drama with great fight sequences and a superb camera work Full Review in Spanish
Stallone delivers a touching and wonderfully understated performance
Creed is able to succeed as an exceptionally entertaining sports story and the groundwork for building an identity free from the past
This is everything we could have hoped for from a Rocky spinoff and more Its a story about legacy race purpose trust and friendship
with Creed the series really has returned to its roots theres an earthiness and street quality that we havent witnessed since the  original
Director Ryan Coogler proves to be a filmmaker with a great capacity for narrative and just like his character hes trying to leave more than a good impression  a legacy Full Review in Spanish
An honest film the will thrill the thoughest and that can be enjoyed even by those who havent seen the originals Full review in Spanish
Like a palooka charging from his corner Creed plunges ahead with only a few sidesteps Director Coogler takes his time with each development endearing these already likable characters to the viewer even more
Panahi may make his points from inside a cab but hes no hack Just a farer of honesty and truth in a country where truth no longer has meaning
Panahi has made a work of invention and brio that remains visually lively throughout despite its formal restrictions
Panahis lighthearted banter and his interactions with his niece and his multiple fares  whether real or staged  give a valuable insight into life lived under a constant veil of political and religious oppression
Taxi is mustsee feelgood cinma vrit of the narrative variety
Jafar Panahis Taxi looks onto a world where the social order and the spiritual order are at odds in flux where the conversations are sometimes cutting sometimes comic sometimes troubled sometimes profound
Taxi is perhaps the most ingeniously optimistic movie Ive seen this year It also reminds me of the current Room another instance of extreme captivity where the only response is imagination Both films make our world a bigger place
Here is an auteur able to swallow the urge to shake his fist and bellow to the heavens about the injustice of his situation Instead he continues to explore with humor and humanity the themes that have always fascinated him
Taxi is easily the directors most accessible work to date
Panahis status as a martyr for his art could have gulled him into loftiness and pride and yet by some miracle Taxi stays as modest as his smile the point being not to recruit us to his cause but to put us on the side of his compatriots
This is not just a powerful lesson in creative determination and censorship defiance but an absorbing look at humanity full of poignancy and humor  Hope lives
iven the option Panahi might prefer to move onto fresh material  but as man and artist he seems to be surviving as well as could be hoped
Mixes the meta elements of his recent output with the shrewd social commentary of his prearrest work all buoyed by a cheekiness that makes it accessible to audiences beyond the arthouse
The sequences go from lightly absurd and funny to troubling and political from Panahis enjoyment of the rich fabric of secular life in Tehran to his anxiety of residing in a society controlled by the thoughtpolice of Irans religious oligarchy
Taxi is certainly the most entertaining of Panahis forbidden works but it is also a rich experience full of human drama comedy and ideas The filmmaker has turned civil disobedience into its own genre
At one point a lawyer lays a rose near the dashboard camera with the comment This is for the people of cinema For audiences Jafar Panahis Taxi is just such a rose
The spirit of Taxi is similar to Richard Linklaters Slackerpicking up new characters for discussions of current lifeexcept its funnier more polemic and a champion of every film ever made
Despite the laughs Taxi never loses touch with the desperate reality at the heart of its formal experiment
Beautifully balanced cheerful but incisive Taxi Tehran is playful but also deadly serious
It all feels spontaneous yet knowing filled with wry humor and revealing asides slipping in political and social commentary with a smile
His most recent work is an eloquent example of their strategies to pursue his film activity Full review in Spanish
What do developing countries need Trade not aid this provocative documentary argues
An easytounderstand docuessay with a toughtoaccept message
Boenishs wife Jean who trained to jump with him is interviewed extensively and although Strauch doesnt provide much backstory for her she emerges as that rarity  a perfect matchup to a seemingly unmatchable man
A generally splendid documentary by Marah Strauch about people throwing themselves off cliffs Literally
Long before there were GoPros there was Boenish a camera mounted on his helmet jumping from Yosemites El Capitan or LA skyscrapers under construction
Boenishs enthusiasm and lifeforce energize Sunshine Superman whenever hes an onscreen presence his absence proves to be a much harder thing to turn into compelling cinema
The two sides of the documentary more often exist alongside each other rather than supporting them Still especially for people who arent going to be throwing out chutes any time soon its a thrilling ride
Daring Thrilling Surprising  a well told true story
A spectacular and affecting portrait
While the doc takes a conventional and even pedestrian approach  with multiple talkinghead interviews and some reenactments with actors  there is also plenty of spectacular archival footage showing jumps dating back to the s
Engrossing tensionfilled and utterly fascinating
Sunshine Superman is at times a bit too reverent with the material and there are distracting reenactments  la The Jinx and Man on Wire some of which seem extraneous but overall the film is a sunkissed tribute to a singular individual
You might walk away from the film saying not That was cool or That was sad but That soundtrack was really fantastic
Boenish convinced plenty of jumpers to strap cameras to their heads which resulted in some spectacular footage
Firsttime filmmaker Marah Strauch skillfully interweaves the Boenish story  full of copious vintage footage shot by Carl  with dramatizations and interviews
You may find Sunshine Superman exhilarating or you may find Sunshine Superman terrifying but you almost surely will not find it boring
It will dropkick you right in the vertigo
Its a routine documentary about a fascinating man albeit one with a great classic rock soundtrack
Heartstopping cliffjumping doc
This is still a wonderful portrait of a man who chose to embrace life to its fullest whilst biting his thumb at gravity
This documentary about Carl Boenish founder of modern BASE jumping is a captivating portrait of a man unlike any other Not just an adrenaline junkie but a superb cinematographer technical wizard philosopher filmmaker and promoter of his sport
Ever watched an extreme sports enthusiast leap off a mountain to his death No Then you havent seen this bittersweet biopic about a compulsive base jumper with a death wish
The true story of charity founder Christina Nobles empathy in action
Noble largely works due to its clenchedfist approach tending to the particulars of Christinas war against suffering while maintaining its message of hope making it the rare faithbased film thats more show than tell
Writerdirector Stephen Bradley may make some missteps but he capitalizes on this underdog storys inherent thrills
The real Noble accomplished a lot but the movie insists on giving her achievements a mystical and mythical dimension  without the imagination to carry it off
A smart touching tragic goofy and surprisingly captivating dramatic biography of Irish childrens rights activist Christina Noble
Her Dickensian childhood provides real intrigue but writerdirector Stephen Bradley portrays the grownup Noble as a messianic figure making her philanthropy seem like acts of hubris rather than selflessness
Nobles biggest flop may be its dialogue
I left Noble wanting to know more about Christinas story than could be shown in a twohour film Can anyones life be sufficiently distilled in such a short span No But glimpses of such lives can serve other purposes
Uneven but nonetheless emotionally gratifying
Her indomitable spirit and merry heart make this remarkable story vivid true and touching
A feisty passionate performance by the Irish actress Deirdre OKane gives the inspirational biopic Noble a serrated edge of defiance and gumption
An earnest tribute to a strong and ambitious woman
Intelligent sincere and unabashedly goodhearted
As its title indicates this is a straightup inspirational tale but its sincerity and aboveaverage execution set it apart from many other similar movies
Portrays Noble as a person rather than a middleclass moviegoers moral pinup
Uplifting docudrama set in Vietnam recounting an Irish womans selfless efforts on behalf of the wartorn countrys homeless orphans
Even if you dont quite buy the philosophy this is selling the three main actors are impressive and director Stephen Bradley does a fair job of slipping between the different timelines
Noble is a solid straightfaced biopic that is raised to surprising heights by a flinty central performance from the redoubtable Deirdre OKane
Often desperately sad but ultimately triumphant Noble is a sincere and wellexecuted portrayal of good triumphing over evil
Bradley has structured the tale well as when we do move between eras its done so in a seamless manner and with a minimum contrivance
Erika Frankels documentary is finally revealed to be a story of prolonged adjustment to retirement and a poignant illustration of sublimated redemption
Frankel has a fine eye for telling detail and the result while sentimental is as irresistible as the dessert cart
King Georges feels stretched into feature length but its ending neatly portrays a man with a fierce personal code who seems to have accepted change
A poignant funny and wellseasoned portrait of autumnal fervor
The relationship between the young American and the old Frenchman is as rich as one of Perriers sauces the pupil and the teacher the son and the father the keen protg and the stubborn classicist
King Georges reminds us that a singular dining experience can often be the expression of a very singular personality singular temperament singular discipline
The vividly detailed behindthescenes glimpse of one of Americas best kitchens along with a holistic view of the title character make King Georges a flavorful treat worth savoring
Fun fare for foodies especially those with a taste for classic French cuisine and nostalgia for the posh French restaurants that once dominated the culinary scene
By patiently letting events play out past the easy and expected endnote Frankel has fashioned a fine story out of what might have been merely a cautionary tale
It finds its filmmaker completely lost between impulses to pay homage play it safe or offer somethinganythingnew
The widescreen intimacy of small moments  the flush of a rainsoaked cheek  humanizes Donzellis grand folly and the couple who challenge the parameters of morality
Marguerite  Juliens romance is a nonitem but Donzelli sprinkles it with faux naivet which has the aftertaste of an artificial sweetener Who would ever want to see this movie
The wan narrative at hand fails on several levels including an annoying amount of intentional anachronistic details that detract rather enhance
For a premise as provocative as an incestuous French fairy tale this sort of toothlessness is tantamount to a death knell
The excruciating experience of Marguerite  Julien need only be endured by viewers with an obsessive interest in the least constructive aesthetic currents in contemporary French cinema
An overheated but haunting strangely involving tale inspired by the doomed romance between reallife aristocratic French siblings circa
Whats missing is any sense of unified purpose or vision
It plays a little long but the visuals are spectacular in Pixars holiday film about family and overcoming fear Its a curious blend of coming of age story buddy movie and mid west adventure
Just nice basic fun really
The latest PixarDisney feature is a somewhat headscratching affair with some sweet charms and cute laughs but nothing like the complexity and profundity of Inside Out
As bright and fastmoving as it is The Good Dinosaur is very much a colourbynumbers effort Pixar has covered this sort of territory so many times now that recurring themes about being yourself have become rote to all but the youngest Pixar fans
Good enough even if its not as Pixar perfect as weve come to expect
Very good indeed
In  for the first time in the companys history Pixar released two films One of them was wonderful The other was The Good Dinosaur
An awesome technical feat The Good Dinosaur is a very good Pixar film but not quite a great one even if it is highly likeable all the same
While not up there with Toy Story  and The Incredibles as Pixars best this is still a warm and wonderfully told tale Arlo may be The Good Dinosaur but this is a great movie
Hyper cartoon design in the foreground and IMAX realism in the background makes an odd dichotomy and its hard to see the point
One of Pixars more entertaining recent efforts
By turns breathtaking to look at and emotionally engaging the film has to rate as a triumph for the way in which it can appeal to audiences of every age while perhaps relying more on classic storytelling elements
A deeply affecting and impossibly beautiful film that is steeped in genuine emotion colorful characters and a moving moral message
While The Good Dinosaur does have things to admire about it including beautiful animation and some intriguing characters its ultimately one of Pixars weaker efforts thanks to an overlyfamiliar story and themes
It certainly has its charms and is several notches better than a lot of the computeranimated mayhem passed off as childrens entertainment these days but its impact is almost entirely in the moment
The Good Dinosaur is a much better film than Cars but the newer movies propensity for sentimentality and Americana does indeed recall the earlier unlovely Pixar adventure
Its a beautiful touching and exciting tale of learning to grow up in a world where loss and death are a part of life
As sublime as D character animation gets
While the humour and supporting menagerie of characters feel slightly stale Arlo and Spots relationship is a remarkable feat of emotional evolution
You dont often chastise a film for not being sufficiently manipulative but My All American is an outandproud tearjerker and it cant even do that
Fumbles the opening kickoff and never recovers Manages a field goal at the end but no touchdown
For those looking for a sports film of basic quality and genuine narrative My All American is definitively useless
Heartfelt if bland football biopic is great for game fans
Dont get me wrong this film accurately shares a wonderful guys alltooshort life with us I only wish it had been delivered in a more wellrounded way
American football is played on a rectangle but movies about it can sure be square
My All American really has only one goal putting a lump in your throat at watching a kid refuse to stop believing that every one of his dreams could come true Lump delivered
This is the kind of movie they make you watch in grade school Its meant to be good for you and the entrylevel script has every clich in the playbook
Instead of genuine inspiration the movie provides only flimsy superficial positivity
Even those who love inspirational sports films might have to flinch from My All American is a movie so square conservative and humorless that it winds up playing like a brutally straightfaced South Park parody of gridiron schmaltz
My All American scores additional points for its emotional impact
an easily digestible slice of Texas football nostalgia that stumbles short of the goal line
Everything in the film may be true but every second feels contrived and thats a momentum killer that leaves My All American wide open to an allout blitz of rolling eyeballs
Almost unwatchable outside of Texas
Commits a major gaffe by overpolishing Freddie Steinmark undermining the impact of his reallife story
My AllAmerican is a redblooded Godfearing cavalcade of walltowall inspirational earnestness
Michael Reilly Burke and Robin Tunney are good as Steinmarks parents while as Steinmarks devoted highschool sweetheart Sarah Bolger makes a mark for herself as someone to watch
Other than a moving conclusion this is a mediocre sports drama
Were never invested in the sanitary whitebread story that renounces drama and entertainment for the sake of accuracy and a squeakyclean PG rating
Not an inspirational football movie but the highlights reel from one with a golden boy who is his own manic pixie dreamboat The worst sort of hagiography
Its an intricate web but when its finally untangled a remarkable and striking picture emerges Spotlight is that picture
the kind of film that takes on the Big Subject with modesty and selfreflection drawing the viewer into the world of the reporters and culture of Boston in  to better understand the scope of everything thats at stake
Succeeds because it puts the audience alongside the reporters allowing us to share the incredulity disgust and frustration
Nearbreathless hightension performances from a fully immersed ensemble each member conveying an unflagging determination to unveil the breadth of degeneracy
One of the years most noteworthy Oscar contenders and a brilliant ensemble piece
Spotlight is an advertorial for journalistic objectivity and integrity in the face of abject perversion
Spotlight a brilliant new film will shake you out of your complacency and renew your faith in the power of investigative journalism
The screenplay by Tom McCarthy and Josh Singer is one of the films greatest strengths imbued with as much wit as it is sobering truths
For such an intense story Spotlight ultimately fails because its so caught up in the process of news gathering that it stops being a compelling movie
Spotlight is a great movie that shows us the extreme amount of hipocresy at work within our world Full Review in Spanish
Spotlight is more than just a chronicle of horrendous events its a portrait of the pain and suffering millions around the world have experienced Full review in Spanish
Spotlight has increased my admiration for journalists who work diligently to find the truth behind serious problems even if it involves powerful institutions like the Catholic Church
McCarthys clear simple direction allows the performances to shine
A stirring assessment of the right and wrong of our humanity in the volatile world of a changing newsroom
The effort and teamwork highlighted in this film is absorbing intelligent and it remarks how exciting investigative journalism is perhaps it lacks adrenaline but its cinematic importance is undeniable Full review in Spanish
The cast is an impressive ensemble but Spotlights genius is in its calmly urgent take on events In doing so it makes them all the more sobering and gravid
There are fine ensemble performances from McAdams John Slattery Paul Guilfoyle and Stanley Tucci but Michael Keaton is Spotlights secret weapon
Spotlight is an indispensable film for moviegoers and for every believer Full review in Spanish
Like the crusading journalists it honors Spotlight forges ahead with a gripping story and doesnt stop until it gets it right
It restores my faith in the concept of redemption to see McCarthy bouncing from The Cobbler straight to Spotlight
The remake adapted and directed by veteran Hollywood screenwriter Billy Ray messes with and messes up the central romance and changes the triangular dynamic
This is a story of longings obsession and the inability to move on from events unaccounted for by justice
Why then does it fall so flat Ray hasnt nailed the structure dug deeply enough into the characterisations or finessed the big twist nearly well enough  thats why
An intriguing timeshifting tale of obsession and one of Hollywoods better remakes
This is a lethargic Alist remake of an ingenious Oscarwinning Argentine thriller from  called The Secret in Their Eyes Im not sure why they dropped the definite article
This isnt so much a remake of Juan Jos Campanellas  Oscar winner for Best Foreign Language Film as an utter travesty of it
This is a pale imitation of the Oscarwinning Argentinian original that cant breathe any new life into familiar territory
Two great performances squandered in a silly misfire
The film itself doesnt generate much suspense but it does engage as a morally ambiguous tale about loss and obsession
Tme and again the drama spirals into cliched foolishness and rulebending procedural pastiche from which only a typically engrossing turn by Ejiofor can save it
As a decent involving thriller it exercises the little grey cells and passes muster
Sacrifices atmosphere and character interest for the intrigues of a murder plot that simply isnt very compelling or consequential
Chiwetel Ejiofors new movie is a mess Even the title  why are there more eyes than secrets  induces brain ache
Appropriately enough a bogus Hollywood prestige picture is exactly what we get with US remake Secret in Their Eyes
Its rare for an American remake to be scruffier than the original but this film is an intriguingly messier take on the superslick hugely engaging  Oscar winner from Argentina
The overwhelming sense of unoriginality lingers over proceedings like a dark menacing cloud
Sure it is still an entertaining film and offers some great moments of acting and suspense but those who have the time to compare both films will be disappointed
The original pictures melodramatic excesses have been augmented with fresh indulgences to deliver a film that is not quite peculiar enough to be interesting
The new film fails to recapture the atmosphere of fear and menace that made the first Secret so gripping
The stars all bring their A games but beyond their convincingly moody performances this dour remake has little to recommend it  unless youre in the market for a circuitous year ordeal of loss regret and missed opportunities
A lowkey drama whose modesty is its own reward
James Francos Boyhoodrandom scenes sketching out boys callowness cruelty compassion and comic books And curiosity Bicuriosity
Yosemite mines Francos fiction for its most vital quality his unsentimental depiction of youthful insecurity this time among fifthgraders
Where STAND BY ME pressed its characters faces into the stink of mortality the boys here have yet to even hear the news about a dead body down by the train tracks
The material aspires to powerful terseness but its seemingly conscientious refusal to ramp up too much drama ultimately renders it thinly anecdotal
Its an interesting effort with a clear portrait of preadolescent curiosity but its not something to be viewed casually as the features patience with tone takes some getting used to
A sobering evocative drama
Franco gives one of his most subtle performances yet as a recoveringalcoholic father and the three young newcomers performances are honest and affecting
The movie succeeds on its own quiet terms binding the three parts together with assurance and tonal consistency to cast a lingering spell
While many may find its slow pace and lack of action a bit frustrating Yosemite is a thoughtful and wellexecuted coming of age indie
An earnest examination of the loss of innocence this critic might have appreciated even more if the subjectmatter hadnt be so relentlessly dark and disturbing
Although evocative and nicely observed the comingofage drama Yosemite ultimately proves too lowkey and elliptical to make much of an impression Stand by Me it aint
Extraction constantly tries to score a flashy TKO  but never lands a decent body blow
Very lowrent with a phonedin Bruce Willis performance However director Steven C Miller and game stars Gina Carano and Kellan Lutz contribute a few solid action scenes
There are a couple of passable fight scenes including one in a biker bar But the plot is ridiculous the bad guys are uninteresting and the script is so dull youll want to check your emails during the screening
Extractions  not by any stretch of the imagination good But at least it doesnt waste everybodys time
An unexceptional action picture with oldfashioned ideas about what it means to be a man
There are I guess some decently staged fight scenes including clever use of some pool balls but they in no way make up for having to sit there and watch Lutz garble out warmedover tough guy dialogue
The actionthriller Extraction may not be about tooth removal but for all the fun it proves it might as well be
Its a rare movie that cant be saved by Bruce Willis singlehandedly killing all the bad guys And yet here it is Extraction
It takes confidence to put out a movie whose singleword title is also the procedure by which a dentist gets rid of a rotten tooth
Extraction is an exhaustively paintbynumbers affair and nobody is more bored with it than Willis
Extraction Id rather have my teeth pulled by diseased evil monkeys
Some stylish touches cant rescue a thin concept thats riddled with cliches
Bad acting clunky cinematography and editing heavy violence and a lessthangenius screenplay make this a totally disposable action movie not even recommendable to diehard Willis ans
An obvious twist wrecks the story and a good supporting cast upstages the Younger Less Charismatic Eastwood in every scene
It inelegantly attempts to infuse a standard revenge western with the gravitas of a war veterans cominghome odyssey
Diablo is the first new film Ive seen in  Ill be surprised if I see one thats worse all year
Diablo is an underminute movie that feels doubly long bringing you facetoface with the Devil but taking its sweet time to get there unfortunately
Roeck aims for an homage to Eastwood Srs revisionist Westerns think The Outlaw Josey Wales Unforgiven etc but the result is pretentious and pointless
Powerful and beautifully shot if emotionally distant
Theres an Eastwood back in the saddle and maybe for some viewers that will be enough but once Diablo gets to a full gallop it really has no idea where to go
An early candidate for the  worst movies list
A relatively ordinary oater
Diablo boasts the skeleton of an interesting allegorical oater  but Carlos de los Rios screenplay never manages to provide flesh or a beating heart leaving the whole endeavor feeling more like a rough draft
Hes got the DNA for westerns but Scott Eastwood still needs some seasoning if Diablo is any indication
Theres not much beneath the surface of this modest vigilante saga
As far as high concept westernhorror flicks are concerned Diablo is nowhere near the excellence of the recent VOD release Bone Tomahawk but it should be satisfying for audiences who are in store for a moody and intriguing genre exercise
Ill give Lawrence Roeck credit at least he didnt reveal the twist to his new film Diablo in its last few minutes
Its impossible not to think of Clint Eastwood in his great Westerns when watching his youngest son Scott in this poor one and the comparison isnt favorable Both the movie and its star seem empty
Subverts the cultural contradictions saturating westerns historically while probing the postCivil War PTSD pathology beyond the officially mythologized dark side wild west frontier And another corrective to the portrayal of Native Americans on screen
The kind of movie your stepdad would mention having seen on Netflix a few days back but have trouble remembering the name of Hed probably think it was pretty good though
Diablo is not a great film by any stretch but it is stylish
Absurdist comedy with a level of violence so extreme and so jarring it makes the ultraviolence of Kubricks A Clockwork Orange look tame
While Moonwalkers is not a great comedy it delivers its laughs through the course of the film and leaves you with a smile
Houston we have landed in an overtly silly modandLSD rendition of  London that seems on loan from the Austin Powers sets
The misguided Moonwalkers invests too much in the comedy potential of things that havent been funny for a long while
Director Antoine BardouJacquet who devised the story is dazzled by period style and puerile jokes but nothing lands as especially funny merely tired
a sweet film that isnt afraid to show a mans head being blown off And to make it sort of kind of funny
Forget a fake moon landing Moonwalkers is a fake comedy
Its a setup rife with possibilities yet BardouJacquet manages to squander all of it veering wildly between farce and abject violence
There are too many footinmouth moments rather than clever writing and imaginative directing The films narcissistic attitude overplays its welcome
Graphic violence lots of drugs in soso black comedy
Moonwalkers starts off with an interesting idea and an attractive visual style but winds up falling out of orbit
Its all good English fun full of drugs brutality and general lates decadence Its also weirdly slack for such an insane ride as if director Antoine BardouJacquet was afraid he might get in the way of Dean Craigs splendid story
A drugfueled hyperviolent swingingsixties story
Moonwalkers is bloody vulgar and strangely mesmerizing with a unique cast that is essential to executing its ridiculous concept Ron Perlman is gloriously violent and is able to showcase the best side of dry humor
Moonwalkers takes a weird clumsy turn
An amusing concept becomes overloaded with quirks and psychedelic nonsense
While it brings a great deal of energy and promise to the table the film ultimately succumbs to its own bombastic tendencies
Has all the makings of a cult classic
Tarantino on acid subversive sixties stoner satire But a combo ballsy big screen intersection of politics publicity and propaganda that couldnt be more provocatively in the here and now concerning truth in movies and the media  if there ever was any
The whole thing has a retro swinging s vibe that I enjoyed The actors all commit to both the lunacy on screen and its premise and the result is a good natured and entertaining film
The Girl in the Book is an auspicious debut for Cohn a showcase for VanCamps true acting abilities and a fascinating feminine story
Cohn displays deep sympathy with her protagonists intersecting emotional crises scripting a narrative thats intensely perceptive without becoming mired in mawkishness
The filmmakers lend the films overall impact the alert observational intelligence of a firstrate short story
So unpretentious that it could be accused of lacking style or vigor writerdirector Marya Cohns maturely conceived Kickstarterbudgeted debut swaps genders on the more traditionally maledriven story of a stunted comingofage
As Alice VanCamp is exceptional eliciting our sympathy even when the character is making maddeningly selfdestructive decisions
Though its resolution is a bit pat most of The Girl in the Book is a smart and pointed look at abuses of power and roles women too often play in the literary world
VanCamp gives a layered memorable performance while writerdirector Marya Cohn making her feature debut has crafted a nonlinear story that artfully tiptoes between cliche and truth
The director Ms Cohn making her feature debut wrote the script and handily keeps the storys many elements in motion
From time to time Cohn allows a few rays of light to penetrate the gloom and suggest that Alice may find a way transcend her despair Its a small measure of relief from the overall mood of hopelessness
The Girl in the Book proves to be anything but a pageturner
Cohn  adeptly balances elements of contemporary romantic comedy and scathing cultural criticism in a film that is obviously deeply personal and simultaneously universal
Perhaps We are Twisted Fing Sister doesnt satisfy in the expected manner but its a terrific summary of hard work and dedication to a dream presenting casual fans with a PhD in SMFology
Its all steak no sizzle  the opposite of Twisted Sister
Its a very entertaining yarn made all the more enjoyable by the fact the bands two leading members guitarist Jay Jay French and his arch rival lead singer Dee Snider have a very welldeveloped sense of their own absolute ridiculousness
This persuasive paean to sheer bloodyminded persistence flies by over the course of  engrossing often hilarious minutes
Dee Snyder is charming and the movie is crammed with great archival footage of the band ranging from their early days as a David Bowie cover act to their jamming onstage with Lemmy Kilmister of Motorhead on British TV
A comprehensive and sometimes harrowing portrayal of the grind a working bar band in the s had to endure to get by
Using a trove of archival footage and memorabilia the story is told by the band managers and its devout fans over the course of  wildly entertaining minutes
Silverman proves that she is a legit actress not just pretty good for a comedian trying to get serious
Silvermans scarily good in this role  sickjokefunny when the behavior supports it raw yet subtle at Laneys most reckless junctures
Silverman the tarttongued standup comic known for treading treacherous social waters confirms herself as a serious actress If only shed been given a character supported by a stronger arc
A tough unbending sometimes brutally truthful profile of one womans addiction and the havoc it wreaks on herself and just about everyone who matters to her
Silverman is far and away the best part of I Smile Back a strained entry in the Mad Housewife genre
Silvermans performance while good is by no means great and she is not able to transcend what amounts to a little too much sweetness and light in this cinematic Smile
This is a dark picture but it has an exceptional focal point in Silverman who fully embodies the drain of depression
Sarah Silverman uses her standup persona to bring her fragile damaged addict to life giving one of the most startling performances of
I Smile Back has all of the makings of an intense emotional drama but the execution is messy and by the end didnt seem to have a clear story that it was trying tell
Holding the film together is Silvermans tourdeforce performance Involved in every scene her serious acting holds the film together
Silvermans shtick may be getting tired but her bravura turn in I Smile Back suggests a career resurgence
Silverman takes a tremendous leap of faith outside of her comfort zone and gives a nuanced and emotionally draining performance
A terrible script by Paige Dylan takes all the fun out of standup comedian Sarah Silverman playing a serious part as a depressed addict
I Smile Back pummels with nastiness then moves from one shocking event to the next without a backward glance
It is a brave performance by Sarah Silverman as a woman slipping into a private hell of her own making but  well that probably says enough
I Smile Back isnt a candycoated noble depiction of addiction and the toll it takes on families but rather a bleak yet honest look at how regular people lose themselves and how difficult it can be to find the way back
Silverman delivers a knockout performance  any memories of her scatalogical standup act are washed away in an instant But her intense commitment to the depths of depression belongs in a better more focused less derivative film
The sort of reductionism one finds in cautionary videos made for schools with equally weak logic
It would be nice if this film offered more hope for Laney but it isnt that kind of film It is just a slice of life and a pretty depressing slice at that with a disturbing beginning a dismal end and false hope in the middle
At a running time of  minutes we learn too little about Laney and too much about her addiction
Entertainment offers some genuine insight into its subject matter Coupled with an astonishing central performance it is a work that continually fascinates
I left this barbed portrait of a crackingup comic with more than a little respect for its fearless director Rick Alverson and his trusting star Gregg Turkington You cant deny that theyre a match made in heaven
Get with the extreme deadpan tone and long takes though and the film feels as brave as it is potentially exasperating
Weirdly compelling if studenty and unfocused
Like Hamburgers metahacky comedy routine the film confronts and challenges in order to produce something increasingly rare in American cinema an active engaged experience
The Comedian is an ugly man with an ugly soul and an ugly sense of comedy that at one point literally includes making fart noises for  full seconds as he pretends to gun down his silent audience with a soccer trophy
Gregg Turkington delivers a spoton performance as a downinthedumps comic in this gloomy offering Too bad the film left me me in a deep funk too
A Greek tragedy told through the vessel of a smalltime comedian Entertainment is a bleak look into a broken man swallowedup by secondrate showbusiness
Its the closest emulation of a waking nightmare in an American movie in a very long time The anxiety is generalized but its also as specific as Hell
A daring and mainly successful black comedy about a dour and depressive comedian on a deadend tour of California
It is not fun but its confrontational style yields dividends
Theres a chic emptiness to Entertainment undoubtedly and anticomedy constructs that may rub the wrong way but theres also a spiky intelligence at work too one that engages through the artifice of disengagement and the illusion of performance
Conjures an elegant portrait of a nation lost in a surrealist slipstream drawing on the iconography of its past and the shell of its present Its one of the best American movies of the year
The Frankensteinesque final scenes of Entertainment make it painfully clear As much as we want to read them as social critique Rick Alverson makes monster movies
An unsparing road trip through the barrenness of American pop culture and by extension the human soul
A weird road flick about a weirdo standup comic with a weird repertoire
This is one of those inyourface angry confrontational movies It wasnt just a chore to sit through it was more like a homework assignment given to you by a teacher who hates your guts
Its difficult to give Alverson and his star too much credit for depth insight or having a point Because Im not certain they have one
Alverson continues to zero in on the most brutal tendencies of contemporary comedy
Entertainment is a fully realised and extremely accomplished translation of Gregg Turkingtons Neil Hamburger character to the big screen
An empathetic snapshot of a country that is almost never depicted in such an accessible light
North Korean culture is lensed in part through a South Korean perspective with the final chapter asking Is reunification possible
The fact that Yoo isnt regarded as an outsider by the people shes recording allows her to capture a number of revealing unrehearsed moments that startle in their raw emotion
Scattering history lessons and ambiguous imagery amid Ms Yoos engagement with North Koreans her film implicitly asks What must they think of us
Yoos film is a necessary correction of how we should view or think about what life is really like in North Korea even if the work feels somewhat limited by its own necessity
Though repetitious this muckraking documentary exposes the dangers of chemicals that corporations put into some of their products
Though Whelans debut filmmaking effort wears some of its homemade characteristics proudly it wrangles more than enough credible interviewees to make its points
Like any of these documentaries this ones all over the map but Whelan grounds it in his family
A rather slipshod approach to the science of the subject undermines what might otherwise be a very strong film
Breezy Michael Mooreish environmental documentary by a nontreehugging venture capitalist advocates for disclosure of carcinogenic chemicals in everyday products
A heartfelt documentary about the chemical industrys aggressive efforts to conceal the thousands of potentially toxic ingredients contained in everyday products
A chilling exploration of the toxic underpinnings of the word fragrance that appears on detergents and colognes alike
No matter its cinematic derivativeness Stinks outcry against continuing to use the American citizenry as chemistry experiment guinea pigs carries with it the unassailable whiff of common sense
While Whelan repeats his points too much it remains gripping and maddening throughout to watch him run into stone walls
Even if you know a lot about the period Trumbo is still full of telling detail and Bryan Cranstons heartfelt performance as the remarkably driven and engagingly human Trumbo makes it well worth catching
Despite the dark subject matter theres much levity courtesy of Trumbos droll wit plus a roll call of Hollywood legends including Kirk Douglas Otto Preminger and an ebullient Bmovie producer Frank King played with gusto by John Goodman
Bryan Cranston does a remarkably effective job of impersonating the man who played such a key role in the era of the McCarthy blacklist from his raspy voice to his distinctive cigarette holder and his pungent sense of humour
With a topnotch cast and a compelling story Trumbo delivers a fascinating piece of cinema history that easily ranks as one of the best films of
With elements that include politics star power glamour and money there is plenty of bite in Trumbo Its a crackling good tale
Its full of both gravitas and titillation as perhaps any true story is there is the destruction of lives and careers on one hand and the spectacle of the players on the other  with Hollywood stars of the s and s as more or less support players
Some have quibbled over the fact that Elliott and Stuhlbarg dont look much like Wayne and Robinson here but it hardly matters as their performances are so strong and theyre matched by a large and luminous cast
The end result is a nobleintentioned attempt to pay tribute to the man responsible for writing Spartacus Roman Holiday The Brave One and Papillion but one that feels all too familiar in the way that it has been put together
Bryan Cranston shines in this remarkable true story about one of the darkest periods in Hollywoods history
Louis C K delivers a gentle and touching performance as fictitious screenwriter Arlen Hird that gives the film its emotional centre though why Roach and McNamara needed to introduce a fictional character isnt immediately clear
The film is a labour of love for Bryan Cranston as Trumbo As Hollywoods greatest screenwriter embattled but not beaten he embodies the old Hemingway definition of courage grace under pressure
If you are vaguely aware of his peerless reputation  or of the noble stand Trumbo took during a dark time in American history  then youll be content enough with this basic biopic of the man Even if it isnt a helluva movie
Bryan Cranston is irresistible as Dalton Trumbo in this sparkling period drama surrounding the Hollywood Ten
Absolutely sterling compelling drama about the notorious Hollywood blacklist of the s and sBryan Cranston Breaking Bad puts in a careerhigh turn as screenwriter Dalton Trumbo
Its repetitive in places but Trumbo is a moderately interesting character study Its not putting Dalton Trumbo on a pedestal and asking for him to be declared a saint
The fabric that holds Trumbo together is the love story of the writer and his devoted longsuffering wife Cleo As played by Diane Lane Cleo emerges as Trumbos most ardent supporter and most honest critic
A film so well built it also allows to explore the social and economic state of things at the time and how it affects Trumbo and his personal life Full review in Spanish
A story so incredible as the fact that Hollywwod was out for a with hunt Full review in Spanish
In his st project on the serious side after movies like The Campaign Dinner for Schmucks  Meet the Parents Jay Roach delivers a rather fine job
A film that does justice to screenwriter Dalton Trumbo Full review in Spanish
The actors have the showmanship to chew the lurid shopworn material up to bits savoring it like a Royale with cheese
Directorcostar Jackie Earle Haley and his cast serve up a solid slice of Tarantinolite But the script unravels with weak third act and overexplained finale
The script veers from comic narrated episodes to surprising violence planting early narrative seeds that yield some effective surprises
Theres an adequate conclusion waiting for viewers willing to work through a movie theyve seen before though Haley throttles repetition with some welcome screen energy and a love for the genre
Its a twisty kinetic and satisfying thriller with a didntseethatcoming climax
The thirdact twists that sink Jackie Earle Haleys directorial debut are downright Criminal
It wont win any awards but its not a bad evening out
An eleventh hour twist adds a bit of intrigue and everyone  including Haley himself as a henchman  seems to be having a pretty good time
It owes as much to Quentin Tarantino as the four bunglers owe to Eddie Yet it doesnt feel like a mere imitation it has too much wit and too many striking performances for that
A profane thriller that so closely resembles the Bmovies that followed The Usual Suspects Pulp Fiction and Get Shorty it could be mistaken for an archeological discovery
The diverting  if derivative  crime drama directed by Jackie Earle Haley comes laced with equal parts comedy and lurid violence a la Quentin Tarantino
Haley whips it into something reasonably entertaining even as you start thinking about how truly great Get Shorty and Travoltas Chili Palmer were midway through those doublecrossing criminal activities
Criminal Activities is a sneaky snaky little crime thriller with some pretty impressive plot twists
Its combined cast has an allages appeal although Robert Lowells screenplay struggles to drive through  minutes
If you dont expect much in terms of originality there are some good times to be had here
All sound and fury signifying nothing  just clichs begetting clichs
I almost wish that the film would have centered on Travolta and Haleys characters since the plot threads involving the four friends get wrapped up in about halfadozen The Usual Suspects wannabe twists
Some stylish touches and committed performances enliven this otherwise generic crime thriller
An exciting revenge thriller enhanced by Pulp Fiction vibes and a fascinating  but quirky  performance by Dan Stevens as youve never seen him before
a family home is also a prison of perverse history aspirational envy and twisted revenge Estranged shows Englands class structure to be a closed system its bricks and mortar staying essentially fixed no matter how deep the genetic pool may run
The transfixing directorial debut of Adam Levins Estranged creates an encroaching sense of imbalance and peril very early on and doesnt let up until the end credits
A welldelivered dark thriller helmed by firsttimer Adam Levins
Based on true events and well balanced the story of The  may be a surprise to many who only know the basics of the event
Despite dramatic physical effects during the initial structural collapse The  lacks the claustrophobia expected playing out like a s disaster movie minus the narrative tension
Strong drama and inherent humanity prevail to ensure that even the most hardhearted of viewers will crack and blubber with a lump in their throats
A missed opportunity to tell what should be a captivating reallife disaster tale that is instead plodding scattershot and lacking in dramatic impetus
How a film with this many faults can still deliver the emotive release its angling for is not perhaps as mysterious as it seems  you can hack away at it with a pickaxe but this storys indestructible
The astonishing true life story of The  deserves a better than movie than this Trite above and below ground it is not suitable for miners Or anyone else really
Its pretty hokey but likable and the fantasy last supper scene is tearjerking stuff
Riggens account is more thoughtful than the average disaster movie
What it does have is a gripping madeforthemovies combination of horror and heart as the families of the men gather at the surface first in protest at the lack of help then in celebration as the government gets behind the miners plight
A conventional but gripping dramatisation
Once seen instantly forgotten
In the end The  only grabs on a superficial level
The notion of a brownedup Juliette Binoche playing a humble Chilean streetfood seller deserves the snorting it will undoubtedly trigger
Riggen methodically juxtaposes crises above and below ground level the only stylistic surprise being a scene that recalls a hallucinatory moment from Oliver Stones wholly superior World Trade Center
A decent absorbing film but it all felt more urgent and compelling when it was unfolding on a daily basis on the news
The  lacks in emotion construction and staging Full Review in Spanish
The mens ordeal remains powerfully affecting although there are long stretches when it is touch and go whether their story will end up buried beneath a mountain of Hollywood cheese and clich
Sanitized for Hollywood a harrowing reallife story does not always make a good film
The film is forgettable but the story it tells us is a touching one and worth remembering
It focuses on painting emotion that was there anyway missing a chance to exploit richer plot elements As a result it is not as powerful as it might have been
Stanley Nelsons documentary tracing the rise and fall of black Americas most militant movement has a little bit of everything good mercurial personalities passion and plenty of drama
An evenhanded overview of a tumultuous time in US history
Exploring activisms role in the s struggle for civil rights this is a powerful look at a contentiously iconic movement
The film doesnt shake our suspicion that the stories being told have calcified into legend
Why are the Black Panthers not a force today Has radicalism collapsed An engrossing return to a forgotten past
It might have benefited from a more critical polemic some of those Panther cats I believe werent all good but for an analysis of US racial dynamics as seen from the post ICantBreathe era it makes for sobering viewing
Conventional as a documentary but speaks about much more than the just the subject at hand
Both educational and entertaining the rise and fall of the most alluring and controversial black organisation of Americas late s comes to life via facts vivid images and unique statements
Combining archive footage and presentday interviews and using music to drive the story Nelson tells a complicated and often violent story in lithe inventive fashion
A pulsing soulpower soundtrack extensive and rare archive footage and fiercely honest contemporary interviews drive Stanley Nelsons blistering account of the rise and fall of the Black Panther party
Vanguard of the Revolution is at its strongest when it entertains female voices Elaine Brown Kathleen Cleaver Ericka Huggins Their fire is vigorously undimmed
Nelsons film mixes rare archival footage with penetrating interviews to explore the successes failures myths and realities of the Black Panthers
This film is an awakening even for people who watched the original coverage firsthand
Theres still a crisis in blackwhite relations which makes this film timely
The thing that struck me watching this was how little things have changed with white privilege and police killing blacks since this group started out as a self defense organization in Oakland California in
Nelsons work feeling more fragmented than kaleidoscopic falls short of sactivist docs The Weather Underground  or   But sobering images and snippets of poetic insight linger
Obviously theres more about Americas postslavery institutions and their resisters than can fit in a twohour movie But with the aid of an extrafunky soundtrack Vanguard of the Revolution makes a good fistpumping start
Nelsons documentary shows that the story wasnt as simple as most people remember today
Despite the title not at all a fan tribute to a group that like so many others in the mad s imploded through a combination of state repression and its own ineptitude
The Panthers confused manifesto included shades of Marxism Leninism and the black nationalist theories of Malcolm X But the movements descent into violence and corruption was swift and fatal as Mr Nelsons fine film painstakingly demonstrates
Aaron Sorkins screenplay and Danny Boyles direction give us a Steve Jobs profile that does not idolise or flatter him As a result the film gains respect and we gain insight
When you have time to absorb all the positives here from the performances to the scripts details it impresses and lingers
Sleek efficient and spunky Aaron Sorkins attempt to distil Steve Jobs life into one pocketsized movie reflects Apple products in all but one way its ambition is alienating enough to render it userunfriendly
A balanced and often critical perspective of the flawed visionary behind Apple Pixar and everything Mac
Featuring fine performances from the whole cast as they make Sorkins sometimestricky dialogue sound real Boyles film makes you wonder how Steves friends and colleagues would feel about how impossible and appalling hes made to appear here
This is one small corner of a portrait magnified to the extreme If you do happen to find this product userfriendly it will be due to the aptly intuitive design of Fassbenders excellent performance in the title role
All respect to Leo but Fassbender deserves an award just for getting through the dialogue here alone
Jobs insistence on a closed system completely incompatible with anything else is a convenient metaphor for the character we see on screen here
Sorkin has created something that feels staged and unrealistic Its as if every key person in Steve Jobs life wants to complain in the halfhour leading up to each launch
Fassbender in a superb performance portrays Jobs as a genius  but also as a painfully flawed human being
This screenplay is staggeringly lazy Its factually incorrect in almost every way consisting of selfsatisfied expository patter and cringingly simplistic characterisation
Steve Jobs a study of the late cofounder of Apple that takes place in the hours and minutes prior to three separate product launches is a powerful film with little subtlety
Though it will undoubtedly mean more to people somewhat intimately familiar with Apples history Steve Jobs is a nevertheless affecting piece of cinema that never attempts to canonise its subject matter
the drive the philosophy the ruthlessness the prescience of Jobs a man who redefined the way we interact with the world
Steve Jobs is a dreamy pop song that repeats the chorus far too often But it sure does play a mean hook
Boyle  and maybe Sorkin though the domineering Sorkinese makes it hard to surmise  is doing something smarter even outright subversive
Though it is a decidedly dark portrait that shimmers into shape before us Michael Fassbender infuses a comic bounce to his performance that neatly counters his regular outbursts of despotism and bad fatherhood
If you loved The Social Network youll also love this film but if you hated it this one will feel as an insufferable drag despite the great effort of everyone involved Full Review in Spanish
Clever funny and utterly engaging this is both excellent and accessible
Thank you Danny Boyle thank you Aaron Sorkin thank you linesmen and ball boys the Ashton Kutcher certified piece of excrement Jobs has been successfully eclipsed by this inventive and kinetic glimpse into the life of tech icon Steve Jobs
Though JP Sniadecki doesnt elucidate any broad structural motive his film gradually adopts an engrossing rhythm among its clatter of steel and ambient chatter
By the end the real focus of The Iron Ministry isnt the train but the world zipping past it
Designed as a broadly impressionistic vision of the ways the countrys vast railroad system is used the pic is nonideological and intermittently engrossing catering to viewers especially drawn to this type of nonnarrative docu filmmaking
The parallel tracks of railways and cinema profitably converge yet again in JPSniadeckis outstanding semiexperimental documentary The Iron Ministry a pungently immersive evocation of traveling on Chinese trains
What emerges is a sense of an optimistic people well aware of how hard times can be but convinced they might be getting better
Coolly formal yet ceaselessly tactile works from lovely visual abstraction to the most material of physical concerns immaculate sound design a song for ears that crave the sound of rail travel and the insistent buzz of human commerce
The overheard conversations touch on social issues eg Chinas rapid industrialization and rampant unemployment addressed more thoroughly in numerous recent Chinese films
The Iron Ministry is neither boring nor confining which is just to say that its not a long trip through a faraway country Its a work of art  vivid and mysterious and full of life
Filmmaker JP Sniadecki withholds judgment and resists editorializing but the result is frustratingly nebulous and devoid of context
Seamlessly rides many rails through China to intimately experience sounds sights and even smells alongside restless people on the move through space and economic change
Amid the rumble Sniadeckis camera spies such a variety of life that it soon seems as though these trains provide a stage for the full spectrum of human activity
A subtly political film about the hopes and frustrations of ordinary Chinese citizens that is as dramatic in its own odd way as a kungfu costume drama
The Iron Ministry is a rather odd work for many reasons even if its depiction of class and ethnicity on various trains across modern China captures an essential moment
Grief unleashes the possibility of change in this wrenching drama allowing for an unexpected emotional thaw that rewards both stubborn optimism and traumatic resilience
An explosive family drama whose intense performances cant always compensate for such a heavyhanded scenario Bad Hurt nonetheless marks a promising directorial debut from playwright Mark Kemble
Bad Hurt wallows in heartbreak dashed dreams and death So why is it uplifting all the same
The material remains startlingly sincere leading with secure profound characterizations and a sensational understanding of toxic environments
An artfully drawn and beautifully acted film about a workingclass family grappling with a drugaddicted exvet son and a mentally and physically challenged daughter
The film hinges on a powerful central performance by Karen Allen as Elaine the wife and mother trying to hold it all together
Exhaustion of mind and body is the primary sentiment in this sensitively observed family drama drawn with an intimacy that is palpable and uncompromising
Throughout the filmmakers live up to the movies title But as the story comes to a close they opt to wrap it in comforting clich and they turn a miserable but credible viewing experience into a confounding one
A bittersweet drama about a family struggling to maintain dignity in the face of a variety of life challenges
Although affecting and well acted the family drama Bad Hurt is too airless and depressing to fully engage
Although Bad Hurt traffics in tough material it is filled with little moments of heart
Despite some powerful moments as the characters work through various demons and secrets the film feels more contrived than authentic compromising the emotional impact
In spite of the harrowing details the film builds compassion for even the most disturbing characters and scenes
Bad Hurt shows the truth is loud violent and unpredictable
Zariwny lacks Roths love for peeling flesh and tarcolored bloodgeysers making this all feel weirdly pointless
Scene for scene line for line gag for gag its basically the same movie And the original was no masterpiece to begin with
Travis Zariwny predictably scrubs all the edges and eccentricities down however fashioning another impersonally polished cover jam
A remakesorry rebootof the  movie of the same title in which attractive young people contract a super flesheating virus is not surprisingly more of the same
Who benefits from the existence of this film
Roth isnt exactly known for being critically defensible or for exercising directorial restraint but Travis Z somehow manages to up the gore quotient
The Cabin Fever remake is pointless primarily concerned with serving the financial interests of its producers not meeting the needs of franchise fans
Ive youve seen Eli Roths Cabin Fever theres no need to watch this  remake If you HAVENT seen Roths Cabin Fever start there and reread my previous sentence
This dud sets a new standard for the term pointless remake
Roths charcoal sense of humor is missing the cruel irony lacking its hellish zing
Unfortunately director Travis Z doesnt quite have Roths sense of humor about the whole thing and takes it all very seriously
A superior remake even if it is still abysmal nonsense
It feels flat throughout the bloodletting disappoints and perhaps most damning of all it neuters pancakes
There just is no point whatsoever to any of this childish witless nonsense
Unlike many horror fans Im usually open to remakes but Cabin Fevers humorless overly reverential redo might set a new standard for pointlessness
The whole thing is a likability vacuum
The symptoms are familiar Nausea fatigue and impatience accompanied by excessive eye roll and exasperation Yup Must be Cabin Fever a mindnumbing disease once believed eradicated
Crimson Peak is a disappointment because it doesnt have a particularly interesting plot nor does its characters end up being too charismatic even if it does show the visual inventivness of Del Toro Full Review in Spanish
Crimson Peak supersedes homage and becomes something new a reoriginal
Del Toro understands that sex and violence are what movies are for He wants to literally show you his characters insides
When Guillermo del Toro set out to cowrite and direct Crimson Peak a work of Gothic horror as gorgeous as it is preposterous the word restraint must have been missing from his moviemaking mission statement
Gifted Mexican filmmaker Guillermo Del Toro Pans Labyrinth makes an odd misstep with this overwrought gothic horror thriller which is so bloated that its more silly than scary
Crimson Peak is the perfect film for that small subgroup of moviegoers that doesnt see shivers and manic giggling as incompatible
The story quickly grew predictable and bland and the design of the ghosts and the role they played in the story left me feeling disappointed
Theres a point at which someone who is an expert in something can go from teacher to pedant
Yes the script is a tad silly at times but a game cast  Wasikowska was born to wander dark corridors  and del Toros eye for detail make for a stylish and scary shocker
Remarkable in every aspect and the classic tone is appreciated given the current tendencies in ghost movies Full Review in Spanish
This wannabe classic oldfashioned grand Hollywood production in the Gothic romance genre becomes ridiculously graphic and violent
Sumptuously stylized and soundscaped there are a few Victorian moments of sublimated psychosexuality But the sensationnovel plot and ghoststory atmosphere are too steamedout here chugging on the ending gets too stabby brutal and modern
As lush and atmospheric a film as the American cinema has created in years
The movies real star is the Sharpes decaying mansion a chilling living presence in its own right
It is fascinating for the myriad ways it veers right and wrong and it speaks to the stronger passages that their weaker brethren can be overlooked within reason
The set is spectacular the cast is stellar but Guillermo del Toros haunted costume drama is short on drama  and scares
Crimson Peak is a film more concerned with style than characters and the mysteryromancehorror plot despite an interesting theme the characters are thin and the plot is predictable
A film that duly fulfills its ambitions it chills and excites with its revisionist visuals and reminds us that the best stories with ghosts are allusions to deepseated societal fears and repressed emotions This is one film not to miss
You can feel Del Toros seal and personality but it unfortunately misses the level of his better works and you can feel the studios pressure all around Full Review in Spanish
With a reasonable smattering of gore and some absolutely breathtaking set and costume design del Toros final product is a truly unique beast
Keeping the audience off balance is key to any supernatural horror movie In that sense Drew Halls Convergence is pretty successful because for its first half I had no idea what the hell was going on
Convergence is a haunting tale of redemption  punctuated by Hostellike torture porn
A gripping police procedural thriller from writerdirector Alberto Rodrguez with understated character and political depth as Spains fascist past looms over all the characters
Rodrguez captures this suffocating and inescapably corrupt world through a noirish gothic cinematography
Allegory washes up against mystery in this superb period piece
Marshland is steeped in ominous atmosphere but the intrigue packs few surprises  its murky political undertow never quite pays off
With each new discovery the film becomes more intriguing and doubts are raised about almost everyone barring the director who has done a firstclass job
A film that keeps asking questions right up until its final haunting image The nighttime car chase is pretty great too
A visually striking noir thriller set against a backdrop of sex drugs and political intrigue
This gritty very intense Spanish thriller has the feel of an Iberian True Detective
How far you enjoy Marshland will depend on your enthusiasm for the ritual of genre  and in particular for the type of story where violence against women is mainly an excuse to probe the troubled souls of men
WOW Seriously just WOW This multiawardwinning drama from Spain ranks as one of the best films of  from a multitude of angles
The swampy lowlands at the mouth of Spains Guadalquivir River as lensed by cinematographer Alex Catalan help liberate the film from its genre moorings to produce a striking new form of Southern Gothic
A couple of mismatched cops in the immediate postFranco era investigate the brutal murders of two teenage girls in Alberto Rodriguezs satisfyingly atmospheric neonoir
Visually and atmospherically Marshland is suffused with an eerie oppressiveness entirely at odds with the regions reputation for lighthearted alegria
The novel settings  rural Southern postFranco Spain  sets this convoluted Spanish serial killer thriller apart
The elegant widescreen compositions and use of light and shadows are strongly reminiscent of Seven and Zodiac and the films eerie disconcerting mood brings to mind HBOs True Detective
Marshland has superb performances and a heady atmosphere but its greatest strength is finding resolution while letting the mystery be
Um filme que sob a superfcie de suspense policial constri um painel complexo de um pas lidando com feridas que jamais fecharam
While were still pretending that True Detective season  didnt happen we have Alberto Rodrguezs thriller Marshland to satisfy our yearning for moody detectives investigating a disturbing case that shakes their faith in humanity
Whilst its broad plot might be a touch too neat Marshland manages to say quite a lot about entrenched neglect and ignorance both within and without systems of power
Even if it stays true to the conventions of the crime drama it dares to explore complicated themes of the political and social issues of the time its set on Full review in Spanish
The most debilitating thing about this whole soggy mince pie of a picture is that its narrated by a dog
The Coopers are quite simply bad company
There are moments of genuine inspiration to be found but such is the intent to tick every Christmas box the good is outweighed rather heavily by the bad
Love the Coopers sets itself up to explore some interesting if not original questions but it baulks at probing too deep and instead retreats into schmaltz
Despite solid performances all round this isnt as good as the sum of its parts  its a series of cleverly constructed sketches culminating in a final act that never quite delivers the payoff
Love The Coopers may not be everyones cup of tea at the holidays but if you have ever had to prepare a special dinner or know that some people just dont get along in the same room you can relate
A bunch of Alisters plays out a string of intertwining plot threads all in search of a heavily eggnogged family hug
This is for Christmas movie masochists only
After spending time with them you wont know whether to hang the mistletoe or yourself
Full of clichs and dumb humor the film is barely entertaining even with Steve Martin as the narrator Full review in Spanish
This may look like its going to be a zany Christmas romp but its really a warm exploration of family connections essentially an American take on Love Actuallys multistrand comedydrama
With moments of unique lucidity this movie occupies its place among the classics that you can infinitely rewatch during the holidays bringing its acid outlook about these kind of gatherings Full Review in Spanish
Christmas With The Coopers employs an episodic structure that recalls Love Actually but falls woefully short of Richard Curtis festive romance
Wasted the grace of his talented comedians in a script that is nothing more than a sum of scattered sketches Full review in Spanish
Lots of photogenic folks serve up white whine usually weak laughs or suddenly candid chats with strangers The Hollywood contrivances are cringing Dundering in its efforts to melt your heart to slush
Love the Coopers isnt immune to a few overthetop moments and it never met a welltrodden plot device it didnt like but its unabashed heart and hopefulness is akin to a warm cozy knitted throw
It may take more risks than your standard Hallmark Channel Christmas movie but for a holiday film theres such little faith placed in our characters to live and breath
Its a terrible waste of talent Bah humbug
Full of embarrasing scenes that make you cringe its a shame that its director couldnt do best with the films talented cast Full Review in Spanish
Only occasionally does a moment ring true thanks largely to talented actors subduing a balky script like parents wrestling a petulant kid onto Santas lap
This one will leave you terrified of your own home jumping at every creak of the floorboard or bump in the night And God help you if you happen to live in a house with an attic
Very scary normal activity
Another quality horror film damaged by the increasingly dopey found footage format
Hangman doesnt rewrite found footage history but it plays to enough of the genres strengths in this creepy little home invasion tale
Doesnt offer connective tissue bellylaughs and scares Its just a shapeless creation that didnt survive the production process rendering a promising premise forgettable
Theres a whole lot of arid downtime in between the ostensibly colorful set pieces and on the whole the movie seems like it was edited with a blender
Freaks of Nature proves a lifeless combination of alien invasion saga zombie thriller vampire romance and highschool drama
Jack is in love but Maria and her family are driving him crazy Its almost like hes living in a sitcom which is great for him professionally He gives up writing a pilot for Space Bar Cheers in space and pens a sitcom based on his life How meta
The comedy is essentially sweetnatured the conflict that will keep our lovebirds apart so crucial to the genre does not go down quite so well
Amindblowing revelation from the silver screen Life is affected by the lifechanging decisions we make
Maybe there is some evolution to the Bond character in this movie from the old lowbrow misogynistic testosteronefueled Bond of the past into something more rounded grounded and thoughtful
Either a return to form a winking homage to the whole series or a dumbing down of the angsty introspective Bond of the Daniel Craig era Choose up sides
feels like a Best of Bond compilation  Bluenoses who once complained of Bonds indiscriminate bedhopping need not worry Its all Craig can do to suppress a yawn midseduction
For his latest adventure James Bond mixes the personal drama of Skyfall with the vintage globehopping action of the previous  movies
Its the end of an era but not the end of James Bond
feels slightly disjointed as if all the parts arent quite lining up and the progression of action that is supposed to build emotionally instead feels more like little more than a series of plot points
Spectre dances in the gate like an antsy thoroughbred from its very first frames as if it just cant wait to be a James Bond film
A film that takes from the classic bond movies but lacks a fundamental element for it to work a time period Full review in Spanish
Thats right In a film that feels about  minutes too long you dont get to the money shot for almost two hours And with that much time to kill you may find yourself dreaming of a few martinis  be they shaken or stirred
Instead of propelling Spectre over the top where it belongs Waltz instead becomes its unhappy avatar the thinly grinning face of sadly diminished returns
Its a solid serious spy film that still has a playful glint in its eye
plotting is convoluted maybe even tortured though I do wonder if all its conspiracies and turns dont constitute a MacGuffin
Most of the film plays like a game of Connect Four piling up and grouping together characters and schemes from Craigs last three films in vaguely related ways in the hopes that the connections to Bonds past will bring them some emotional heft
Essentially a wellacted Fast and Furious movie with more British accents Its good but not great
Spectre features all you would expect from a great Bond film amazing gadgets exotic locations stunts a new Aston Martin and a sprinkling of humour
With a perfect blend of a compelling narrative and breathtaking action sequences Spectre marks a return to greatness for the James Bond franchise whose past two entries had struggled to find the right mix of these two vital elements
A fairly muddled entry into the Bond canon
A film best enjoyed in theaters with your family and youll maybe have a good time but those looking for more than popcorn entertainment should stay away Full Review in Spanish
In their second  collaboration Mendes and Craig lighten the tone considerably Here at last we sense the two are actually having fun with a franchise that originally found success after all as equal parts adventure romance and spoof
Spectre is both modern and cutting edge as well as classic vintage James Bond
It doesnt exactly soar and the lack of levity grates yet the Spooks movie still delivers some appealingly oldschool mayhem
In the end it does not feel much different from an abovepar television episode but then that is probably no bad thing and it does leave you wishing that British filmmakers would make better use of an old reliable such as Peter Firth
Undone by sentimentality grumbles a senior secret agent in Spooks The Greater Good having been foiled when a longfavored rendezvous location proves a trap He might as well be talking about the film itself
Its nonsense but theres fun to be had in the endless doublecrosses and fans of the TV shows trademark gruff faceoffs wont feel disappointed
A solidly entertaining espionage thriller not up there in the first rank of spy movies but a decent enough placeholder to keep fans of the genre happy until the next James Bond
The film passes the time perfectly tolerably but it is no more comfortable in this less intimate medium than were ancient movie versions of The Sweeney and Callan
The big screen proves an unforgiving canvas for both the shows hitherto highend production values and its topical urgency
This may be bereft of the bombast that Americans expect from their spy adventures but as a modern spin on a John Le Carrstyle thriller its a stylish and smart action film
If you can overlook the films weaker elements however then Spooks still entertains consistently enough to make it a genre entry worth investing time in whether or not youve previously been a fan of the series
Led by the honorably dour Firth and the charismafree Harington MI is convoluted and dull
Written shot and cut to the demands of TV Dull and cluttered with overthetop intrigues
A dreary directtovideograde adventure that should have been terminated before it ever began
Nalluri confronts the familiarity of it all with commitment to speed and a general awareness that while his effort isnt going to look like a blockbuster it can periodically play like one
A film that like most of its ilk these days is more interested in action than motivation
Theres a surplus of doubledealings plot twists and international locales to justify the theatrical treatment
MI is no action bmovie classic but it manages to weave a complex and compelling narrative knot mix in some absorbing musings about the nature of doing right and following orders and pack in some nailbiting shoot outs
Spooks The Greater Good though nothing entirely new is a pacy wellcrafted spythriller that certainly matches the competition
Its difficult to call it great more like Spooks The Quite Good
Can an enduring hardy British television show make it on the big screen
Ultimately its Firths film and he ensures its never less than thoroughly watchable
A story about intergenerational bonding in a nonicky way its not for the whole family but it is one hell of a ride for anyone with a lot of miles on the biological clock
Given the film comes in at a shade under  minutes Weitz deserves credit for packing a lot in its an economical movie in every way but one rich in life
The fabulous Lily Tomlin finally gets the lead role she deserves in this smart engaging comedydrama
With not a wasted frame in its taut  minutes this is deserving of your attention as one of the years best movies even if its being released in the middle of August
This is the sort of accomplished indie film that we have been missing the last few years one that couldnt be made in the mainstream but which tells a recognisable important human story without feeling preachy
A bracing very entertaining little road movie with more grit and gumption than the subject matter might have suggested
a lot more conventional than it pretends
A fresh and intelligent film about scorned women but also really strong A comedy to enjoy with your family Full review in Spanish
A comedy with hints of drama and a simple plot but with a strong message Full review in Spanish
Tomlin is undone by much of the dialogue even when you get a sense shes putting her own brash spin on it
Lily Tomlin is excellent as Elle Full Review in spanish
Grandma is a transgressive and innovative option for the audience and not just a film about unwanted pregnancies abortion homosexualtiy or feminism Full Review in Spanish
Its a movie that invites reflection but is also entertaining Full Review in Spanish
A modest but effective feminist dramedy Full Review in Spanish
Lily Tomlin is a tridimensional and unforgettable character you cant keep your eyes off the screen Full review in Spanish
Paul Weitz the cocreator of American Pie wrote the clever consistently funny surprisingly affecting script especially for Tomlin who hearts back with one of the best performances of the year
Grandma is a crowdpleaser potent with Tomlins DNA from the razorsharp wit in dialogue to the way the movie itself speaks to sometimes pointed but necessary ends
Grandma never intended to be more than the feel good movie it is Full Review in Spanish
Wonderfully aggressively feminist a rare crossgenerational portrait of two women getting to know each other amidst a crisis Smart and acerbically funny
Grandma may be a short film but unlike many other films in the genre does not let go at any time thanks to its beautiful story and great performances from actresses Full review in Spanish
Bahranis most accomplished film to date
Its a mesmerizing and disturbing portrait elevated by two strong performances
Audiences will no doubt notice the powerful and important role played by the score from Australian screen composers Antony Partos and Matteo Zingales important and remarkably effective
Worried about how youre going to pay that next clump of bills Then  Homes will bring on long and lasting nightmares
Homes isnt just about chasing the American dream its titular dwellings represent after all but about its destruction
The two powerful lead performances in this tough emotional film are strong recommendations on their own
The rackets and scams it exposes are all real the result of extensive research Its a gripping thriller with good guys and bad guys but everyone in it is a victim including Rick Carver Bahranis achievement in this film is breathtaking
It is smartly if predictably told with a flair for capturing moments of heightened emotions regularly involving people we meet only once or twice
It is perhaps the most compelling film yet made about the global economic downturn and the everyday people whose lives it tore apart
A powerfully affecting modern tragedy
a dramatic thriller about predatory capitalism that cuts deep to the economic bone in the way it reminds us how one persons attaining his dream usually comes at the expense of someone elses
A brutally honest compelling drama about Americas housing crisis
A breathless thriller with expertly shot you are there camerawork and a rising score but it also comes from moments of conflict that are pitched at a very high level Emotions are high lives are at stake and something is very very wrong
Homes is a timely topical drama that packs real punch the deeper it goes into the rabbit hole of lawless capitalization on destroyed lives
A sizzling righton morality story pleading for justice for the vulnerable workers who lose their homes
An electric melodrama  with Andrew Garfield in his best role so far supported by an outstanding supporting cast
Michael Shannon gives an exceptional performance as a real estate broker who is absolutely determined to succeed in a business where most are failing by any means necessary This film effectively shows the human tragedy of home foreclosures
Shannons coldblooded villain is compelling too snarling a string of heartless maxims and giving us a chilling glimpse of American capitalism red in tooth and claw
By the way Shannon Boardwalk Empire is brilliant in the role I truly admire this actor He is going to win the Oscar some day His performance in  Homes should have him a nomination
Sustained rhythm urgent framing and a perniciously overbearing score ensure this second venture into the darkness of a systemic failure will not be forgotten so quickly
Tucker Green certainly isnt shy about testing her audiences patience and while she can sometimes get a bit too enamored with her own moody elliptical atmospherics theres clearly a unique imagination at work here
The main body of the movie is a slow repetitious study of the familys daily life
An intimate earnest and astute depiction of a family dynamic picking up on all of the idiosyncrasies and nuances of a somewhat relatable household full of repressed feelings
However willfully obscured the narrative may be the emotional truth of every moment in the moment is always piercingly clear and punishingly accessible
Its nicely shot superbly acted and extremely frustrating
An impressive sliceoflife drama thats tinged with unease and strangeness
While some might find its conclusion faintly ridiculous buy into the premise and youll experience an unexpectedly joyous finale
A soulful drama that heralds the arrival of a new voice in British cinema
The title and central concept may be ripe with religious allusions but theres no heavyhanded allegorising here Nor does the film ever veer off into soapy drama forget about any shock reveals or intrigues
A mysterious and intimate fable in the guise of gritty social realism simply and powerfully acted
Second Coming is an enigmatic and quietly impressive piece of storytelling
Overall this is a British film of rare ambition and imagination which builds to a final image of heartstopping grace
Heralds the emergence of a major new filmmaking talent in Debbie Tucker Green
The pace will be too slow for some but still if you make it to the end youll come away chuffed
Leaves no doubt that its central supernatural event is  real yet it makes absolutely no case for it whatsoever and refuses to even engage with it
In a highly nuanced central performance Marshall is both engaging and evasive perfectly matching the fluid tone of Tucker Greens enigmatic urban parable
We see a family at its best and at its worst with the grumpy mealtime silences and the playful banter Much of this is down to the firstrate performances from Elba and Marshall
Its an effective thriller that sets out to scare the living daylights out of even the most skeptical viewer and delivers in spades
The Pack wont make anyone forget Jaws  or even Cujo
We learn nothing much about the family members except that outsmarting dogs doesnt seem to be their forte Also not a forte of anyone involved in this project originality in picking a name
Despite the interesting openings the film punctures in its formula the detours from the expected course of events are short and return us firmly to known genre territory
The attractiveness of the package and the steady pace at which it doles out the action more than make up for the character shortcomings in one of the best animalsrunamok horror movies in recent memory
Ultimately these feral dogs pose no danger that couldnt be solved by staying inside boarding the windows and barricading the doors Its not a bad approach to the film itself
Take Me to the River falls woefully short on offering a serious contribution to the history of African Americaninspired music
An often vibrant documentary about the making of an album of the same title seeking to connect generations
Take Me to the River is at its most interesting when zeroing in on the backandforth between musicians of different eras
Not so much a traditional documentary as a musical celebration of Memphis status as what host Terrence Howard calls one of the special places on this Earth
Theres magic in the music and the men and women who make it Maybe they put Mavis Staples at the end because nobody could follow her
Take Me to the River includes just enough history of the civil rights era to lend it gravitas The colorblind recording practices of studios like Stax were an anomaly at the time and are well worth noting
An overdue homage to a city that for close to a decade was home to the second largest black business in America
Theres some talk about the collaborations significance and theres some racially charged history to relate but the real point is to hear these oldsters one last time to remember and maybe even discover what they gave us
With better direction and execution and a more substantial budget and judicious editing it could have been so much more
A must for music lovers
While the work is joyous and respectful the movie is poorly directed frequently decimating pure musicianship to spotlight banal conversations that add little to the overall flow of the feature
It feels like a mishmash effort overall more a home movie than a theatrical release Thats fine If you approach it on those terms you cant help but feel the love too
A moving tribute to a grand piece of Americana
The premise for this documentary couldnt be more stilted and some of the matchups are enough to make you wince But there are a few striking intergenerational moments
While those sessions result in full songs some of the most memorable iconic tunes in music history this film never coalesces into something greater than a collection of mildly interesting pieces
Three of the guitar marvels shown here  Hubert Sumlin Teenie Hodges and Skip Pitts  have died since filming This is a marginal but worthwhile footnote to their legacy
Flawed as it is River reminds us where all the great music came from
Even if the filmmaking loses focus at times the personalities onscreen are consistently engaging and the experimental collaborations yield some toetapping tracks
Take Me to the River is a goodspirited but patchy documentary less about dropping beats and more about dropping names
While you may stick around for the stories the films true draw is the music
In form and content plotting and politics the film is egregiously disguised as a smug inspirational piece but in reality it is the cinematic equivalent of being offered free hugs from Donald Trump
A strange and relaxed film that feels like the Levinson of the good old days Full review in Spanish
Rock the Kasbah hides a much more predictable and tedious story of redemption Full review in Spanish
We stand before a lazy piece of work that includes well established actors and avoids totally ridiculing itself because of its protagonist Bruce Willis being tough Kate Hudson showing off her beauty and an excellent soundtrack Full Review in Spanish
An interesting cast and premise that feels like a missed opportunity Full review in Spanish
Bill Murray is everything in this movie sadly its not enough A weird and failed movie Full review in Spanish
Leaving aside the galloping misogyny Rock the Kasbah just isnt remotely funny or smart
Turns out its ruinously hard to raise a smile at IEDs and arms deals gone awry especially in a conflict thats still raging No doubt its meant to be satire but it feels like profiteering
Rock the Kasbah feels likes the pathetic last wheezes of the Baby Boomer Entitlement Project Bro Division
On paper this could have been excellent as it stands its painful and futile for all involved
Murray has rarely been so charmless in a movie
Singularly fails to charm  and even Murrays on faltering form
A resounding misfire
Its neither funny nor feelgood but its biggest crime is wasting Murray left trying to mine laughs from unfunny circumstances
Unfunny and also casually offensive
Its a strangely lacklustre film thats happy to plonk Murray in a vaguely interesting crisis situation  in this case wartorn Afghanistan  and just hope for the best
Barry Levinsons film loosely inspired by the true story of reality TV show Afghan Star comes at its subject matter from entirely the wrong direction
A dogs dinner of a movie which not even the usually reliable Bill Murray can raise above the level of confused cliched claptrap
Murray is entertaining but the plot seems entirely arbitrary with characters such as Kate Hudsons happy hooker Merci popping up without much rhyme or reason A real disappointment
Murrays performance really is one of very few saving graces in this truly underwhelming piece of cinema
Its a fascinating look at modern journalism  but perhaps not always for the reasons its makers intended
Its endeavor in capturing the global downfall of the media and this downfalls continuing eradication of one of the most respected professions in human history is every inch the hard truth
Mapes and Rather resent the way that the debate gets bogged down in the documents without focusing on the larger issue but the film gets bogged down in just the same way
The casting cannot save a slightly hohum story that crosses the Atlantic badly
The movie is wordy worthy and often bombastic in dialogue with Blanchett the backbone of the story while Redfords careful nuanced portrayal of the nations most trusted news anchor is a study in understatement
Solid but uninspiring
Redford is marvellous but even Blanchett cant do anything with the screenplays messy rendition of Mapes who too often plays like a mushy romcom heroine
Blanchett is terrific as Mapes juggling hard journalism and home life serving breakfast to her son while fielding toughasnails phone calls
That generic title obscures a surprisingly complex exploration of the reallife events surrounding the fall of iconic American newscaster Dan Rather in
Anchored by a blazing performance by Cate Blanchett as the resolute Mapes and a studiedly cooler one from Robert Redford as legendary US newscaster Dan Rather Truth delivers a gripping account
As a case study in journalism and ethics the film is intriguing As drama it stutters
Truth is well made but a little too pleased with itself
A captivating real life drama thats up there with other great films about journalism Full review in Spanish
A well built script that balances the professional work aspect of journalism while also showing how this affects their personal lives Full review in Spanish
A good drama about how to handle an editorial department when there are interest conflicts with the government Full review in Spanish
The movie fails by its own premise why do we need to feel empathy for two journalists that screwed up no matter how you feel about Bush Full review in Spanish
Predicatable old and conservative using the old strategy of manequism of a true story Full review in Spanish
Sadly Vanderbilt isnt Fincher and neither is Pakula his work might be good but it lacks personality Full review in Spanish
A great film that surprisingly didnt make a lot of noise hopefully time will give it a place that praises all its virtues and great performances Full review in Spanish
Based on true events the film stands out because of the great performances by Blanchett and Redford Full review in Spanish
A heartfelt and earnest homespun comedy about the family ties that bind us all together
A novel setting surrenders to a seriously corny rural romance with not nearly enough laughs to put it over
Good nature counts for something but its all so comfy you may fall asleep
Apparently too close to the story to recognize how ill suited she was to translating its charms to the screen Trigiani has emerged with nothing but corny stilted Americana like something Garrison Keillor might burp out on a really off day
The warmth of spirit behind this project allows its missteps to be mostly forgiven
Director Adriana Trigiani adapting her bestselling novel delivers the hackneyed material with good cheer eliciting bright performances from an excellent ensemble cast
Nothing feels believable in Big Stone Gap a bungled charmfree look at smalltown life in the South in the late s
Trigiani doesnt push the material into any energetic directions but she does capture the sway of the town better with atmosphere than dramatics
The plot itself has little momentum and what should feel dramatic instead feels inert
Its tempting to say Big Stone Gap is greetingcard pretty and sweet but that doesnt quite fit because these days greeting cards seem to have developed a bit of an edge Big Stone Gap is edgefree
That the creaky romantic comedy Big Stone Gap attracted such a large ensemble of talented name actors only makes this countryfried turkey feel like an even more notable miss
Unpretentious and it goes down easy like sweet tea brewed by sunshine
Sure the script can be simpler than a diner menu And at times the nostalgia seems manufactured like the goodies at a Cracker Barrel gift shop butBig Stone Gap proves to bea nice change of pace from the summer popcornmovie season
The movie ambles along amiably enough for a while its better if you are a fan of one or more members of the cast
Brutally mediocre
By the time we get to the final borderline ludicrous moments in Big Stone Gap I wasnt even surprised by the overwhelming sappiness of it all
Flat comedy about smalltown s life lacks laughs
Bleeding Heart may deliver some much needed catharsis but its ultimately a hollow film that isnt concerned with consequences or the echoing cycle of violence just vanquishing the bad guy reclaiming a dime store sense of freedom and not much more
Bleeding Heart is a fascinating film though a tad predictable If nothing else it recognizes the underappreciated work of Jessica Biel and Zosia Mamet and places writerdirector Diane Bell at the forefront of exciting indie filmmakers
Ironies are sliced thick and violence offers little surprise but the feature manages matters of the heart quite well even with little narrative to explore
Good movie bad title
Bleeding Heart shows that all the asanas in the world cant change a man with hate in his heart
Biel turns in a fine performance as the unlikely violent hero but Mamet excels in a role far different than her Shoshanna on HBOs Girls
Despite its nonexploitative approach to its subject the film is too schematic and obvious to have the desired impact
Theres a core of a good idea here but the film itself is pale and anemic
It pales in comparison to the recent epic saga tweeted by Aziah King about two strippers unapologetic road trip from Detroit to Tampa Fla that went viral on the Internet
Though violent and thrilling Bleeding Heart is first and foremost a film about healing
There may not be much surprising here but this is a smartly sensitive depiction of abuse and redemption that never descends into caricature
A potentially intriguing examination of family bonds and abusive relationships is relegated to the background in this generic revenge thriller
If anything this reminds me how much Id like to see both Anderson and Biel  hell even Mamet  in a better film than this
Bleeding Heart takes subject matter deserving of mature thoughtful treatment and distorts reality into a series of soap opera clichs
Why does Michael Shannon get the best written part in a lesbian drama
May be a familiar DavidversusGoliath tale it is also an inspiring and hugely emotional experience due in large part to the powerful performances
Freeheld has a very worthwhile subject and its Alist liberal actors do their best
Worth catching
Sollett mounts a fullscale attack on the tear ducts as he rolls out scene after scene of inspiring oratory heartrending illness and fullon liberal dogooding And it works like gangbusters
Moore gets to wear a striking wig but has no dialogue that makes you look twice And the more Stacie cries over her saintly lover the more guiltily conscious you become that your own eyes are waterfree
Freeheld is earnest and wellintentioned but often feels like it is trying to teach us all a lesson It works best in the tender emotions of one woman facing the awful reality of losing the woman she loves
Perfunctory direction renders dull what could have been a compelling story about the need for equality in a job where decades worth of gender discrimination has been compounded by systemic homophobia
Flat and lacking the archly deadened attributes of Todd Haynes Carol a quartet of Worlds Greatest Actors work hard to animate a timid movie
An acceptable drama that had more potential in its noble story Full review in Spanish
It falls in the same cliches about tolerance and doesnt really add anything new or original to the subject Full review in Spanish
The movie manages to put its ideas out there maybe not in the most subtle way but it gets the discussion going and thats the idea in the end Full review in Spanish
Una pequea joya bien actuada bien contada con un elenco extraordinario aunque muy modestamente producida
Even if its a small film the performances and the relevance of its story save it Full review in Spanish
The performances are top notch and the case is a worthy portraying example of human rights addressing issues of equality tolerance and homophobia Full Review in Spanish
A manipulative drama with good intentions that ends up being a cartoon of what it wants to say Full review in Spanish
Thank God there are good performances that work but dont save in this movie Full review in Spanish
Great themes and story and a spectacular cast however its predictable and it takes the easy way out Full review in Spanish
In the quest to make An Important Film director Peter Sollet Nick and Norahs Infinite Playlist Raising Victor Vargas has produced Teachable Moments instead of complexity or authentic emotion
Freeheld is ultimately a film without a soul without character full of clichs to a genre that simply does not need them Full review in Spanish
It offers a CliffsNotes encapsulation of Edgar Allen Poes most enduring works for viewers unacquainted with them
It has its moments and will be a welcome respite for any middle schooler sitting through a boring lecture But if we were ever asked if we wanted a second viewing wed have to quoth the raven nevermore
In spite of the common source material and tone of oppressive psychological horror these shorts feel like they could be the work of five different people
Its no substitute for Poes atmospheric writing but its a pretty good introduction to it
Despite working from extraordinary subject matterEdgar Allan Poes various tales of the macabretoo many entries in this animated anthology are disappointingly ordinary
The voices of the late Christopher Lee and Bela Lugosi reading from the text isnt quite the same But Garcia compensates with a different animation style for each of the five stories
The best sections reverberate with the power of Poes words As such they serve as apt tributes to yarns that will always be more formidable on the page than on the screen
Just in time for Halloween comes this singular tribute to Edgar Allan Poe from veteran animator Raul Garcia
Extraordinary Tales reminds viewers that animation can enable an artist to realize an individual vision even on a limited budget
With its weak framing story and hastily assembled formatting Extraordinary Tales simply is the sum of partsnot a whole
Poes storytelling gift is so timeless and the voice actors assembled so captivating that Extraordinary Tales cant help but work on some level It just never quite rises above that faint praise
Garcia brings these classic Poe tales to life with the notable aid of atmospheric score by Sergio de la Puente
Extraordinary Tales leaves us wishing for more more running time more of Poes material more of Garcias macabre animated magic
The fact that an author who died  years ago continues to be a draw for moviemakers says something about Edgar Allan Poes imagination And heres a feverish new animated Poe feature  released just in time for Halloween
the definitive works of Edgar Allan Poe on the big screen
Five stories by Edgar Allan Poe get varied treatment in this striking but inert animated compendium
Unsure of its targeted demographic Tales straddles the tonal line flipflopping like a fish left on the dock
Poes creepy unevenly adapted tales arent for young kids
So much is made of the Marvel Cinematic Universe but what DC is doing with these animated features is much more organic
Batman Bad Blood assures everything remains status quo in terms of another outstanding actionpacked and entertaining effort starring the Dark Knight
Superviolent superhero mayhem with a complex story
highlypolished garbage from almost the beginning with no relief from its elderly ministrations all the way through to the end
An impeccable proposal with a nostalgic tone that almost touches ingenuity Almost Highly recommendable Full review in Spanish
As gripping and as smart a piece of entertainment as youll find this year
For the most part Bridge of Spies is a sober reflection on America actually living its values and living up to its promise
Steven Spielberg takes on the Cold War with a stately sentimental thriller that gurgles along with quiet intensity only occasionally finding a real spark of energy
Bridge of Spies is a warm portrait of a friendship an excitingly intricate story of realistically scaled suspense and a visually ravishing and lovingly crafted rendering of midcentury America where how you look matters much less than what you do
A fascinating story where the interactions between smart and unpredictable characters is the main focus Full review in Spanish
With Hanks on superb form Spielberg delivering a visually stunning and surprisingly feelgood thriller Bridge of Spies absolutely satisfies
Simply the best film Spielberg has made in decades Avoids the sentimentality of mostperhaps because of the inclusion of the Coen brothers in the screenwriting team Their best work in a decade as well for that matter
The strength of the film lies in the bond developed between two men A bond built on mutual respect and common understanding in a time when trusting the enemy could get you killed
With a mostlycompelling story and a particularly excellent performance from Oscar nominee Mark Rylance Bridge of Spies is an enjoyable Cold War tale that manages to bring enough tension to its narrative to make it a worthwhile journey
Steven Spielberg goes into Stanley Kramer mode for Bridge of Spies a socially conscious tale of touchandgo diplomacy at home at the office and on the global stage Bluray
A lesson in how to direct movies and proves why Spielberg is matchless in his ability to place the camera as a narrator who tells a story through images Full review in Spanish
The master of multiplexes certainly found a fantastic tale for his st feature
With an overriding penchant for nobility above all Bridge of Spies has a welcome straightforward charm despite its ultimately understated impact
Bridge of Spies is a quality film featuring loads of commendable work but Spielbergs glossy and exceedingly upbeat take on the material might make it more of a crowdpleaser than an Oscar contender
Locates the sweet spot for so much superficially disparate recent Spielberg morally engaged and reflective yet playful and funny and loose  and with the formal beauty of one of American cinemas greatest mainstream artists
Bridge of Spies is an inspiring drama about a man like any of us who struggled to do the right thing Full Review in Spanish
Its reminiscent of a Clint Eastwood film solemn with no provocative scenes filled with images that remind us of what America was built on Full Review in Spanish
Any doubt that Steven Spielberg is a latterday Frank Capra can be put to rest Bridge of Spies is a celebration of an Everyman empowered by Americas cando spirit of justice and decency in this case doing nothing less than saving the world
Dr Cabbie does something interesting It takes a rather serious subject  the fact that immigrants bring vital skills to Canada that are cruelly wasted  and milks the issue for its comic possibilities
A prime example of a movie with a meaningful message that fails to deliver on its potential
Its still a distinctly Canadian movie delivered entirely in English and lacking the lengthy and wellchoreographed dance performances that have become a trademark of Indian cinema
Functioning as light satire Dr Cabbie never quite digs deep enough
The Prophet gets a Disney makeover thats ultimately even more disturbing than it is cloying
If you are a fan of Gibrans work this film is recommended for those sections just be prepared for some schmaltz to go along with the transcendentalist philosophy
Simply put this movie is gorgeous from start to finish
What makes The Prophet worth watching is the animation
Neesons gentle sonorous voice perfectly underpins these visual extravaganzas
an acquired taste but a frequently powerful examination of spirituality and relationships for those in the right mood
As a visual experience many of the animated sequences are beautiful But without the focus of a traditional screenplay the story drags a bit and even feels tedious
The animators are uniformly talented but wildly divergent in styles
Gibran encouraged his readers to savor every drop of life even the bitter ones and viewers should relax and savor every panel of this film
The simple Disneystyle look of the framing story merely feels like a delivery system for those dazzling depictions of wisdom and the most memorable lesson is that there are so many more glorious ways to tell an animated story than Disneystyle
The real star of The Prophet is the animation The visuals make this a film a craft of art that also has spiritual uplifting of words
Too childdirected and broadly characterized the commercialminded Disneystyle highstakes is a padding out of ponderingness to ponderousness This adaptations more posey and cutesy than questionposing or poetically profound
Theres no room in theatres right now for movies that just want to ilustrate text no matter how deep and poetic it is Full review in Spanish
A film with a strong message about achieving what seems imposible Full review in Spanish
An interesting cinematic experience that mixes poetry music animated sequences and relatable characters Full review in Spanish
An affectionate and endearing proposal that dignifies the human spirit Full review in Spanish
The fact that the film abuses some narrative elements make this a tiring tedious and at moments a boring film Full review in Spanish
As it often happens in these type of films the end result is uneven but there are a couple of outstanding segments Full review in Spanish
An interesting project but it misses its target completely Full review in Spanish
An interesting experiment that compiles different styles of animation in a single film Full review in Spanish
An obsessive friendship between two teenage girls unfolds with equal amounts of tenderness and terror in Breathe a modest but acutely observed and affecting adolescent portrait
Theres no doubting Laurents skills as an actors director
Director Mlanie Laurent conducts this minute drama at a leisurely pace getting terrific performances from the two leads
Its only the second feature for actressturnedfilmmaker Laurent whos probably still best known in the US for her avenging cinephile in Inglourious Basterds yet Breathe crackles with the intuitive control of a seasoned auteur
Breathe is also about the great intimacy between girls that is an exclusively female domain the closest boys can achieve is a bromance but dont even think about cuddling and stuff bro
I can tell you that Ms Laurents direction is astute and economical that both of the films young stars give fine performances and that Breathe is a very good title for a film that ever so gradually takes your breath away
Josphine Japy and Lou de Lage are painfully genuine as two very different but equally lost young women
The entire piece is precisely woven together from script to performance to execution and the result is a chilling study of emotional annihilation and its aftermath
These are extraordinary performances and considering Frances way of nurturing female talent Japy and de Laage could be at the dawn of year careers Theyre that good
Nuanced sensitive and unflinching
The subject is the kind of intense frenemy relationship only possible between teen girls and Laurent guides her young stars to naturalistic performances that convey the joys and miseries of adolescence
A harrowing examination of teen angst as one girl navigates the highs and lows of an unstable friendship
If you think the emotional volatility of adolescent female friendships is a uniquely American phenomenon Mlanie Laurents potent psychological drama is evidence to the contrary
In some ways it plays like a horror movie in other ways its almost a documentary
Laurent has an excellent eye for shot composition and cinematographer Arnaud Potiers crisp photography aids the director in creating an enviable set of gorgeous and memorable shots and the film is consistently visually compelling
Breathe offers ample evidence of the growing confidence and skill of Melanie Laurent as a director
With boys  and men  on the side the females of the film anchor it with ease once again proving that a good drama is dependent on everything except the actors genders
Breathe is a deftly observed look at teen power games and how closeness can turn to animosity in one miserable night out
A modest but keenly observed comingofage drama about an average suburban teenager
The examination of the unknowable and undeniable power and pain of a broken friendship on display in Breathe is truly something to behold
Its Mulligan to whom the biggest plaudits belong But the rest of the film cant quite live up to her
Some of the most interesting aspects of the suffragette movement are glossed over without examination and you get the feeling that while the film is quite decent an opportunity for something outstanding has been missed
A maximum of four minutes on screen by Meryl Streep give the story an even more special meaning Full review in Spanish
Mulligan transmits all the emotions of her character and the audience  especially women  achieve to connect with her Full Review in Spanish
An astonishingly tedious drama
A very conventional and safe film about a very singular and brave event in the postvictorian era Full review in Spanish
With a plot and characters based significantly on historical events Suffragette does succeed in highlighting the or at least a systemic oppression of women through manipulation humiliation and violence
Its Mulligans picture all the way though and once again she shows the depth and range that makes her one of the leading British actors of her generation
Suffragette highlights how much the world has changed for the better over the past century However it also highlights there is still much room for improvement
Suffragette is vividly made enough that even if it glosses the full breadth of the suffrage movement it still introduces viewers to the history of womens activism and gets them intrigued enough to want to learn more
The problem is that it is simply a politically correct film and could be much more Full review in Spanish
An earnest melodramatic and probably necessary history lesson
An interesting story sadly we have too many loose ends at the end Full review in Spanish
An important film serious well crafted and relevant Full review in Spanish
An overly straightforward plot ditching any complexity for an awkward simplicity but thankfully the cast is topnotch and the characters are compelling
One of its most appreciated accomplishments is its ability to paint the heroic events of a social movement that happened more than a hundred years ago with a modernday relevance sure to resonate with audiences of today Sadly no one will see it
Sarah Gavrons focus on one character helps lift the film above slogans and marches and makes it more personal But when the fight becomes more of a war we lack the attachment to the movements biggest losses
This is an urgent persuasive if cloyingly conventional history lesson with a story that Hollywoods barely touched on before
This is a beautiful film not just in its narrative but also in its astounding cinematography and production design
Suffragette barely delves into the inner workings of its political movement opting instead for an admittedly sympathetic everywoman protagonist
A lot more fun than many heftier supposed romcoms thanks to the timing and chemistry of its leads
There are enough twists turns and strange little bumps in the road to make this welltraveled journey to romantic comedy bliss surprisingly worthwhile
Larkish and nicely confident Brit romantic comedy enlivened by sparkling performances and a tangy quite original and observant script
Even though you know how things will turn out its amusing along the way
MAN UP is that rare romance fueled comedy that will ably entertain both men and women
Perhaps the whole thing is intended as postmodern  But its not funny ironic its not funny straight its not funny anything Its embarrassing from start to finish and Pegg looks deeply uncomfortable in it
Im a sucker for movies where smart people fall in love Smart wellwritten people anyway And about  per cent of Man Up is that sort of movie This is enough
Its definitely not Peggs finest professional hour but as fizzy romantic comedies go Man Up has its share of surprises and enthusiasm for the material
Bell and Pegg both terrific comic actors mine small gestures and reactions for laughs But theyre at their best when theyre talking and they talk a lot in Man Up
Aside from the silly title and disappointingly pat ending even the romcomaverse will find something to love about Man Up
Man Up has a couple of bits that dont quite work and the ending is just silly but for the most part Pegg and Bell carry this thing through sheer force of personality
Honestly it would be nice if on occasion a film like this at least tried harder than making the main character somewhat dissatisfied and assuming that would be enough to get everyone identifying with her
Both actors stay sharp through some pretty degrading moments and if Palmer and screenwriter Tess Morris are bent on serious buttonpushing in the closing scenes at least they garnish it with playfulness and wit
Bell and Pegg are utterly defeated by a screenplay that favours plot over characterization to an almost surreal parodic degree
Formulaic romcom has lots of sex talk drinking
The kind of romantic comedy that reminds you why the genre continues to wheeze and sputter and die
Lake Bell and Simon Pegg make for an amiable pairing in a hugely enjoyable and fastpaced comedy
Man Up benefits largely from the hilarious dynamic between Pegg and Bell and the humor alone is strong enough to recommend the film
Its all very gener ic and predictable but not in an entirely bad way Man Up has its charms and itll raise a smile perhaps even a chuckle or two if youre looking for a nofrills romcom
For all the romcom conventions of the film and there many it has a lowkey charm and an adult take on expectations disappointments and missed opportunities
If the purpose of a horror movie is to be horrific then Hellions gets the job done
An unpleasant muddle of the visceral and the abstract
A nifty way to get freaked out for eightytwo minutes Hellions is Alice in Wonderland if Alice in Wonderland was about unwanted teen pregnancy
Bruce McDonalds latest is a genrefreakonly affair that even at  minutes feels like a joke that takes much too long reaching its punch line
The thrills diminish very quickly as Pascal Trottiers script runs out of ideas collapsing into incoherence in the final reel
Hellions is unsettling but in all the wrong ways
McDonald does manage to craft a few striking visuals that take advantage of holiday elements like pumpkin patches and costumes but other shots border on cheesy and overblown
Even at  minutes Hellions feels padded out running out of plot after an unsatisfying expository scene halfway through and relying on visual gimmickry to make up the rest
When it comes to the visual scheme of Hellions less would have equaled more There is enough good left to savor however to prove more treat than trick
As the terror shifts from suggested to explicit the eeriness wanes and this supernatural story becomes muddled
Hellions is art book horror something to flip through but never truly eerie or scary
A halfcocked fusion of Rosemarys Baby and a home invasion flick
A perfect light midnight movie that should go great with light drinks good company and a warm blanket
Hellions is a bigger Halloween bust than the house that gives out dental floss to trick r treaters
Some of the horror tropes being played with here  Rosemarys Baby is a clear touchstone  are staying up way past their bedtime
In its best moments Hellions is interesting and perplexing and worth soaking in in the sense that one would admire a Hieronymus Boschinspired painting But without at least one foot on the ground it doesnt inspire fear
This creepfest initially serves up some good horrorflick fun but seems to lose the plot as the wind kicks up and the sky changes colour and the chase goes on and on
The scare factor wears down pretty quickly
Its all fairly preachy nonsense and never as cerebral as McDonald thinks it is
No matter how good these aesthetic tropes are at cultivating fear however the content inevitably falls prey to nervous laughter before sadly devolving further into eyerolls
Apparently this is a sequel to Chiens Zombie  which according to reviews is even nastier Count me out
a problematic cavalcade of uncomfortable male fantasies and their punishment but there is something to its anythinggoes insanity that proves irresistibly infectious especially at the midnight hour
Its unbelievably awful
Well manufactured but no strokes of genius here the best thing of it is that Vin Diesel and Micahel Cane are in it Full review in Spanish
As one random and chaotic supernatural brawl follows another the decision to keep the lead villain on the sidelines becomes increasingly puzzling Why make a movie about a witch hunter without a memorable evil witch
If youre willing to put the faults in the script aside youll have an entertaining movie with good action sequences and humor Full review in Spanish
Not only has Diesel plopped the mantle of Cage on his shoulders he seems to have borrowed one of Cages justly celebrated hairpieces
Esentially consists of everyone involved hoping this is successful enough to make sequels
The plot is a bewildering blunder expecting you to jump right into it even though youre never sure you understand the storys progression
Vin Diesel delivers on his leading man role in this action packed film Full review in Spanish
Its not even a letdown because you could see from the start it was going to fail Full review in Spanish
It is terrible It is fabulously entertaining
This is all too bland too serious The characters needed a significant injection of charisma
An abortion and one we can blame on the success of the dimwitted Fast and Furious hits
Hohum except that its all set up for a sequel and I wouldnt bet against it Vin Diesel is a hard man to kill
Vin Diesel makes a bid for yet another franchise with a supernatural action romp thats both deeply ridiculous and enjoyably entertaining
The special effects are poorly designed and the laughs are readily found though probably not intentionally
A yarn that is just entertaining enough for you to walk out of the theater satisfied but not enough to have you begging for more
The Last Witch Hunter has some great production design and digital effects but the movie directed by Breck Eisner is an interminable bore which foolishly places the weight of the film on Vin Diesels broad shoulders
The greatest disappointment of all is that the ending promises an unfortunate sequel That comes off like a threat
Exposition backstory and actiondialogue clich are all tossed into the pot and stirred up real bad A hocuspocus hodgepodge of balderdash and humbuggery The only smells wafting from this cauldron are the odour of silliness and stench of stupidity
Picture Harry Potter for Emo kids No wonder theres already a sequel in the works Brood on Vin
It may not be a good film but its made  or at least acted  with a bizarre affection that goes a long way toward being endearing
An engaging provocative and pertinent piece of cinema
Too light too silly
A smartly written cheekily engaged glibly enjoyable political satire
Theres nothing new about the political smokeandmirrors the BullockThornton relationship fails to convince and the inevitable idealistic resolution feels unsatisfying and unearned
Even the title is unwieldy and that sets the tone
With characters speaking almost exclusively in soundbites anecdotes proverbs and quotations its like being cornered at a convention of fortune cookie fillers
Billed as a comedy drama Our Brand Is Crisis sits on the fence of both genres without really committing to either
Bullocks usual audience might be turned off by her characters dark cynicism but as meaty political satire this stands tall in a nearempty field
In the end though in spite of its hardhearted cynical veneer David Gordon Greens comedydrama turns into a strangely softcentred and manipulative affair
Bullock is hilarious as well as unsettling in the lead
The movie loses its cynical nerve with an utterly phoney finale that ties itself up in conscientious knots
The plot is dull and a few illjudged moments of comedy just seem weird
Bullock is great fun as the brash bossy empress of spin doctors and has a feisty chemistry with the snakelike Thornton
A protagonist who revels in the sheer cynicism of her job gets a sentimental redemption out of nowhere Sandra Bullocks comedic chops are undercut by it
Chaotic storytelling and uneven tone get in the way of an outcome that should really enrage an audience
The set up induces hopes that a screwball comedy with real buzz will be in store something along the lines of Preston Sturges hilarious  movie The Great McGinty Sadly these hopes are soon dashed
A regular dramedy that doesnt boast great performances or a great story Full review in Spanish
Bullock does her best to render the emotional turmoil authentic but the script doesnt do any favors by moving from laughoutloud to heartfelt soulsearch
Still there are important lessons about political manipulation and theres fun to be had watching Bullock and Billy Bob seething at one another
Promising to be a biting satire but succumbing to the juvenility it loathes this film feels like its still teething unable to chew on a political jawbreaker
This teen horror fantasy based on RL Stines bestselling childrens books is made with an anarchic glee that in its better moments rekindles memories of Joe Dantes zany tongueincheek movies of the s
Its great fun combining live action with topnotch computer animation and I enjoyed it enormously
Goosebumps is a wicked delight packed full of spooks and scares that should have adults jumping out of their seats almost as often as little ones
The film is pleasantly enjoyable throughout combining likeable characters with knowing gags and lively set pieces
While only providing sporadic entertainment for the overs itll keep the tween set amused to the bottom of their popcorn buckets
Mixing the action comedy and horror from novelist RL Steins books into a familyfriendly package this lively romp is entertaining enough to amuse the audience even when it veers off the rails
Goosebumps speeds up helterskelter in the second act and never loses its momentum providing an eyepopping feast Cool visuals aside its also full of intertextual content
At times as lumbering as the mantis director Rob Lettermans semifamilyflick has a few giggles but ultimately feels strained
A pacy quirky and thoroughly entertaining family adventure
Its a little scrappy but theres more than enough carnivalesque mayhem to keep monster fans young and old diverted
Goosebumps combines cynicism with sweetness If you can get past the slow start woefully blank hero Zach Dylan Minnette and hitandmiss special effects youll be charmed
Goosebumps has a clever mixture of monsters mayhem comedy and romance that will remind parents of childhood favourites such as The Goonies Gremlins or Jumanji
The picture doesnt have the satirical edge of a Gremlins but it combines a sharp funny script with some emotional substance
The film is set up as a great entertainment for children who do not avoid the early fears Full Review in Spanish
Jack Black gives one of his more enjoyable performances playing the writer as a Frankenstein figure  the creator of creatures that he cant control
Black is in good form as the enigmatic Stine but it is Minnette as Zach who impresses most
A goofily entertaining horror romp full of tweenfriendly chuckles and scares
Makes the Jumanji reboot superfluous
Goosebumps focuses on keeping the dialogue snappy the action plentiful and the CGI convincing as it quickly moves through scenario after scenario
The triumvirate of young actors are charming but inevitably its Mr Black who steals the show with his patented hilarious bellowing Will todays creepypasta kids still enjoy such silly fireside supernaturalism We do hope so
Its steadily paced with more longheld contemplative shots of natural beauty than bursts of impressive action and an elastic dreamlike sense of passing time
A breathtaking work of art which revolves around a haunting female lead
Its cinematic night nurse
Episodic scenes of violence ensue but fans of martial arts should bed in for something more slow spare and mysterious than the likes of Crouching Tiger Hidden Dragon
Its a wonderful thing to behold Its also a frustrating beast to absorb
A film of surpassingly exquisite visual beauty
The director pays exhaustive attention to sound editing and miseenscne
May just be the most tedious and enervating kungfu movie ever conceived
A film that gains greatly from second or third viewings
The Assassin unfortunately is more still life than cinema
Magical and utterly mesmerising
A measured elegantly choreographed tale with Shu Qi convincingly capturing the steely determination and conflicted heart of the lethal blackgarbed assassin
The Assassin targets cinematic pleasure and kills it stone dead
When you spend most of your time either admiring the look of the film or trying to work out what the bloody hell is going on theres precious little time left for enjoying it
Still waters often run deep but here you cant see past the glossy surface
I gather there is something to this film that I lack the patience to appreciate in its storytelling but I cannot for the life of me meditate on the occurrences like the film seems like it wants me to
As beguilingly elusive as it is exquisite
The Assassin is a singular vision realized with absolute mastery of style and a lightness of touch thats to die for
Its not an easy film to follow not because it is especially complicated or labyrinthine but because it is abstracted by Hou into a flow of gentle beautiful moments punctuated by bursts of violence
Intriguing at times because of a maliciously dosed ambiguity The Assassin is a really novel film even among HsiaoHsiens own body of work Full Review in Spanish
Ches mission to lift his father up  intimately demonstrates how health care education and supportive housing  help a motivated man gain confidence and his life
In My Fathers House doesnt spend much time on politics examining why rates of fatherlessness are so high among black American families but it doesnt need to
The movies message if any If youre a successful rap star you might want to think twice before returning to the ghetto to track down the deadbeat dad you never knew
The result is a film whose exploration of responsibility and addiction will interest viewers whove never heard of Smith
In My Fathers House could not have come at a better time
Has vulnerability and a few laughs But a story about reconnecting shouldnt seem so uninterested in filling in the gaps
In My Fathers House offers lots of interesting raw material but it could use a disinterested observers remix
Though it brings limited insight to the problem of absent fathers in the black community In My Fathers House tells a story of loneliness abandonment anger and joy that all can relate to
Whats powerful and interesting about In My Fathers House is the way so much goodwill and urging are tested in Ches personal life
Watching Mr Tillman as he cheerfully embraces sobriety to please his son and is rebuffed by his former street pals for doing so we see a poignant meditation on how our expectations of loved ones can become a burden that not everyone can carry
Its neither as hopeful nor heartbreaking as it might first appear
A brave personal introspective exploration of a phenomenon that has no easy solution
Thomas and Rose ricochet from one boneheaded depressing escapade to another and by the end you may start to feel as eager as the mobsters theyre tormenting to get rid already of these babbos
Few films are better titled than The Wannabe a portrait of a Bronx kid who would have done anything to be part of the mob world he knew from the movies
Arquette shines but this unmademan mob movie lacks the energy and brutally dark humor of Rob the Mob which was about exactly the same story
The film is less a revisionist take on the circumstances of John Gottis  indictment than a tedious love child of Bonnie and Clyde and Goodfellas
The mix of callous humor and romantic doom doesnt always hold up but in its best moments The Wannabe finds real spikiness in the pitfalls of antihero worship
Mr Piazza offers a persuasive portrait of decline but it is the crumbling beauty and flailing hopes of Rose that resonate Ms Arquette comprehends the character inside and out and her aim is true
Its taste in antiheroes doesnt inspire fascination just a desire to see a different movie
The Wannabe is too derivative for any insight offering little more than tired tropes and bad accents
Theres frippery and flummery a la Almodvar and Hitchcock two of Ozons favorite directors and he also cites Wilders understanding of drag And not to forget Ozons love for Fassbinder the late rapidfire genregobbler
Its fun to watch The New Girlfriend the way its fun to drink a glass of Champagne and about as memorable
An oddly sweet film gentle and genuine but its also aware of murky psychological spaces pushing focus on clearing confusion not sensationalizing the obvious
The New Girlfriend is a funny delicate subtly flavored dish deliciously prepared by Franois Ozon  idiosyncratic and unforgettable
Duris in drag looks like a refugee from the New York Dolls but his character aspires to nothing more than life as a bourgeois pearlclad mom
Its less than it shouldve been and a little mild But Ozon makes it glide with confidence in or out of heels
Duris creates a sensitive and moving character a confused soul lost without his wife who was the anchor of his life
What is surprising is how conventionally the screenplay begins to wrap up this story and how blatantly manipulative its methods are in order to do so
Ozon is on typically slippery teasing form with this sly and playful tale of female friendship with a twist
A swirl of gender confusion The New Girlfriend tosses all the heshewe question marks it can find in a tale of contemporary confused sexuality
Its never clear what the directors intentions are the premise looses strenght and it falls apart thowards the end Full review in Spanish
The acting is remarkable in a movie that does not pretend to be something else than what it truly is approaching the subject of confusion between genders with originality Full Review in Spanish
The movie is a revision of both the limitations of the upper classes and their completely outdated morals Full Review in Spanish
Simply magnificent Full review in Spanish
A solid script full of surprises and great performances Full review in Spanish
A strong twisted but perfectly written directed and acted film Full review in Spanish
While many gay and identity films wallow in weightiness Ozon has always rejected categorizing cinema in the same way that he has embraced sexual fluidity
Arguably reminiscent of camp Ozon entries like Angel and  Women this has good enough work from Duris but is truly saved by the lovely and lively Demoustier
an intelligent investigation of hearts that dont know quite what they want
A strong thematic undercurrent pits bourgeois social conventions against authentic selfdefinition and ultimate freedom to live without shame or undue social limitations
Director John Wells deftly navigates the trajectory of the story helped by Rob Simonsens frothy score
This drama about a worldrenowned chef seeking redemption in his personal life features a fiery performance by Bradley Cooper compromised by a predictable script that doesnt have the right mix of ingredients
Burnt should have been a spicy treat Instead it comes and goes like so much fast food leaving you hungry for a latenight DVD snack
It is hard to find the characters preoccupations anything other than ridiculous however divine the food looks
Anyone who makes an informed decision to watch Burnt might want to bring a bingocard of dramatic cliches to tick off during the runtime
Its intended to be a tale of redemption but Adam doesnt have a lot of redeeming qualities
This halfbaked dramedy about cooking feels more like a lackluster television pilot rather than a featurelength film
Cooper delivers the goods right enough with help from Daniel Bruhl as his business partner and Sienna Miller as his souschefcumloveinterest
It makes for a pretty good show Cooper cooks up sweet and juicy in just about every role he plays so watching the him sizzle on the griddle of shortorder Shakespeare set against the backdrop of Europes fine cuisine is a ready pleasure
a missed opportunity that ultimately feels like two movies ungainly stitched together at the middle ie its both an engrossing cooking drama and alltooslick character study
Burnt is plated beautifully but lacks the complexity of flavor it so desperately wants to achieve
Strong characters help hold the attention as this overcooked drama develops but in the end it feels so concocted that its difficult to believe
This story of how a troubled exile achieves healing through a creative partnership with a woman equally devoted to their shared craft is familiar ground    Burnt could be retitled Silver Linings Cookbook
Built around a strong performance by Cooper Burnt is a story of redemption
Coopers performance helps lift the character but as is often the case with a mediocre script and story its not clear what exactly this movie wants to be Youll enjoy the greatlooking movie star The comeback story Not so much
Halfbaked tepid underseasoned  all these culinary putdowns are deserved for a film that asks us to root for a broken man with nothing to lose then hands him redemption on a silver platter
A light entertaining and well told film you can enjoy without any complications especially for those who enjoy great food Full review in Spanish
What is most noticeable in this film is that in the  minutes there is nothing that appeals to the viewer Full review in Spanish
Bradley Cooper is a chef looking for redemption in this little successful dramedy Full Review in Spanish
Despite the amount of delectably photographed dishes the drama in Burnt unfortunately feels undercooked and watered down
Long after youve seen the film youll remember the wonderfully nuanced work of the cast particularly Ms Hawkins
Parents and teachers of all the Nathans out there may be inclined to give A Brilliant Young Mind the full four stars for the rest of us its small smart and satisfying
A Brilliant Young Mind is not only warmhearted but offers surprises in a symmetrically crafted script and a handy rebuke to the compounded hearttugs of another recent tale of genius in the world The Theory of Everything
A poignant story with an accomplished cast
A beautiful movie wrapped around a moving story  one that should not be missed
Though billed as a boygenius story A Brilliant Young Mind is really a tougher thing a movie about the complex emotional lives of autistic children
stock characters are part of the toolkit for writing melodramas but they can be more or less fully realized and in this case Matthews and writer James Graham have gone for less
The path may be predictable but the priorities of the screenplay by James Graham are unexpected
Yes this movie is as sugary sweet as it sounds but director Morgan Matthews uses his documentarian eye and does a good job of showing us the world through the eyes of a remarkable young man
Bungled ending but a great stylish brilliantly cast film about math prodigy
well acted but tugs too eagerly at the heartstrings and relies too heavily on manipulative melodrama that tends to trivialize its protagonists plight
It is a fully realized love story of rare spirit the sort of deeply affecting film that can make you laugh until you are near tears then drive you close to bawling
Im sort of amazed that movies like this keep getting made which makes me the stupid one
A Brilliant Young Mind is an inoffensive film looking to please a broad audience and while it achieves its goals theres always a sense that it could have done more
Hardly breaks new ground but its a watchable entry in the tortured young genius genre
It does for Aspergers syndrome what A Beautiful Mind did for schizophrenia lots of drama but not much science
Witnessing Nathans special powers as his dad called them may give the film its spectacle but its soul is in the relationships Nathan struggles to build
Given that the film is concerned with the rigid certainties of algebraic formulation its winning formula  mixing charm lowkey humour onscreen chemistry and emotional delicacy  is altogether more ineffable
Matthews direction is perfect Yes he manipulates you throughout but nothing is milked there is great subtlety and his settings in Taiwan and at Cambridge are deftly shot by Les Miserables and The Kings Speech cinematographer Danny Cohen
A confidently directed version of an underdog story that weve seen before
ChiRaq isnt nearly as polished or cohesive as his earlyperiod worksbut its delivered with a sense of fervor and immediacy that hasnt been felt from Lee in years
Its messy in places as Lees movies tend to be But there isnt a moment that ChiRaq isnt alive This is a deeply serious biting picture that also has joy in its heart
If theres a hotbutton modern American issue to be pressed Lee jabs it The common thread is vitality energy and urgency
Lee who birthed such memorable films asShes Gotta Have It and School Daze is back at the top of his form withChiRaq Lets hope he stays there
Submitting his finest work since the s Lee is inspired and alert for a change displaying renewed interest in the world around him The mischief and outrage presented here is outstanding
Savage optimism
In short its a Spike Lee film and one of his best in a long time  earnest flawed idiosyncratic and unforgettable
When Lee cooks up a stew this heady one best recognizethe right film at the right time Lees most creatively fertile and socially immediate narrative feature in years
Youve got the superbly selfpossessed Teyonah Parris as Lysistrata a stern and witty Angela Bassett  and Nick Cannon looking very comfortable in the role of the rap artist and gang leader known as ChiRaq They are served both chilled and hot
This is a Spike Lee film through and through It features jarring shifts in tone and temperament It stridently proclaims its message throughout at times like a revival meeting seeking a callback at others a history lesson in verse
A mesmerising indelible and important piece of contemporary cinema
Deliriously satirical bouncing between comedy romance and tragedy yet it primarily serves as a wake up call for the disenfranchised people suffering in this country
Theres so much swagger in ChiRaq that its a little uneven this movie is both small and immense the same way that violence within a community can feel isolated to an area but is also reveals a systemic worldwide problem
There is no story beyond this simplest standoff set up no character development no sign of any other life
There are some good acting performances by Nick Cannon who also sings a couple of songs Jennifer Hudson who also sings a song on the soundtrack and Teyonah Parris The soundtrack of this film should have gotten an Academy Award nomination
This is a movie that manages to be both brash and earnest hilarious and deadly serious bluntly rhetorical and poetic at the same time
Never subtle always strident and absolutely necessary
Lees film is worth seeing for its bombastic excess and camoclad dance scenes But if youre looking for tactful visual responses to the Black Lives Matter movement and the effects of police brutality this isnt it
Lee returns to engaging enraged form with ChiRaq combining social commentary anger humour dramatics and overthetop style in a sometimes messy mix that uses every trick necessary to put a spotlight on Americas poisonous love affair with guns
Ultimately this is neither tragedy nor farce Anybody know the Greek word for boredom
Its real honest funny and downright predictable but the Patel family is so engaging and delightful that you wish theyd adopt you
Meet the Patels ends up being much more than a movie about a guy going out on dates Its about shifting identities parental expectations and trying to hold on to a life raft of tradition in a swirling sea of change And its pretty funny too
Their attempt to stitch home movies goofily animated family conversations and wry crosscultural observations into a fullfledged movie has a few amusing moments but more often falls flat
Meet the Patels isnt an event but it makes for an engrossing story with a specific cultural perspective
We enjoy Ravis search for suitable mates and we like his family In the end they just want each other to be happy  and thats all that matters
Engaging but overlong and more than a mite unsettling
Ultimately a touching funny documentary about family and cultural forces putting pressure on a firstgeneration IndianAmerican man to do what should come naturally find love and a life partner
This story of a guy looking for love in many of the wrong places turns out to be one of the happiest surprises of the movie year
Its a film that pokes goodnatured fun while maintaining a real respect both for traditions that have survived for generations and for the circumstances when its healthier to set some of those traditions aside
Meet the Patels is a funny lightweight look at the weight of cultural traditions
Ravis journey for an American happy ending is eclipsed by the fascinating intricacies of Indian matchmaking
A charming if completely obvious documentary
Its nothing new under the sun but this is a charmingly unassuming and often very funny little movie
A gently humourous doc
Theres a touch of vanity project about the enterprise but Ravi makes an entertaining narrator imaginative use is made of animation and the film sheds light on timeless themes like love family and honesty
The documentary works as well as it does because Ravi while exhibiting a wry dry sense of humour is also something of an introvert Hes not always looking for a punchline so the ones he makes have more punch
Rather than taking a firm position for or against arranged marriages Meet the Patels is a bighearted poignant and truly funny documentary that shows people will try anything to find love  and sometimes anything works
This highly personal and lighthearted documentary gives some social context but never dives deep on the potentially devastating cultural divide it is exploring
One of the funniest romcoms of the year
Patels parents are the real personalities of the movie so goodhumored and spirited and so wonderful in sticking to their oldfashioned view of family life
Theres no reason for Rabid Dogs to exist as even character identity and motivation receives little attention
This remake of Mario Bavas  thriller is a serviceable timekiller that benefits from star Lambert Wilsons slowburn performance
If you can endure the messy slaughter with a body count in double digits the plot is not without its rewards
French filmmaker Eric Hannezos new Rabid Dogs never betters its predecessor but its a smoother ride
What this French redo of Mario Bavas  heist thriller lacks in plot it more than makes up for in flashy camerawork energy  Hitchcock would have cued us to the surprise twist earlier
Straight Outta Compton is not a movie that will change your life but its a very entertaining and strong approach to NWA Full review in Spanish
It has equal parts biopic movie and a lively representation of a cultural phenomenon thats still relevant today Full review in Spanish
An initially valiant attempt to do justice to a sprawling complex story that eventually succumbs to the pitfalls of a conventional music biopic
It follows some of the tropes of the biopic genre however what sets it appart is the human and raw emotion in the performances of the main cast Full review in Spanish
Despite some forced sentimentality it does its job as a biopic and that is the way it chooses to be but it could have offered more Full Review in Spanish
While the story might seem convoluted to rap neophytes Straight Outta Compton remains compelling due to its brilliant cast
the movie would have benefited from a much much shorter runtime ie this is not material that needs or deserves such an epic length
When it lets its music talk its rage has street cred
Straight Outta Compton essentially picks one narrative and sticks to it but the story it chooses to tell is undeniably powerful
One of the better musical biopics of the last  years Straight Outta Compton is a real ragstoriches showbiz story
As with most musical biopics its the rise of the artist that is the most entertaining
The first hour flies by in a haze of frenetic energy and exciting performances from all the main players
The movie has inescapable verve capturing why its subjects mean every bit as much as The Sex Pistols or Rolling Stones
Its all pretty entertaining and a testament to how much they accomplished in such a short space of time
Its just all a little too much for a twoandahalf hour movie
The opening act has a level of insight and exciting energy that the rest of the movie bogged down in plotting comes nowhere near replicating
A short history of rap
Stylish yet all posturing with no humanity an exercise in brand control by those who have no interest in exposing their faults
It portrays incredibly well the elements that made the creation of NWA inevitable the social political and economical conditions in which its founders were brought up Full review in Portuguese
F Gary Gray tells the story efficiently and leaving no loose ends on both script and plot Full Review in Spanish
What the film lacks in logic and scientific plausibility it makes up for in the philosophical questions it raises
The ideas McClean puts on screen are creative and original And his technical skill is outstanding He shows a lot of promise in other words and the films worth checking out
This is the first feature from writerdirector Joe McClean and he has made a debut that is admittedly uneven but not without promise
The turmoil in the friendship of the trio grounds the film in humanity when things get too theoretical
Joe McLean makes a memorable directorial debut with this mindblowing scifi reminiscent of such inscrutable screen classics as Memento and The Matrix
An inspiring film that shows the power of faith and forgiveness on and off the field
The Erwins latest film leans heavily on a genre formula but it is better than most Christian films
Enough of the flick worked for me to give it a slight recommendation Full Content Review  Violence etc  for Parents also available
Sure its an evangelical Remember the Titans but at least the Erwin brothers have made an effort parochially speaking to go outside the lines
Entertaining Inspiring Gripping True Story
Until the balance tips rather too blatantly toward the latter during the final minutes the overall narrative mix of history lesson gridiron action and spiritual uplift is effectively and satisfyingly sustained
Sports and religion are a potent combination one that siblings Jon and Andrew Erwin October Baby Moms Night Out exploit to canny effect in their new film based on the reallife Woodlawn High School football team
Remember the Titans  Facing the Giants  Rudy  Woodlawn
Its hard to buy that this brand of Christianity is fighting for the rights of the minority while so clearly throwing their own weight around as the majority religion
Heartwarming factbased drama about faith race football
Woodlawn hits spiritual pay dirt
The unintentional message here What integrated Alabama wasnt Christian brotherhood but Alabamas REAL state religion  football
A film taken with the singular American delusion that Jesus loves football though it also throws in a new delusion Jesus hates the US Constitution
Although handsomely crafted wellacted and made with transparently noble intentions Roland Emmerichs Stonewall is a movie that seems destined to please almost no one
Isnt a perfect movie by any means but the good intentions are there
Lends all new meaning to the phrase Roland Emmerich disaster movie
The Stonewall Riots deserved a better movie and unfortunately Emmerich was not the guy who was going to deliver it
Its a selffinanced passion project from a man who might be the most financially successful out gay filmmaker ever We should be celebrating this but man oh man does he make it difficult
Irvine is with that tousled blonde hair and milky skin with its apple cheeks a beautiful ephebe with no more emotional resonance than if he were actually carved from a pillar of marble
This cant be the kind of equality that those Stonewall activists were fighting for the right to have their story turned into formulaic historical fiction as tedious as the kind about straight people
If someone set out to make an afterschool special about street hustlers this would be it
The worst Quantum Leap ever
more than bad and worse than disappointing a tragic distortion of a vitally important story that insults the people it tries to honor and insults its audience as well
Where the social impact of the riots is lasting the impact of the movie is negligible
Almost everything that could go wrong does
Its a high school playready version of the riots a version more focused on outside characters and related inspiring lessons than even the riots themselves and wears the rosestcolored glasses about the dynamics of the era in which it took place
spends an excruciatingly long time on a happily ever after coda  minutes about Danny and not enough of what followed after the riots
The Stonewall Riots were a triumph for a marginalized community but Emmerich fails to convey the significance of the event in any meaningful fashion The subject matter deserves better and so do we
Stonewall is such a cataclysmic disaster of a film that Im surprised nobody has called FEMA yet to help with all the damage its done to the GLBTQ community
Despite the volatile subject matter Stonewall is a rather bland rendering
Disappointing gayrights drama has sex language violence
Stonewall is a movie about a pivotal moment in LGBT history as filtered through the perspective of a fictional hunk of Wonder Bread named Danny who steps off a bus from Indiana and right into a central role in the Christopher Street scene
Compared to a genuinely rousing gayrights historical drama like Milk  Stonewall falls considerably short
While its hard to shed too many tears over a corporation taking its customers for granted what Hanks movie does so well is appreciate the people who built Tower and in doing so transmit that in this case they did care
But Hanks wisely limits the celebrity talking heads in this riseandfall story Instead he focuses on the people who built the company from a Sacramento drugstore annex to a global brand creating a ragtag family in the process
Hanks does a strong job with his documentary debut here giving All Things Must Pass a lot of energy and an endearing quality It helps that the interview subjects are so engaging and likable
The film never lapses into hagiography partly because Solomon is too authentic a presence
A warm wistful documentary that fondly recalls a time when buying music was a communal activity and not just clicking on things
Overall this is an assured effort informative bittersweet and appealing for both the young and the not so young
Hanks and Leckart tap into a latent nostalgia for Towers freeandeasy heyday and the movie is an intensely bittersweet experience for anyone who grew up with Tower Records as a home away from home
In Colin Hanks admiring and tragic corporate biography Tower Records wasnt just a rock n roll mecca but a family operation that got high on its own supply
Solomons skills as a raconteur the employees unabashed love for their work and the constant stream of rock music playing in the background advance the film into something much more than a talkingheads documentary
The real beating heart of the film is its collection of wild war tales told by the companys former employees who regarded Tower as more than just a paycheck gig or a commercial proposition
Lively and loving
Colin Hanks makes his feature directing debut with this irresistible documentary about the evolution of the music business
Its loving and lovely but goes too easy on the hubris and greed
Hanks makes the rookie mistake of covering the same points too thoroughly  the film could be  to  minutes shorter  but you can see why he lets entertaining interviewees ramble a bit
This is Towers story and Hanks tells in a way that will resonate with both grizzled music veterans who have hung onto their physical collections and millennials wondering what all the fuss was about
As Bruce Springsteen says in the film Everybody in a record store is a little bit of your friend for  minutes or so And hes right  including all the ups and downs that friendship entails
Hanks found an amiable raconteur in Solomon now  but sharp and focused on the business that was his life A collection of Solomons confidants sing his praises and get misty about how much fun they had in the old days
We learn of the partyhearty environment and familylike vibe of a world where it was cool to write off cocaine as a business expense And we see the hubris and myopia that doomed the industry
Director Colin Hanks lets his affection for his subject run over The film probably is for record aficionados only
A love letter to the store and Solomon  but also to the bygone era of music consumption before iPods and Spotify
If only acting was as easy as hitting his patented RKO
A belated barelyrelated sequel generic enough to make the eminently forgettable  original look like an oasis of cinematic personality
The story fails to embrace the humor in the premise resulting in a sappy melodrama that tries too hard to bring you to tears Full review in Spanish
A light comedy that offers great entertainment and also touches on womens rights and the elderly at the workplace Full review in Spanish
The chemistry between Hathaway and De Niro feels sincere and is what keeps the story up and going Full review in Spanish
Thanks largely to performances by De Niro and Hathaway The Intern is a gentle enjoyable fantasyand certainly Meyerss best film in more than a decade
It is strange seeing De Niro playing a nice normal old dude and not insane edgy enraged andor emotionally constipated and you also cant help but think that Meyers movie would have worked better if he had shot someone
Compared to the dreck now synonymous with DeNiros name this soso comedy a pleasant but forgettable fish out of water story is actually one of his best films in years
Its a work of unwavering optimism with an uncompromised procareerwoman message and I found it refreshing
A typically breezy and entertaining Nancy Meyers comedy
Theres a lot less going on in The Intern  the story of an old guy who goes to work for one of those fancy new ecommerce places whatever that is  than meets the eye or the funny bone
DeNiro is his usual charming best and Hathaway takes what could have been a onedimensional character and breathes life into her
Its a strange one The Intern Just as youre wincing at a tired gag about how older people dont know Facebook along comes sentimental melodrama and youre suddenly grabbing for a tissue
Its the job of movies to fulfill wishes and the desire for an understanding parental figure is as basic as the desire to feel needed and useful in ones old age
De Niro and Hathaways very real chemistry is enjoyable and transcends The Interns many shortcomings
Fauxfeminist and whitewashily liberal A movie that wants us to feel bad for a female executivewhos actually highpowerprivilegedonly for a great white fathersubstitute to be her perpetual personal cheerer is far from feminist and damn near stupid
The Intern is a smart funny and surprisingly touching film anchored by firstrate performances from DeNiro and Hathaway
Some films have grace notes this one has what might be more appropriately termed graceless notes
Hathaway plays much better against DeNiros pinstriped persona when shes biting into her role with a Miranda Priestly boorishness
Its sad too because De Niro brings an amiable twinkling charm to the role Instead were stuck with misogyny dramedy and snivelling
The Intern doesnt have a huge amount to say about the issues it touches on its generally happy to let its two charismatic leads simply hang out together Its a li ttle cutesy at times and overlong but its not without its charms
Those of us who were never cool young guys live with an enduring hope that someday well be cool old guys Its a fantasy embodied in the flesh  specifically in the gloriously creased proudly aging flesh of yearold Robert De Niro
A predictable drawnout and cheesy story about remaining true to yourself in the face of pressures to sell out
The music is peppy enough but the spoken dialogue feels like it was written with emoticons
As silly and sometimes nonsensical as it is the movie is surprisingly sweet and wellintentioned
In the end this awful movie is an adaptation of the cartoon about as much as Steven Spielbergs Jaws was an adaptation of Emily Brontes Wuthering Heights
Jem has less in common with its neondrenched s source material than with the reallife Internettoredcarpet trajectory of Justin Bieber  a similarly generic teen idol with moves dully modeled on superior artistic predecessors
Though there are some light scifi elements the story remains largely grounded and based in the real world focusing on the characters rather than the spectacle
Everything about Jem and the Holograms seems specifically designed to be annoying The film is deeply misguided and not half as clever as it thinks it is
How can a dressup party with this much glitter makeup and hair dye preach the importance of being the real you
Jon M Chus weaksauce adaptation of the s Hasbro toy turned cartoon not only fails resoundingly as a film it also fails as a nostalgia piece  which honestly might be the greater sin in todays pop cultureverse
Sure Peeples has a nice if unmemorable voice but the vapid storyline with fantastic overtones transports Jem and the Holograms into another dimension one thats utterly flat
The movie is trite cheap and shoddy designed to be watched on an iPhone instead of in a theaterplus theres no way this assortment of bland nicegirls would ever become superstars Not even on YouTube
a shallow glittery girlygirl fantasy that unless you have a high tolerance of cute and the ability to laugh at how stupid this whole thing is may make you put your head through a wall
For a movie about someone learning to come out of their shell Jem and the Holograms is utterly confounding about what it even means to be authentic
Its hard to imagine a movie this year more sadistically boring and bland
Utterly implausible on every level and ultimately rather insulting a bit of glitter and lots of hugs are the sum total of its girl power
Chu was responsible for the shambles that was GI Joe Retaliation and proves that was no fluke through his uncertain handling of the conflicting material
Every line every twist and every note of music feels painstakingly focusgrouped
A highly flawed occasionally dumb but fascinating blast of neon revisionist girl power and social media wanderlust
This live action dud is basically what you imagine a bargainbasement Star is Born would be like if it was directed by the chap who did a Step Up sequel a horror producer and Justin Biebers manager
An unrealistic representation of the music industry
Gainsbourg and Sy play off each other wonderfully emphasizing how these characters relate to each other as people their scenes together feel emotionally honest even though one can barely imagine them happening in real life
Deserves credit for raising important issues at all but the trite romcom packaging compromises the good intentions
Samba manages to be hugely entertaining featuring a superb cast and expert direction without abandoning its important message about a universal problem
Though the film doesnt provide any answers it does give voice to the millions who suffer these same situations daily
Samba tries to be too many things to too many people although you cant say it doesnt have heart
Going for frivolity the endeavor abandons authenticity adding more confusion and disorder to an already scrambled film
The filmmakers mix touching social realism feelgood romantic comedy and workingclass farce into a patronizing ragout of flavors that never successfully blend together
Unfortunately the material flounders from the broadly farcical to the bombastically melodramatic
So affecting is the performance by Omar Sy Jurassic World The Intouchables as Samba like the dance an undocumented Senegalese chef you almost forgive the shortcomings
becomes caught between a breezy romantic comedy and a provocative drama about social justice
A comedy that was sold to appeal to a sensitive and inteligent side but that never happens Full review in Spanish
A refreshing romantic comedy that shares a lot with the directors previous film Full review in Spanish
Although the performances are memorable the plot does not know how to combine drama and humor Full review in Spanish
A small comedy that bases its success on its leading couple Omar Sy and the always gracefull Charlotte Gainsbourg Full review in Spanish
A highly enjoyable movie Full review in Spanish
Olivier Nakache and Eric Toledano return with one of The Untouchables stars Omar Sy in the title role Its a darker sadder and less audiencepleasingly contrived tale than their previous effort
Samba finds a much stronger rhythm when it stops contriving and simply shines a light on the joy and pain and musical interludes of lives lived in the margins
The editing errs on the side of longueurs but likable people and the miseenscne draw you in Somehow even the artificiality feels heartwarming
Nakache and Toledano have another crowdpleaser with international appeal on their hands
Samba is still an entertaining and enjoyable movie due in large part to the charm of its leads Omar Sy and Charlotte Gainsbourg
An adventure film of big scale that focuses on human drama Full review in Spanish
Unfortunately Everests technical feats arent matched by a subpar script from William Nicholson and Simon Beaufoy
The film only seems to come to life when showing how challenging is the climb and how unpredictable and harsh mother nature can be
For a movie about the worlds biggest mountain Everest feels small
The script emphasises the dangers but fails to underscore that with a truly treacherous aesthetic environment
Everest has a couple of good moments however nothing that moves you the way its advertised Full review in Spanish
Everest splits the difference between documentary reenactment and hypedup Hollywood drama
Worth seeing for its terrifying action sequences and its stunning visuals but it never delivers the expected emotional gut punch
Kormkurs film excells because it never romanticizes the mountain and he doesnt drag the last moments of those who died in it Full review in Spanish
Its a solid disaster film that certainly isnt the feelgood movie of the year
Thrilling exhausting and ultimately devastating
Everest remakes the experience as entertainment invites you to observe but not feel the suffering and invites you to pay for the privilege
Spectacle filmmaking with a purpose Everest is a welcome throwback to the testoroneheavy adventure flicks of past decades
With visually stunning imagery and a solid Alist cast this film just about transcends its oddly uninvolving story
The picture utilizes every bit of the expanded D frame to provide an immersive experience But thats combined with a real sense of brutality
The movies first hour is a bit slow precisely because the filmmakers refuse to inject conventional dramatic elements But it all pays off in the devastating finale
The stunning location photography stays with you much longer than the scripts shortcomings The mountain as ever has the last word
This is an impressive filmmaking achievement but those expecting to be moved by the story may well be left cold
Everest the movie in showing how the mountain conquers man never explains the torturous treks appeal and that monstrous Himalayan peak ends up dwarfing the human stories here struggling for air
Over the course of a fragmented  minutes we find ourselves lost in just as much of a snowy fog as those of the actors who wander in and out of the frame as if uncertain as to whether the story even requires them
Inevitably and immeasurably affecting but conventional in terms of what it says
The footage  discoveries made by the Allies in the liberated Nazi camps during   is graphic terrible unforgettable
This wise sober new documentary is a reminder of historical horrors but its also a tale of censorship
Though undoubtedly an underwhelming documentary this remains an important piece of cinema as it documents a barbaric brutality we have a duty to explore and study
Night Will Fall isnt simply a film about the war it documents the power of emerging technologies to reveal and publicise war crimes  something that also feels acutely relevant today
We wouldnt watch it again but were glad we watched it once
As startling and bleakly compelling as youd expect from this rare combination of director and subject
This is an extraordinary record But be warned Once seen these images cannot be unseen
This is a shocking and moving account of how the Bernstein documentary was shot edited and shelved
The survivors and liberators interviewed by Singer have never forgotten their experiences and this crucial account of a dark episode in history stands as a chillingly eloquent memorial
It is difficult to imagine that therell be a more important film this year Dont miss it
Impressively sober thoughtful
After  years in the can some of the most visceral detailed and shocking footage of the liberation of Nazi concentration camps is being released
A masterfully constructed eyeopening and at times eyesaverting look at an obscure chapter of Holocaust history
As well as footage of Bernstein Singer has some fascinating interviews with English Soviet and American soldiers who did the filming
Horrifying Fascinating
As Night Will Fall shows even in the darkest hour sometimes the greatest heroes are those willing to stare bravely into humanitys worst depths and tell the world what happened
The liberation footage itself is the centerpiece of Night Will Fall It is mesmerizing sickening disturbing and essential
It may not do full justice to all of its subjects in its tight  minutes but its not a film youre likely to forget
Night Will Fall is an unsparing yet unfortunately necessary reminder of the atrocities committed seventy plus years ago
Hellers grasp of the material is as firm as a more experienced directors Still the primary attraction is Powley who offers up the best acting performance Ive seen all year
Boldly honest and frank comingofage film that is at once daring in its subject matter yet very much part of its genre Good but not quite great
Frankness candor and honesty served hot A mustsee
Playing a character trying to reconcile her hormones heart and head Powley seems genuine in a way that actresses playing adolescents rarely do and helps ensure that the heady rush of a movie that shes in does the same
This powerfully arresting and affecting adaptation of the book by Phoebe Gloeckner is only getting started with the many and varied provocations it has in store
Powley offers a bold performance that mixes rabid hormones spiritual agonising scary selfdebasement and extreme teenage dramaqueening
Most crucially Heller is more interested in bearing witness to adolescent experience than passing judgment  and this approach extends to her handling of the adults around Minnie
Powley is a yearold actress but shes a convincing teen in every sense which will lead to the discomfort of some Its a breakout performance
Whether adulthood is measured by age or experience or by something less quantifiable is one of the key questions the movie asks
Prepare for the most fked up love triangle ever
From her breathless confessions of true love to her sweaty accounts of coitus Heller Powley and Gloeckner pull us onto the mental merrygoround of the female teen
An irregular coming of age tale that having a story thats grounded on reality flirts with improbable situations taken out of a very used mold Full review in Spanish
The film is quietly radical not because it dares to rattle cages so much but because it doesnt This is a story of huge emotions and big moments told via intimate gestures and tiny power shifts Its a gem
The Diary of a Teenage Girl is honest frank and doesnt hold back So if you dont mind a swear word or two and you want a realistic approach from a film this movie could be for you
Bel Powley Alexander Skarsgrd as the boyfriend and Kristen Wiig as the mum all display the subtlety not in the script
Multiple stars are born in The Diary of a Teenage Girl the conventionally titled film premiered earlier this year at Sundance that turns out to be unconventional in every way that matters
Writerdirector Marielle Heller never passes judgement on her characters giving us an honestly messy account of her heroines sexual and artistic awakening
The Diary of a Teenage Girl is a rare gem because it strips the male gaze and sees Minnies sexuality as her initial link to a deeper self discovery
The Diary of a Teenage Girl is surely destined to become a tentpole comingofage movie and I imagine a significant vehicle for the careers of everyone involved
It abuses the unconvincing drama and dramatic twists the director submits to the young protagonist Full Review in Spanish
Striking the right goodnatured tone between admiration and amusement I Am Thor isnt particularly slickly or imaginatively packaged but its straightforward DIY presentation feels apt
Jon Mikl Thor is a showman and a stalwart a flatout legend and I AM THOR is the often uproarious and sometimes quite poignant love letter he deserves
A charming subject goes a long way in characterstudy doc I Am Thor an imperfect but energizing portrait of selfstyled metal god Jon Mikl Thor
Highly entertainingThankfully Thors charisma is undeniable and its fun to watch him interact with fans and dominate the stage
I Am Thor is an amusing show business tale that is all the more endearing for the respect it shows towards the eccentric and indomitable figure at its center
Eden is proof that you need more than a good beat and stylized visuals to be memorable Full review in Spanish
An honest portrait of the hardships that come from chasing ones dream Full review in Spanish
A failed attempt by director HansenLove to create a portrait of a young DJ Full review in Spanish
Even with the nostalgia factor behing it the movies falls short of what it couldve been Full review in Spanish
A fair portrait of the French music scene of the s that captures the escence of the music that began to feed from recicled nostalgia Full review in Spanish
The cruel chronic of an aspiring DJ that never quite gets there Full review in Spanish
If not for Daft Punk this would be another generic movie about a musician that grasps glory only to loose it all at the end Full review in Spanish
HansenLove focuses on creating an atmosphere with rhythm and rich charachters to tell her story Full review in Spanish
Edens best quality is its bittersweetness a story about postponing adulthood and an honest portrait of fun confusing and good times as a teen Full review in Spanish
A swan song to the life of a teenager living free of consequence Full review in Spanish
Eden perfectly captures the spirit of the s and boasts a great soundtrack Full review in Spanish
A complex and elegant film Full review in Spanish
A tribute and acknowledgement of the intelligence behind the DJs craft Full review in Spanish
Like going to a club sober
Paul describes a song he likes as existing between euphoria and melancholia which is the balance the movie hes in strikes as well as interested in joy as it is in loss Which may be the best thing these stories of not making it bring to the table
Where the early scenes of Eden feel on the nose the back half is filled with precisely the sorts of glancingyetwounding blows that are HansenLoves specialty
The drama occurs only fleetingly and Greta Gerwigs cameo as one of Pauls flames is stilted
Eden does however offer an intriguing insiders view of an important musical subculture an insiders view that crucially remains rooted in the alltoorelatable reality from which the music itself is designed to offer temporary escape
Only in retrospect could I understand Edens careful delineation of temporal emotional and geographic properties
An impressive and suprisingly melancholy look at repetition in life soundtracked by the recursive music of the French Touch scene
Learning to Drive is a story of companionship loneliness resilience Its a small artfully crafted thing but it resonates in big ways
Charming and often very funny
Learning to Drive is precisely the sort of adultthemed intelligent and heartfelt film it wants to be with Clarkson and Kingsley wonderfully on point
Despite Patricia Clarksons performance and screenwriter Sarah Kernochans expansion of Katha Pollitts  New Yorker essay the film still feels undercooked
Kingsley acquits himself in the role with considerable grace He also makes a solid anchor to Clarkson who relinquishes her own elegant sometimes frosty screen persona for something a little wilder and decidedly sexier
Small in scale and largely unassuming both of which may be in the movies favor since it never tries too hard The film seems content to just be
Empathetic drama treats mature themes with warmth
A perfectly pleasant minute diversion with little conflict and no major drama You could watch these two act all day
Patricia Clarkson and Ben Kingsley are such likeable performers that theyre watchable in the worst tripe but its hard to imagine a weaker vehicle for their considerable talents
A light duty and passably entertaining exposition by Ben Kingsley and Patricia Clarkson
Its an engaging diversion but falls short of anything more
Driving a car is a useful cinematic metaphor for taking control of ones life and there is a gentle determination behind this film which makes it more engaging than it sounds
Directed by Isabel Coixet and based on a magazine article by the feminist writer Katha Pollitt this is a little film but there is a lot of craft in it To make a movie about human connection that is funny without being mawkish is no small achievement
Visually Learning to Drive is mostly flat  but then there are only so many ways you can shoot two people sitting next to each other in a car
Clarkson and a compelling Kingsley make for very easy company as their gentle exchange of age and culture enables them to embrace a challenging future
There are glimpses of intrigue but we never get to know the characters well enough to care for them
Director Isabel Coixets films have of late been a dark and sometimes dreary bunch so its a relief that her latest Learning To Drive is wryer sweeter and far less pretentious
there is a sense of wornout familiarity that attaches to this wellintended but ultimately featherweight indie drama about a privileged Manhattan lady and her relationship with her exotic and presumably wise driving instructor
This isnt just a good film because of the great acting however it has a solid romantic story and it touches on some important social issues as well
Somewhat lucid at times with flourishes of comedy as enjoyable as individual moments are it unfolds rather predictably
A Perfect Day is rarely actively bad but more often inspires a shrug
This compact little satire  set in s Balkans  is a small personal story about huge unfairnesses and injustices Bleakly bitterly blackly funny
The refusal to sugarcoat or overpraise its fearless dogooders is itself noble and useful and at its best A Perfect Day relishes in portraying characters we rarely see on film in a darkly comic fashion
Its frustrating how close this is to being a good movie The pieces are there The follow through isnt
The characters are too thinly sketched to generate much of an emotional connection leaving some compelling ideas unfulfilled in the process
The film may be scattershot and odd but it needs to be odder still
Although caught between Mash and Hurt Locker the film packs a powerful lesson
Gritbomb comedy dripdripdrip absurdism Altmans MASH Richard Shepards The Hunting Party and the grandiosely mad Emir Kusturica can be glimpsed in the rearview mirror of floundering influence
With an eclectic soundtrack welltimed editing and crisp cinematography  and of course that terrific cast led by the great Del Toro  A Perfect Day is a roughedged gem
A project thats intriguing funny emotional and sensitive one that raises issues abundantly important today and makes us consider whether were truly considering what else is going on in the world around us
There is a subtle tonal complexity to A Perfect Day where even the title has an air of sarcasm There is a lingering note of melancholy throughout the film and it earns every bit of it
By focusing on a small story the sarcastically titled A Perfect Day becomes more relatable and in the process tells us more about the big picture in this case the Bosnian conflict
This is one of those rare thoughtful war movies that doesnt emphasize brutality
An irreverent comedy in the style of the original MASH this wartime romp takes an entertaining look at  hours in the life of a group of humanitarian workers in the Balkans in
The movies got a great concept and Benicio Del Toro is terrific but the blackcomedy tone feels off from the very beginning
We see lots of shots of the aid workers two trucks hurtling back and forth across the landscape and the movie often seems to be just as aimless
This shaggy likeable film from director Fernando Len de Aranoa chronicles  obstacleridden hours in the life of some international aid workers
Like its subjects the movie fails to accomplish all that much but it does more good than harm
A Perfect Day is a war story told on a small scale Its all the more powerful for its subtlety
A taut darkly comic drama about the dilemmas of international intervention in civil war all of it neatly symbolized by one elusive length of rope It is also sadly a film much marred by its sexism
Klein is an excellent synthesizer of difficult ideas and knows how to make them accessible
All this material has a hard time coming together in a natural crescendo and no amount of vibrating violin strings can give it the emotional urgency or focus it badly needs in the closing scenes
Its sometimes boring it makes assumptions about its audience
This Changes Everything isnt a gamechanger but it is jarring enough to be scary
An unsettling but ultimately encouraging documentary about global warming and grassroots activism
By failing to address basic issues This Changes Everything comes off as misleading at best deliberately confusing at worst
If as many seem to believe we are presiding over the possible annihilation of our species then maybe we need a kick in the pants more than a reassuring hug
It distinguishes itself among similar planetloving docs by showing evidence there is hope in what can often seem an overwhelming bleak task to begin repairs to an industrialdamaged world
The evenly wrought film accessibly argues that runaway capitalism and shortterm economic growth at all costs has caused an imminent catastrophe and that global warming is the hotfoot instigator for a radical change in economic thinking
Klein and husbanddirector Avi Lewis cant make a rhetorically strong enough case for the central idea so they fire up the waterworks and hope youll feel bad enough to let them have their way
Klein and those impassioned protesters provide something that has been in short supply in the predecessors  namely a modicum of hope for the future
Despite Everythings desire to distance itself from other climate change ventures these early stories are perhaps the most conventionally structured for a documentary and also the most engaging
Riveting climatechange docu encourages grassroots action
An engaging and sometimes witty comingofage story it is a patch above some of Sandlers recent liveaction films
Proving once again that with the exception of PunchDrunk Love Adam Sandler is at his most tolerable when you cant see him this perfunctory animation sequel provides innocuous preHalloweenhalfterm distraction
A lively and amusing sequel to the  hit
Essentially if you enjoyed the original youll enjoy the sequel but its more of the same Cant we try a little harder than a gagfilled outline
Like the first entry in this animated Sony Pictures franchise the film is spirited engaging and has an idea its about mutual tensions between the undead and the living who have forged a truce up in the Carpathians
Still better than most Sandlerwritten movies
The jokes are just as easy and fall just as flat but the film is saved somewhat by a cracking last act
A delightful perfectly formed sequel to the animated ghoulsaregood originalThe animation is gloriously kinetic What more needs to be said If you liked the first one
This enjoyable sequel lands a slight notch below the original only because of a few scenes lazily squeezing more juice out of its predecessors best gags
Kids are not likely to get the jokes about Gary Oldmans wig in the  film Dracula nor sympathise closely with the emotional difficulties Drac faces in becoming a grandparent Adults will have seen virtually all of this before
A clever and funny animated romp that kids and adults will enjoy
This has no aspirations to be anything more than a throwaway  minutes of slapstick silliness
Theres a charm to these characters and its easy to see why kids are attracted to the story
What Sandler does pull off here is an uncommon feat a sequel thats an improvement over the original which itself was a solid movie Your kids will love Hotel Transylvania  and youll like it too
If you liked the first Hotel Transylvania youll like the second but while the special D effects look great the storyline can lack depth at times
Hotel Transylvania  isnt a horror movie but like its predecessor its made for those who love these characters so much that we like to see them in childrens movies
Has its charms but it disappoints with a script that feels sloppy and hastily slapped together
Hotel Transylvania  succeeds in creating the sort of wacky visual comedy that kids love  from slime monsters to screaming slices of wedding cake
Adam Sandlers Dracula sinks his teeth into another batch of slapstick fun so much more enjoyable than any of his recent liveaction movies
Still the material is a lot stronger than anything in the premisesquandering Pixels or lackluster Paul Blart sequel
This may be the only time Ive wanted to see a directors cut where theres less material
Thanks to Ashtons brilliant careerdefining performance were made to see that the only thing worse than doing evil deeds is being nice enough to feel guilty about them
Overall a very impressive piece
Uncle John is so subtle so exquisitely paced and so determined not to go in any of the obvious directions that its hard to believe the film is Steven Piets first feature
The two mostly unrelated stories culminate in a climactic crosscutting of sex and violence that borders on farcical
For a first film its very assured filmmaking and from here Piet and Crary could choose to go in either direction Uncle John points to  or any direction really
This is a truly special film that is intent on taking the road less travelled
Uncle John is an audacious combination of genres a slowburning mysterythriller mixed with a mumblecore love story Theres a dash of morbid comedy thrown in for good measure too
The kind of polished understated throb in the temple that gives independent lowbudget filmmaking a good reputation Uncle John is meticulously observed and startlingly good
The movies small but Ashton keeps us watching and listening
Steven Piet has masterfully interwoven multiple genres into one tightly crafted surprising little film
Impressively light on its feet given that its slogging through the twists and turns of a plot heavy on maggots gore and rotting flesh
The filmmakers dont appear fully committed to the idea of a zombie apocalypse so no sense of dread or suspense ever takes hold
Its all as ridiculous as it sounds with only the occasional attempts at dark humor providing saving graces Genre fans at least should be satiated by the copious amount of gore and viscera on display
Start stocking up now on the PeptoBismol
Rarely do zombie movies seem more desperate
It resolves all of the questions you never needed answered It also follows all of the supporting characters from the first film that you you never needed to learn more about
amidst all of this baroque busy plotting and tonal volatility the film loses the subtlety of characterisation that infected the original film
A passable sequel that earns points for trying to expand upon its predecessor even if it takes a little while to get to the new stuff
In a film that opts for grossout gags over scares Contracted Phase II feels like a rushed steppingstone towards bigger things to come if the franchise is to continue onward towards disgusting glory
A fatal new STD furthers its grip on Los Angeles in this uninspired bodyhorror sequel
Phase II has some nice comic touches but its a forgettable Bmovie
Reed and Frank may have overcommitted to capturing life as it happens taking the cinema clean out of cinma vrit But its a courageous first effort and a lot of people should be genuinely interested it what they come up with next
Sweaty Betty is a reminder that poetry comes in all shapes and sizes and that art ultimately dictates its own terms
This isnt hardtimes reportage or a deepdive ethnography Its a lifeasitslived picture a chance to meet and loiter with the people in the places the interstates zip past
The film  has a disturbing intimacy and richness that are startling
Defiantly amateurish yet never less than engaging Sweaty Betty is a true oddity
Though its not entirely satisfying the looselimbed feature exerts a genial pull in its offhand exuberance
A blackface variation of Babe  with street cred
Predictable fatherdaughter tale has solid messages peril
This  and crucially only this  is whats scary about Sinister  that only when you most expect it something may happen on screen thatll make you twitch in your chair
The original Sinister was a passable chiller but the sequel feels more like a vague attempt to continue a lucrative franchise than to create anything truly compelling or spooky
The script to Sinister  is a mess Mostly its boring but its also a hodgepodge of ineffectual dialogue and setups
This sequel has an eerie effect that stays with you long after its over Full review in Spanish
Familiar settings cliches and jumpscares theres very little imagination put into it and even less subtelty Full review in Spanish
The best way to describe this bad sequel is unnecessary Full review in Spanish
A waste of time Full review in Spanish
If youre the type of person that thinks horror sequels are a bad idea Sinister  is not the movie to make you change your mind Full review in Spanish
It has a couple of good scrares but its ultimately forgettable Full review in Spanish
In most respects its an improvement over its predecessor but its still more of the same with better characters Watchable but hardly essential
Sinister  plays like a sputtering unnecessary fourth act that lacks the tension and sense of dread of the original
The new perspective opens up possibilities but the story still feels rehashed
As the ghoul from the  horror hit stalks a new family this sequels sharply wellcrafted setup leaves the hackneyed conclusion feeling very disappointing
Sinister  basically destroys everything good the first one did Full review in Spanish
The ghost children require the boys to watch home movies of murder in this way the film ritualizes the aesthetic observance of violence to quote one character that gives its particular bogeyman  and the horror genre  life
Ciarn Foy provides a further link to the first film by resolutely failing to generate any decent scares
Whilst Sinister  at least manages to avoid being a mere retread the final product comes off as decidedly flat
A lackluster follow up to what was a fairly unimpressive horror entry
Sinister  depends greatly on cheap scares
In a film where ghostly children attempt to force other kids to watch horror movies only to be repeatedly told I dont want to this somehow manages to be a microcosm for Sinister  I dont want to watch any more either
A warm and funny tale of clashes both personal and social and an engaging showcase for a wonderfully rich performance by Regina Case
A familiar story of economic oppression is enlivened by good performances
comedienne Regina Cas is vital and present in every moment especially the minuscule and picayune humiliations that have compounded for years Classconsciousness is first and foremost in each emotional and comic moment to come
Though hardly revolutionary Mother subverts conventions  both cinematic and social
The characters are so accustomed to keeping up appearances that they cant bring themselves to say whats bugging them Their interactions may be mild but the claustrophobic imagery creates the sense of being trapped in a powder keg
The Second Mother manages to remain funny throughout its depictions of class struggle and motherhood  stories mostly told to death in dramatic fashion
Its always presumptuous to suggest that a filmmaker doesnt realize where her narratives attention should be directed yet its hard not to wonder if writerdirector Anna Muyleart picked the right protagonist
A great performance by Regina Case as the films putupon heroine and a telling look at class divides
Muylaerts writing is strong and incisive her shooting style inventive but the strength of her film rests with Regina Case the Brazilian superstar who makes her own individual play for Oscar with an unforgettable performance
Anna Muylaerts carefully composed images provide a cool stage for some hot acting  all of which manages to be amusingly uplifting rather than sociologically bleak
All the elements of the story fit impeccably together for a humorous and occasionally wrenching examination of relationships
It speaks to anyone considering hiring a livein nanny Regina Cas is terrific
It all builds to an affecting insightful climax deftly avoiding morality tale tropes while never failing to entertain
A powerful movie  one with no easy answers for these mothers and children
Brazilian upstairsdownstairs drama effectively portraying the class tensions which surface when a housekeepers headstrong daughter moves into the mansion
Subversive yet heartwarming  its a joy to watch
Muylaerts film a quiet kind of modern classic is less fascinated by the power play and ritual than it is by the people who are inhabiting those roles and hoping ever so softly to transcend them
This is a smarter film than any single aspect initially lets on
Writerdirector Anna Muylaerts beautifully shot frequently comical take on the second mother phenomenon sets the film apart from other explorations of similar material
Cases performance as Val is a joy
At its most basic level Irrational Man is like s Match Point a murder drama This time around though the satisfactions are relatively few
There are some funny one liners the plot although derivative will have you engaged from start to finish and overall its an enjoyable ride
Irrational Man is turgid to the point of ridiculousness and absurdly anachronistic
Irrational Man is a visually plain and simple film Full review in Spanish
The film is well made and there are good performances and I did like the ending However I did feel a bit slimed by this film The misogyny and the disregard for morality are troubling
As always Allen is a master in portraying the hypocrisy of the middle class but this time the synergy of the actors doesnt live up to the effect of the ensemble casts of his other films
Although far from perfect Irrational Man shows that Woody Allen keeps looking for new ways to express the ideas that have been eating at him forever Full Review in Spanish
A minor film about a straight white and privileged guy that complains about life like many other Woody Allen characters Full review in Spanish
If you have seen a couple films of the director you will probably enjoy the film for its intelligent and nonsense story However the fans will know in advance what theyll see Full review in Spanish
Woody Allen has the hability to keep surprising with complex and smart movies This isnt the case Full review in Spanish
Poorly developed characters make it hard for us to take seriously their actions and believe the story Full review in Spanish
Its not the best Woddy Allen film but at least its entertaining enoguh by commercial cinema standards Full review in Spanish
A film lacking inspiration that feels like recycled ideas from previous Woody Allen films Full review in Spanish
Defineately not the best work by Allen Full review in Spanish
Irrational Man is one of Allens lesser film however its still better than anything else on theatres right now Full review in Spanish
Irrational Man is far from being Allens best film but it has interesting drama to keep you entertained Full review in Spanish
A blithe and chipper drawingroom comedy that in a deliciously perverse way plays with notions of chance and karma and very bitter irony
Woody Allen does what he does best and the result is effective and entertaining Full review in Spanish
An excellent comedy with the usual Woody Allens themes but with a great mastery of suspense Full review in Spanish
Allen gives us another big profile of a read successful and sinister man one of his great strengths as an author Full review in Spanish
If Damon manages to get into the awards fray for this role  and he should  it will be thanks to the fact that he makes it easy to understand how a desperate man could still find a way to laugh
Fascinating feelgood and quite simply a hugely fun reason to head to the cinema
Damon has never seemed more at home than he does here millions of miles adrift Would any other actor have shouldered the weight of the role with such diligent grace
an often potent scifi drama that benefits substantially from Damons almost impossible charismatic performance
If the title Robinson Crusoe on Mars hadnt already been snagged it would be a perfect fit for Ridley Scotts space epic which maroons Matt Damons astronaut on the red planet and then shows him using Crusoelike ingenuity in a bid to survive
This strandedastronaut movie cant muster all that much drama Watneys crewmates just cant convey much emotion Watney mostly emits smartass movie lines NASAs tech and its astronauts gutsiness and smarts are glorified to the point of porniness
The Martian pushes the right emotional buttons and is a crowdpleaser
Science is front and center as Matt Damon is left to his own wits as he tries to survive The Red Planet until a rescue team arrives Destined to become a SciFi classic
Charming and exciting without ever losing sight of its tale of humanity and science
Will eventually find itself planted amongst the best in Ridley Scotts bulging anthology as it is perfectly paced visually stunning and mentally challenging
The ending is so farfetched that the suspension of disbelief dies long before anyone in the film ever does
Probably The Martian proves to be an entertaining popcorn movie that pleases general audiences but is beyond of being as good as Alien and Blade Runner Full Review in Spanish
A rollicking space procedural that depends on some logic and a great deal of luck
The Martian may be light but its a fun ride out of this world
The biggest surprise in Ridley Scotts latest film The Martian  is that Damon is very funny in a movie that you wouldnt have expected to have any laughs So if youve been put off
Scifi is withouth a doubt the genre in which Ridley Scott feels at his most comfortable and he finally delivers an entertaining movie worthy of his caliber even if its not a masterpiece Full Review in Spanish
When it comes time for the inevitable triumphofthehumanspirit that necessitates this regurgitated survival narrative all that computes is smugness and nationalism
I like my alone in space movies to be far weirder and more cerebral than The Martian Scott or Damon can stay on Mars fly me to to Sam Rockwell and the Moon or directly into the Sun with Danny Boyle
Its an adventure pure and simple in the tradition of Robinson Crusoe and Gulliver And every bit as timeless
Like Tom Hanks before him and Leonardo DiCaprio Damon doesnt shrink from the burden of being alone onscreen for so much of the filmhe owns it
If Killing was a cleverer film and perhaps a more informative one if you want to learn about the Indonesian killings at least then Silence is a more gutwrenching one
Focuses on the victims instead of the criminals
The Look of Silence is The Act of Killings straight injusticedoc brother Its graceful and eloquent and it has as its subject a lierotten culturenot merely in the official cant but in the hearts and minds of even the poorest people
Joshua Oppenheimers sequel to The Act of Killing wanted to provide an emotional and moral coda to the original as it sought remorse in the eyes of the guilty but in every beautiful saturated frame The Look of Silence finds only the blank face of denial
The Look of Silence stands on its own as a perfect addendum to The Act of Killing
Unlike many documentaries on unpleasant subjects Silence isnt an ordeal you undergo for the information value Its no upbeat experience but it is always absorbing and it is sometimes disturbingly beautiful
This sequel to The Art of Killing features much more soulpricking confrontation and demands from the aging perpetrators that questions stop being asked
oshua Oppenheimer has followed his  documentary The Act of Killing with another even more powerful film The Look of Silence
Oppenheimers followup The Look of Silence is more lucid but less interesting
Frightening illustration of how cavalier the perpetrators of ethnic cleansing can be about their heinous acts
Remarkably the third Oscar entry this year along with Trumbo and Iraqi Odyssey as bold leftist historical corrective And naming the names of Indonesian anticommunist genocidal death squads and the shadow government there enabled by CIA intervention
This is documentary filmmaking of the highest order  urgent necessary and deeply compassionate viewing
This documentary has the potential to induce nausea It is also like The Act of Killing landmark cinema
Powerful provocative and deeply affecting
I was somewhat thankful for be rid of the games of theatrical oneupmanship that made The Act of Killing a queasier experience The power of this terrible moment in history and its lasting legacy is no less powerful without them
Every scene weighs on the audience But Oppenheimer and Adi manage to locate a lightness as well that lessens the burden
Dont get it twisted The Look of Silence will disturb you
Oppenheimer turns his Act of Killing camera around in a sense to examine how the Indonesian genocide impacted those who survived
Its a film that shows how weak and fragile memory is and how the mind is powerful enough to suppress the greatest horror
It only takes a few minutes to fully comprehend how The Look of Silence is complementing and expanding the subject and why Oppenheimer is not letting this go
A definitive reflection on the work of two great directors and the specific slices of cinema they so fruitfully cultivated
Fitfully captivating though plenty amusing
Engaging look at Ricky Leacock from his friend Les Blank marks a valedictory for both filmmakers
Like Mr Leacocks other films it helps give the feeling of being there that he strove for
Provides a leisurely insightful history of documentaries that parallels the filmmakers biography done in the style he championed by Les Blank who he greatly influenced
The great documentarian Les Blank died in   two years after his mentor Richard Leacock  but both are warmly present in this set of interviews Blank did with his teacher in France  years ago
The ordinariness of this filmand the flatness of its videoshot images relative to Blanks beautifullooking s filmsisnt a significant drawback given how eloquent Leacock can be
The D sequences work The problem is that theyre too brief and nothing else happens in pretty much the whole movie Full review in Spanish
As a regular viewer of the franchise I was especially happy to see the refrigeratordoor gag make an appearance this time around and once again not pay off Gotta hold something back for the seventh one
Just ask someone you know to put a sheet over their head and suddenly run at you Its far cheaper and much more memorable than the grainy gimmick being pushed by The Ghost Dimension
Ugly ineptly written clumsily acted Ugh
Paranormal Activity The Ghost Dimension plays as dumb as the title sounds
Is this truly the end of the Paranormal Activitys Well lets hope so
Youve seen all this before so much so that very little of it results in the actual jumpoutofyourseat shocks that peppered the previous five films
Just isnt scary
Feels like parts four and The Marked Ones Just one big missed opportunity
Far and away the best thing about this latest is the claim that this is the last of the Paranormal series I can think of no better Halloween treat  assuming its true
its ultimately difficult to recall a franchise that so completely failed to stick the landing
Paranormal Activity The Ghost Dimension hits creative rock bottom The absence of legitimate suspense creates an atmosphere of unintentional comedy throughout
One of the most empty films ever to appear in a genre that has gone stale Full review in Spanish
A film full of jumpscares the ending is not surprising at all the only good thing about it is the D Full review in Spanish
At no point does anyone say put down the camera bro Youre freaking me out When do they think theyre going to watch this stuff back Hey lets crack open a few beers and watch those old tapes of us watching those old tapes I cant see it
Presented in D the special effects are more elaborate than usual but the regressiveprovocative idea that filmmaking and filmwatching open windows on unhealthy influences especially among the young remains undeveloped
What once was a chilling spectacle of a series has become an unimaginative and tedious mess
Longtime series editor Gregory Plotkin finally directs giving us bravura jump cuts spliced for maximum humor and dread
Its sad to see a series that started with such a bang go out with such a whimper But its better than wringing a seventh movie from this tired old formula
The film is Blumhouse through and through preferencing technical innovation and ingenuity over big budgets
Like few contemporary films Anesthesia distills the anxious intellectual tenor of the times
The effect of keeping the various subplots apart for so long is enormously frustrating because we dont know what Mols frustrated suburbanite or Stewarts morose student or Williams and Freemans strained friendship have to do with one another
I want to recommend Nelsons film in spite of how misconceived it is simply because it asks interesting questions albeit in some of the most banal ways imaginable
The beautiful performances and raw intimacy are definitely worth your time but its wispy good intentions ultimately dissipate into thin air
Like an East Coast Crash  with better acting and a higher IQ
Theres nothing wrong wpurely observational dramas or straightforward character pieces but the drama were given here is stale and lifeless and the characters even more so
Stiff and unsatisfying Anesthesia doesnt snap together profoundly straining to reach a sophisticated examination of desperation and confusion while it offers tedious dramatics typically found in a Lifetime Movie
People seek escape through selfmedication in somber drama
A pile of incomprehensible existential gibberish by the vastly untalented actorwriterdirector Tim Blake Nelson about the meaning of life in an age of technology told in the tiresome style of multiple characters who intersect at odd angles
A discourse on existential angst in the modern world the ensemble piece comes across as sadly familiar and as emotionally desiccated as its pallid characters
ake drink this is the thesis to my sketch drama The world has just become so inhuman Everyones plugged in blindingly inarticulate obsessed with money their careers stupidly arrogantly content I crave interaction but you just cant any more
A stellar cast of seriousminded playersfight a losing battle against the characters overwritten dialogue and unlikely behavior
A grand gesture going nowhere
Stewart stands out because her one big scene seems so passionate and genuine Its the only moment when Anesthesia seems to be working
The irony of Anesthesia is that while it uses interconnectivity as a storytelling mechanism the characters do not really connect
Is that all there is
In cramming together so many interlocking stories of existential angst the structure of this wellintentioned ensemble drama feels more forced than authentic
The movie isnt nearly as intelligent as it thinks it is its a plainly incoherent anthology film
What will numb our pain after meeting some of Anesthesias dull characters
A puzzling and uneven tonal mess
The Treasure is both another testament to his eye for telling details and Porumboius most seamless combination yet of fiction and fact
In a manner so sly you could overlook it Porumboiu invests this tissuethin premise with the shadows of Romanian history
Dont expect a big payoff in Corneliu Porumboius longbuild satire about some hapless diggers for buried treasure the journey is the joke here
The film is an unusual mixture of joy and cynicism
Though it almost overplays its hand with a Robin Hood storybook motif The Treasure manages a tricky balance between lowkey social satire and total fantasy
Minimalism of the sort that Porumboiu specializes in is an extremely hard register to maintain and in this film it involves the viewer walking a tightrope as much as the director does
The Treasure is like the work of Samuel Becketts longlost Balkan cousin  bleak stoic and suffused with a flinty exasperated empathy for its ridiculous characters
The Treasure has a broader social vision underpinning its surface subversion
This charmer is determinedly mundane and lowkey until an unexpected finale transforms it
Deadpan determinedly low key and deeply absurd the films of Corneliu Porumboiu are very much a particular taste and The Treasure is no different
Its good absurdist stuff a little reminiscent of the work of Aki Kaurismki albeit without that filmmakers woolyheaded whimsy
The Treasure is a onenote film thats a dry bleak endeavor devoid of character and lacking personality
The Treasure suggests that you can have both feet of clay and flights of fancy and in the battle between the prosaic and the pieeyed the prosaic doesnt always have to win
Slowly  something changes and the functionality of Porumbouis style gives way to an eversoslight hint of wonder
A less artful version of A Pigeon Sat on a Branch Reflecting on Existence  though it may help to be Romanian
If the humor were any drier it would be dust
As the title suggests there are rewards in this slight but consistently amusing gem
These recent Romanian films have sometimes been gloomy about both past and future but The Treasure sprinkles a little sugar in the medicine
A tightly focused minimalist and enchantingly humane story of individual struggle within the broader social reality of contemporary Romania
A film that recalls the clarity of the parable or childrens story in its unadorned yet subtly suggestive narrative
Two secondact revelations alter its tired dynamic for the better but those changes are undone by cheap scares and a climactic revelation thats more hohum than horrifying
I was somewhat dissatisfied with the ending but that sort of thing is subjective and I encourage everyone to watch The Abandoned to judge for themselves
Jason PatricMan what happened
A polished but overly familiar psychological thriller
If youve ever wondered why studios treat this month like a celluloid landfill The Abandoned presents a compelling case study on its own lack of merit
The Abandoned is a steadfast and creepy haunted flick until the final five minutes sink the entire production Itll work for some but sadly not for most
Eytan Rockaways generic chiller clings so tightly to conventions that it fails to even moderately raise ones pulse
Just when you think this IFC Midnight flick will slide into an effective genre track it flies off the rails in spectacular fashion getting more unbearable and ridiculous all the way up to its closing minutes
The film is effective for long stretches mainly due to Rockaways superb use of his principal setting
Despite its formulaic structure The Abandoned has a lot going for it It eschews cheap scares bloodletting and gore
A visually and emotionally effective descent into darkness
Cusack says at one point I did what you do with a puzzle I stared at it until it made sense That I can guarantee you wont happen with Shanghai
Everything in Mikael Hfstrms film is needlessly bloated to accommodate its status as an international prestige production
The film often feels like a drab collection of scenes where John Cusack disinterestedly questions attractive elusive Asian cast members
Shanghai isnt altogether awful but knowing that it has the potential to be so much better in the hands of a stronger director makes it a frustrating affair
Goes through all the motions of an oldschool wartime spy pic with plenty of technical competence but zero panache the filmic equivalent of a bar band working through one last Skynyrd cover just before last call
A morass of clichs and orientalism salvaged from the Hollywood backlot debris of  years ago
Cusack does his best to evoke compassion for his character and he is quick on his feet whether trying to woo Anna or outsmart his captors But none of it is enough to highly recommend Shanghai
Its all intriguing and breathtaking along with a mournful lovely score by Klaus Badelt but not enough to compensate for the miserable writing
How many times does John Cusack have to prove he is no leading man In Shanghai he comes off as Sam Spade with chopsticks
Hfstrm directs it like hes sucking too hard on a cigarette in a quick overanddonewith style that results only in fitful coughs of smoke
Director Mikael Hafstrom and a fine cast and crew have made a film thats elegant and gauche in roughly equal measure
A turgid anemic already fiveyearold motion picture from Swedish director Mikael Hfstrm
Dont look for plausibility in this World War II epic set in China just enjoy the lavishly produced exotic and often exciting ride
It is hampered by weak writing and a badly executed story line
Under Mikael Hfstrms visually clunky rhythmless direction its a snooze of epic sameness choppy action scenes a blankly stern Cusack and too many allegiance shifts to count or care for
You might be forgiven for expecting either an unfairly squelched masterpiece or an unholy mess The movie is neither although it falls closer to the mess side of the table
Even if an uncredited editor were hired to make clear sense of these pieces the film wouldnt work
Cusack is halfway between Humphrey Bogart and Jimmy Stewart and the result doesnt come together
Written by the talented Hossein Amini and clearly inspired by spy and detective movies of the s this seems to have been a labor of love But SHANGHAI is so convoluted that it eventually becomes lost
The truelife historical background is more compelling than the fictional melodrama
Hell And Back may be for adults but it is not very mature Everything from drug use to rape is lampooned in this outrageous Rrated comedy
Its a wonder how this stinker of a script  packed with foul language sex references and scatological jokes  attracted such a strong cast in the first place and the finished film is no better
Hell and Back is a far cry from something you would sell your soul for
A script like this should be destroyed upon receipt
Alison Brie and Jason Sudeikis offer performances that manage to be funny sweet rude silly and uncomfortably painful often all at once
Sudeikis is fast and funny and Headlands willingness to tackle the unromantic aspects of the romcom lends a sophistication you dont find in more anodyne examples
I wanted to really like this movie but the laughs largely werent there and the ultimate spiral of the movie attempted to rely on shocking dialogue and situations rather than actually pushing the envelope
The guy sat directly in front of me stood up loudly declared to everyone that This is the worst film I have ever seen in my entire life and just walked out
Like Bridesmaids and Trainwreck before it Sleeping with Other People is a sometimes raunchy comedy that occasionally tries too hard to shock
What matters is that these two brilliant flawed people finding each other at this moment in their lives made them each more whole Whatever happens next is beside the point
If it eventually surrenders to convention Sleeping with Other People is still a refreshingly adult often rulebreaking romantic comedy
Inevitably it pretends to be edgier than it actually is and doesnt create a sustained character development in the way Nora Ephron managed but its watchable for all that
Its all so selfconsciously metropolitan so determinedly uptotheminute that even at a fairly concise hour and  minutes it seems too long
Headlands romcom may have sex on the brain but it certainly gives a couple of Hollywoods lesser lights the chance to shine
Of course we all know how its going to end and the films crushing predictability could be ignored if it raised more laughs  but this is all over the place
Writerdirector Headlands screenplay is sharply observed in places and yields a few memorable set pieces including a brawl in a restaurant that shocks a pregnant woman into going into labour It is also utterly predictable in its plotting
Sleeping With Other People is an updated  and raunchier  take on the When Harry Met Sally romcom premise can a man and a woman ever be best friends or will there always be just a troublesome frisson of attraction
Headlands script sometimes feel more like a  zeitgeistcapsule than an organic piece of comedy but the mix of sophistication and unabashed raunch has a definite light charm
Brie is bright and engaging but womanising wisecracking Sudeikis seems awfully bland to be the object of so much desire
Perhaps Headland is afraid of her films being too unlikable for audiences or this an attempt to legitimise herself within the mainstream Because ultimately why sacrifice all this smart perceptive modernity for a finale hewn of Hollywood clich
Director Leslye Headlands followup to Bachelorette addresses big issues like the role of sex in relationships while delivering refreshing laughs thanks to a supporting cast who make their value known Adam Brodys cameo is a revelation
funny sexy and wonderfully free of reflexive irony Its exactly what a romantic comedy ought to be
A pretty simple and easy going romantic comedy that keeps you hooked by the realness of its story Full review in Spanish
The romantic comedy has reemerged alive and well
Trying to win over the viewers with the strongest stomach for such things as devilish dismemberment is too depraved for me
The story echoes that of Roths  success Hostel but it represents a descent into cynicism The Americans butchered in a foreign land in the earlier film were sexual predators but most of the kids here are guilty only of cluelessness
Any sense that the kids die because they never bothered to research the tribe they were trying to save is undermined by the movies treatment of the natives as just as abstract
Eli Roths new movie may be the scariest film you will ever seeif youre a food critic
Its the same xenophobic sophomoric silly slop that director Roths been feeding audiences since Hostel
The result is a film that will delight Roths fan base of gore fanatics but will be quickly forgotten by everyone else
South Park took a bite out of social justice warriors last year Eli Roths The Green Inferno embraced that spirit  literally
nothing if not cynical about human nature although it does allow at the end for some level of human decency to emerge from all the carnage both physical and political
Too much time is spent preaching against social activism and not enough time is spent on the actual consumption of humans Its a cannibal movie for crying out loud
Whilst this is perhaps Roths most mature film to date and looks frankly gorgeous if youre not already on board with the provocative style of filmmaking Cabin Fever Hostel youre going to find The Green Inferno hard to digest
The films failure is thumpingly basic Roth just lacks the chops to turn everyone into chops in any way which scores as either potent horror or lipsmacking satire
The sheer chutzpah of Eli Roth escalates in this breathtakingly crass ultraviolent satire targeting the liberal PC classes  a twist on Ruggero Deodatos cult shocker Cannibal Holocaust
We see certain films so you never ever have to and The Green Inferno is one such atrocity
Eli Roths cannibal horror movie has its full quota of gore but this is much more than an exploitation pic
Roths beautifully photographed mansinhumanitytoman tract offers brutal thrills grisly gore and bleak thoughtprovoking horror served up with the signature aplomb that his fans appreciate
Overall a partial success
es Roths film has its expected moments of high transgression but you can never get away from the fact that all these references have been attached to the most hackneyed teen slasher narrative imaginable
Flatpack acting fratboy screenwriting the portrayal of activists is spitefully dumb and retro gore combine with smug throwback neocolonialist racism and unfunny jokes about diarrhoea dope and ScoobyDoo
This one will leave a bad taste in the mouth
If there is a target for the pitiless cynicism of this brutal exercise in cannibalistic gore I cant figure out what it is Inhumane in multiple directions
It doesnt break the mold but good pacing an a good balance of comedy and gore make this a good movie Full review in Spanish
The best scouts movie since Moonrise Kingdom and this time with zombies What more could you ask for Full review in Spanish
If you arent a stoned teenage boy then consider sitting out Christopher Son of Michael Landons new film This isnt for you
The Scouts Guide to the Zombie Apocalypse is a one joke movie like all genre splices before it but its a good joke  if a little laboured A comedy with a contagious sense of fun
The film is full of tasteless humor poop and sexist jokes all over Full review in Spanish
minutes with not many laughs or scares Full review in Spanish
Coarsely explicit groincentric slapstick and trendy zompocalypse theme aside this is a surprisingly lively and agreeably goofy throwback to the Fright Nightstyle teen horror comedies of the s
An uneven yet passable horror comedy
Its like if you grabbed The Walking Dead and American Pie and mashed them together with fun sexy and surprisingly hillarious results Full review in Spanish
Politically incorrect excesive raunchy and in bad taste but its very funny Full review in Spanish
Scouts Guide isnt good enough to qualify as a guilty pleasure but it gets an extra mark or two just for its zombie cats thats something you dont see in The Walking Dead
Inspite of how disgusting it could be this is a fun movie specially for teenagers Full review in Spanish
Scouts Guide to the Zombie Apocalypse does not seek to compete against higher profile zombie movies but it turns out much more satisfying and entertaining than most of them Full Review in Spanish
Entertains with gross out humor hilarious oneliners and an action packed climax I was pulled in to
Beyond its tardy fitful contribution to the undead evolution this teen flicks apparently never heard of the actual womens revolution So were treated to some Bhorror fun here and there increasingly buzzkilled by hornymoan maleitin humour
The film delivers the kind of entertainment fit for a certain maturity level your inner seventeenyearold may enjoy this more than youd care to admit and against your better judgement
Demerit badges all round
Of course the movie lacks any sort of logic but why does that matter if weve never seen an actual zombie horde Better to enjoy the action and forget that its not supposed to be a work of art Full Review in Spanish
If comedy horror that mines silliness and genitals for humour is your idea of hell this will bring you close to suffering But if youre a fan of the genre I think you might rather like it
Zombie Apocalypse is as dumb as a box of rocks and as spectacularly violent as youd expect
It revives hope for a popart cinema thats capable of treating characters like actual human beings rather than pawns on a chess board
This uneven but sweet bildungsroman is distinguished by its light tone and idiosyncratic handle on genre conventions
Its stylistic brio makes Prince enough of a live wire to bode well for de Jongs future
Expertly paced and gorgeous to behold Sam de Jongs Dutchlanguage directorial debut sets a stylish comingofage story in a bleak Amsterdam housing project
With pieces of its plot not deepened or stretched out far enough and tidily assuming their predictable spots in the finale Prince oftentimes proceeds like a parade of tropes
The dialogue is stylized part hiphop part American Western and many compositions are studies in landscapes that are cool and resistant to the figures within
At its narrative apex Prince veers toward expected tragedy and then unexpectedly crumbles both stylistically and thematically
De Jong indulges in some shocking violent imagery in these passages and this detracts from the films effective social portrait so too does the aggressively loud synthpop soundtrack
Instead of shades of ambiguity or a structure that quivers upon impact Sam de Jong the writer and director of Prince depicts a universe in absolute terms Ayoubs arc from good boy to bad prince is too rapid and uncomplicated
Although the disaffected youth drama knows no international boundaries few have felt as fresh and consistently unpredictable as the slyly satirical Prince by Dutch filmmaker Sam de Jong
Prince could use another  minutes to flesh out its narrative and its relationships  what movie couldnt  but it uses its time wisely and with an enormous efficacy only matched in size by the films heart
While Prince feels a little slight when the credits roll less than  minutes after it begins its still a strong creative addition to the crowded comingofage genre
Prince may be a little heavy on the style but it is a contemporary style that lights up the story with energy
A slender morally simplified fable that makes up for its tonal and narrative imprecisions with considerable visual energy musical pizzazz and a panoply of colorful characters
It wants to be cute it wants to cool and it also wants to be thoughtful and engaging but Sam de Jong is unable to make the story feel tight and focused enough to allow it to succeed on all those levels
Like so many comingofage debuts before it Prince isnt worldchanging but like the best of them theres enough youthful electricity to De Jongs work to make you want to see what he does next
A Eurotrash hood movie ineffectively placing a posts costume design and pastel color palette inside script elements as old as Mean Streets or something like Juice
Blunt and Benicio battle bad guys in best in TexMex drug war saga since Traffic
If that is an experience to reccommend I ignore it I enjoyed the film on several levels but found it depressing and tedious in others Full Review in Spanish
It examines the Mexican drug cartels and how corrupting they are and how ruthlessly they operate The best scenes are the most brutal
Sicario is ultimately so obsessed with the minutia of its subject matter that its often as engrossing as a day at the office
Sicario is as brilliantly made as Prisoners perhaps even more so
There is actually little dialogue in this film Facial expressions and the scenery take over for words
Blunt is terrific in a physically and emotionally demanding role
Intense compelling and utterly nerveshredding Sicario is a cinematic gutpunch of a movie that leaves you reeling
SICARIO is playing in theaters opposite SPECTRE and generally is the same genre of film but there is no doubt that SICARIO has a better and more relevant story with more believable characters
Sicario may have a lot of downtime as it slowly unravels its plot but it more than makes up for it with plenty of thrills a fine ensemble and excellent work from cinematographer Roger Deakins
The protagonist here is not the story but its photography Full review in Spanish
Horror masquerading as actionthriller a desertGothic descent into the nightmaregrotesquerie of Mexicos narcowar Brutal and dismal in its Stygian twists Sicario indicts the drugwar as a relentlessly macho endjustifiesanymeans battle
Yet another unwinnable war
Once again Villeneuve leaves the moral judgement to the spectator with an open ending that will leave you guessing Full review in Spanish
Its the films pervading sense of dread and unease  enhanced by Roger Deakins sublime cinematography and Jhan Jhanssons skinprickling score  that keeps us on edge
It delivers a constant exhilarating stream of elaborate and exquisitely photographed thrills that ends up largely compensating for the wouldbe profundity
Villeneuve pulls no punchesits refreshingly frank and while it may not be a feelgood movie its a film that youre not likely to shake too quickly after it ends
There is much agony in this film but its depiction is as forthright and unpretentious as it can be and as such it must be respected in its search for verisimilitude
Sicario is a good film filled with great moments and good timing for suspense and action but its main conflict weakens as Emily Blunts character does too same with her will Full Review in Spanish
This is a film built on a foundation soaked in nitroglycerine every situation seems pitched on the verge of combustion
Its as unsubtle as a boot to the head but its dourandcampy lofi style is far preferable to the spastic flash of its bigbudget genre compatriots
Makes use of every room in the country house and every rock in the country mountains Adkins gets more acrobatic as the movie progresses like hes building up to the big jumps but he still does plenty of fancy footwork and fistwork
Once Adkins and Florentine get their engines rumbling Close Range manages to deliver some compelling combat sequences blasting kicking and stabbing its way through a southwestern war zone
The conclusion hatched by Adkins frequent collaborator director Isaac Florentine is as inevitable as the films basiccable destiny
Whilst Close Range doesnt reach dizzying heights of Bmovie madness theres still a lot of fun to be had
Kids face peril in silly talkingdog adventure
The thin story has been stretched like Silly Putty to featurefilm length and the result is utterly seethrough in its sledgehammer moralizing
Captive seems more concerned with delivering empty platitudes than with even beginning to address the complex root causes of human suffering
Yawn
They let the story both fascinating and inspiring speak for itself and leave the rest to Mara and Oyelowo each of them perfectly cast
Captive to my chagrin never finds its story  that is it never finds the meaning the arc the narrative that justifies its retelling
Solid but wholly unspectacular reallife crime drama that is only in the least bit cinematic because of its leads
The actors are committed  Mara generally waiflike appears frail indeed  but theres barely anything worth committing to
The films whole agenda is the problem
Profoundly idiotic
What begins as a straightforward thriller tracing a manhunt following a lethal courthouse shooting bizarrely morphs into a paean to the powers of a Christianitythemed selfhelp book
You feel as if you have sat through a modestly interesting picture under mostly false pretences
There is a moral somewhere in Captive but it is hard to work out what it is
Committed performances from the two leads provide ballast although the inevitable Oprah coda feels a little too much like a sales pitch
Its never clear to what extent God really touched these characters nor do you ever really fear for Ashleys life The result has a TV movie feel
Hokey nonsense designed to celebrate the power of piety
Hostage thriller about escaped prisoner has violence drugs
Finally Christian audiences have a topnotch captivating film
How does an actor depict something as ineffable and internal as undergoing a spiritual awakening Mara does it with only the darting of her eyes and the slowing of her breath and its an extraordinary moment that should be remembered come Oscar time
So inept a film so bland and monotonous that it fails even to serve as the blatant ad for the certain Christian motivational book it would appear to be
Captive is TV veteran Jerry Jamesons first theatrical feature since Airport  Thats by far the most interesting thing about it
This segment about how food heals a community is poignant and powerful and makes the hourlong doc feel less like something youd find on the Food Network
For all its halfcocked plotlines Ashby is able to maintain a consistently humorous and light tone
The film displays little ability to utilize Ashbys violent actions for means other than highconcept fodder and outofplace bloodshed
Its Wolff and Rourke who have to carry the load and for the most part they do
A winning film about reconciling ones selfimage with reality
The bubbly Wolff and laconic Rourke have a nice comic giveandtake but Rourkes face in the right light looks like a bad pottery experiment
As uneven as the movie is it does manage to find a few pockets of compelling behavior to explore
A semiengaging comingofage story with a smidgen of actionmovie violence added to the mix
Its a comedy afraid of being too funny lest its macho sentimentality seem even more ridiculous than it is and a drama afraid of appearing too serious lest you dismiss it as hogwash
Its a quirky lifelessons setup that while occasionally earning deadpan laughs tries for but never achieves Wes Andersons patented mixture of the archly witty and the sneakily emotional
A smart funny and fresh variation on an ageold cinema trope is sparked by Mickey Rourkes richest performance to date
Charming comingofage story with good performances by Wolff and Rourke even if the later is a bit too musclebound and coiffed to accept as a dying hitman
Folks buying a ticket to Ashby hoping to see a film about the lategreat director of Being There and Harold and Maude are about to be sorely disappointed So is everyone else
The ambition outpaces the execution of this comingofage drama
Ashby is engaging enough and features strong performances by Rourke and Roberts but its not a film that will linger on the mind for very long
A genreblending adventure which somehow successfully combines elements of the comingofage and last hurrah formulas
Mickey Rourke is a grizzled exCIA assassin improbably mentoring Nat Wolffs misfit teen in this lukewarm genre jumble
Complex violent comedy explores friendship and mortality
Sometimes the cast doesnt seem so sure its a real movie and especially for the first act it feels like they shot everything in one take and left them all in the movie Maybe they were afraid to yell cut
So much is random and lame here handed little consideration before being committed to a hard drive Joe Dirt  is an awful film absolutely unwatchable and depressing
Precisely as good as it sounds
But even by the rather lofty modern standards of cynical pointless cashins Joe Dirt  is pronounced a sequel to a mild hit from nearly  years ago that even its most ardent defenders would be hardpressed to justify
Pointless sequel isnt funny lots of profanity crude humor
At an egregious  minutes Joe Dirt  feels like a directors cut where every single moment of footage was carefully preserved no matter how pointless or unfunny or digressive it might be
Youd think that after  years theyd have more than stale fart jokes and weak callbacks to bits that werent funny in the first place
JOE DIRT  BEAUTIFUL LOSER is not a good film In fact it is utterly terrible
Memories of the Sword stands apart from other action films because Park wisely imagines violence as an elemental clash of dispositions
Park Heungsiks Memories of the Sword is a historical Korean action film that delivers plenty of what its title promises
The plot in the South Korean martialarts periodpiece Memories of the Sword may verge on the incomprehensible but boy is it gorgeous
It would all be eyerollinducing were it not for Ms Jeons striking poise and the luminous presence of Ms Kim an eager performer who imbues the film with energy
These actors keep Memories of the Sword watchable and give us faith that well see them in better projects
Lavish Korean historical epic has many things going for it but a bewildering plot and an undeniable repetitiveness undermine its intent
Not even Alist stars Jeon Doyeon and Lee Byunghun can save this plodding Korean martialarts epic
An engaging tale that doesnt reach the level of epic but should appeal to fans of wuxia action and majestic visuals all the same
Its fast slick and lovely to look at but theres absolutely nothing there beneath the hood
Time and again the filmmaker cuts the money shot meant to theoretically cap a sequence
Pitched somewhere between Duel and Joyride this motorpsycho picture fails to stake out new territory or ring any significant changes on a decadesold formula
Less a chase picture than a drivingaround picture the movie relies heavily on long action sequences that never develop much zip
Exudes familial love and forgiveness
Infinitely Polar Bear is a muchneeded reminder that the realm of film is still a place to encounter something other than comicbook scenarios
Ruffalo is generally wonderful at finding the tone and mood of a character and holding to it here he has to bounce about but again he latches onto a consistent energy that makes Cameron a singular life force
The tone of Forbes attimes surprisingly funny script and the latest fine performance from Mark Ruffalo combine for a sweet compassionate look at mental illness
Infinitely Polar Bear suffers from the same problems as many other movies dealing with mental illness But a look back at writerdirector Forbes goals for her film shows that she accomplished what she set out to do
Infinitely Polar Bear certainly gives off a palpable sense of warmth and sincerity but I wish that Forbes wanted to give her audience something a little more substantial than a case of the warm fuzzies
The story may seem lightweight to anyone in search of blockbusters during the summer but its a welcome bit of personal storytelling in which the only thing at stake for this family is everything
Writerdirector Maya Forbes sets a sweet and compassionate tone throughout as she charts Camerons trialanderror growth as a parent
Surfacebobbing childhood memories may not deserve movie treatment
Only really works when all the parts fit together I found it hard to judge until the film ended Up till then I waffled back and forth as to whether or not I even liked it
It often finds a lighter touch to some dark subjects At times this is charming but just as often it feels tonedeaf
Forbes dug deep into her family history for her debut as a writerdirector and Polar Bear is a beautiful story warm and generous of spirit
Infinitely Polar Bear might be called an impressive debut a slice of life that indicates a sensitive and empathetic presence behind the camera This might have been a movie Forbes had to make but its unlikely to be her best one
To paraphrase a politician in a campaign ad I approve the films message which suggests that a chaotic but loving household is as likely as a disciplined environment to produce welladjusted and more important interesting creative children
Infinitely Polar Bear is a vivid snapshot of one family surviving in a state of chaotic affectionate imperfection  and of a time when kids raising themselves was seen not as neglect but as normal
The performances are fine and its no mean feat to derive warmhearted laughs from mental illness
Infinitely Polar Bear just rolls along leaking all reserves of authenticity You could argue that feeling aimless and oddly hollow actually is the right way for a film to reflect the close observers experience of mental illness
Forbes drama is very moving and has great compassion for Cameron without turning him into some sort of saint or excusing him for his recklessness
As is the way in these Sundance movies there are moments of beauty nothing much happens and at one point somebody runs through the woods
Firsttimer Forbess earnest resolve to demystify bipolar disorder prevents her film from establishing a satisfying story arc Ruffalo is good though
A wholly unpleasant ungratifying experience
Robert Zemeckiss computergenerated spectacle about wirewalker Philippe Petits famous promenade between the Twin Towers lacks any sense of tension because everything about it feels fake
While Zemeckis who cowrote the film with Christopher Browne makes sure the movie looks wondrous most of the characters are campy caricatures especially after Petit and company touch down in the Big Apple and mingle with the Noo Yawk folk
There are moments when one regrets heeding its call to step right up but when Petit steps right out all is forgiven
Its worth it because the nerveshredding climax deserves high praise indeed
Zemeckis jettisons most of the hard edges of Petits character in favor of a wideeyed followyourdreams story Rendered in broad strokes it falls more into the fairy tale category than anything else
Its an entertaining adventure that captures your interest and mantains it throughout at all times despite certain weak moments in the script Full Review in Spanish
The Walk encompassing all the magical elements cinema has to offer serves as a gorgeous tribute to those buildings loved admired and missed by so many
Joseph GordonLevitt makes a physical gestural and vocal work truly memorable Full review in Spanish
Zemeckiss film must be proud of being the most dizzying in its literal sense of history Full review in Spanish
While Man on Wire remains the better telling of Philippe Petits story Robert Zemeckis The Walk is a fine adaptation of these events culminating with one of the most breathtaking climaxes of the year
In spite of GordonLevitts pretty terrible performance the last thirty minutes of The Walk are what makes it worth the price of admission
The real star of the show is the World Trade Center with its ghostly appearance that still evokes heavy emotions to this day
Undeniably goofy in tone but welcomingly sincere in spirit  for those who can get on Zemeckiss wavelength The Walk offers an array of pleasures
This is a fun and exciting execution of an inspiring story Its beautiful Its pure Zemeckis
A technically brilliant sweatypalmed account of the man who walked between the Twin Towers
Petit is the story the story is a conduit for effects and effects are The Walks way of circling back to him
Robert Zemeckis latest piece of cinematic wizardry is not a good film  but it may be the greatest Imax film ever made
The Walk has good visual effects and an interesting story but it unfortunately gets tiresome nearing the end despite solid acting Full Review in Spanish
Zemeckis is in his element here and the film starts to pick up energy and as the outcome is no doubtits history after allZemeckis eases us from the thrill of the caper mechanics into something more spiritual
Dont look down may be the first commandment for aerialists but director Zemeckis spectacularly ignores it His camera soars above Petit swoops below him comes in close on his face his slippered feet his delicate hands
Experimenter is a psychological thriller in its own way
Mostly playful if occasionally pretentious
This isnt just a director showing off and having fun with an offbeat screenplay Almereyda is actively challenging us to take a fresh complicated look at Milligrams research
Cinematic portrait of a provocative psychologist
Well acted thoughtprovoking drama about role of authority
A compelling experience a film of ideas that is almost a tone poem too
Experimenter makes many smart choices and choosing to have part of the films story play out like a staged play helped the narrative exponentially
A robust biopic on Yales controversial social psychologist Stanley Milgram
In the end Experimenter feels a lot more like an avant garde play than it does a movie but I think thats probably exactly what Michael Almereyda was going for
you need more than an intriguing social experiment to drive a featurelength film
A smart biopic a great history lesson and an ode to critical thinking
Chilling docudrama revisiting Yale Professor Stanley Milgrams surprising experiment in human behavior finding a sadistic streak in subjects directed by an authority figure to torture by delivering electric shocks
Director Almereyda is as much an experimenter and illusionist as his subject
Experimenter isnt groundbreaking but a respectable contribution to the existing body of knowledge  and given its evident ambition this must mark it a failure
compelling even for those who know the outcome and it conveys contemporary relevance
This is a fairly effective blend of biographical drama and semidocumentary form with some unusual film techniques like black and white rear screen projection time traveling scenes At times the fourth wall is also broken
What makes Experimenter so absorbing and cogent is how Almereyda uses biopic tropes to explore Milgrams legacy in a formally and narratively meaningful way
Almereyda wants us to be aware that this is a fiction even as he explores the ideas with great intelligence and the life of Milgram with curiosity
Impressive in its refusal to obey formal convention but less thrilling as intellectual legwork to which Milgram dedicated his life
The movies unsettling depiction of our capacity for cruelty makes it essential viewing
This writer laughed so hard that I spilled beer all over my lap and then carried on laughing Ferocious inventive and oh so very gory Deathgasm is the most fun youll have with a horror film all year
an affectionate skewering of comingofage tropes metal culture and New Zealand mundanity where the bloodsplattered headsevering end of the world proves utterly endearing Death to false metal
Shamelessly lowbrow reaching a beerfueled gleeful high with a zombievssex toys battle its a very metal tribute to the grand tradition of Kiwi splatter comedies
Director Jason Lei Howden has a flair for punchlines that are funny for reasons that are essentially impossible to describe
Confident and giddily brutal
For everyone already inclined to see a heavymetalthemed New Zealand horrorcomedy called Deathgasm heres some good news The movie is exactly what it promises to be
A Cannibal Corpse album cover come to life Deathgasm is a wily creation from writerdirector Jason Lei Howden thats teeming with humor and gore
The gross hordes of undead spew humours and inner organs all over the screen in outstandingly nauseating ways
Deathgasm combines the visual flair of Edgar Wrights Scott Pilgrim vs the World with the manic gory energy of Sam Raimis Evil Dead  In the horrorcomedy pantheon this movie most definitely rocks hard
Deathgasm isnt just for lovers of metal and horror enthusiastsits a movie made for any misunderstood loners
It wont carry fans of Shaun of the Dead or Army of Darkness into unexplored territory but its energetic enough to slap a stupid grin on your face
A brew that aims for a campy midnightmovie vibe it never quite achieves
In between the rampant fourletter words and the occasional partial nudity are likable attempts at humor  some sweet some saucy
The director and his stars commit to the insanity of their project
With its localized demonic outbreak Deathgasm plays a bit like Demons or Evil Dead but with the energetic lighthearted appeal and splattery gore of Dead Alive and the clueless fanboy heroism of Waynes World or even Beavis and Butthead
Deathgasm drips with classic splatter film goodness If youre on board then the little issues wont detract from this darkly fun unique little film
Its the rare overthetop horror show thats also brimming with heart Even if that heart is being ripped from somebodys chest
An unhinged horror comedy
Howden can be hugely entertaining when he mixes some brains and heart in with the guts and gore which is about half the time  mostly the first half
Deathgasm is perhaps too busy for its own good and not always seamlessly plotted but the movies infectious sensibilities are hard to deny
Definitely worth a spin
A drama set around a cultural movement in s Britain this film captures the period beautifully but its story is so underdeveloped that it leaves the fresh young cast without proper characters or relationships to play
The plot is ropey but the soundtrack is fantastic Whitehouse shows great screen presence and the likes of Steve Coogan Ricky Tomlinson and Lisa Stansfield add some colour to the supporting roles
Newcomer Langridge does an excellent job of making John grow up through the course of the film subtly moving him from mouse to man through a gradual increase in confidence
Brilliantly captures the amphetaminefuelled energy and sweatsoaked fervour of s northern Englands soul music scene
Northern Soul feels driven by youthful energy
This project was clearly a labor of love for Constantine but it is hard not to conclude that her original documentary idea might have had a lot more passion than this flatfooted fictional treatment
The convincing atmosphere performances and soundtrack of vintage Yank RB obscurities make this an enjoyable throwback to a scene whose influence if not its fame eventually traveled far
While the dynamics of John and Matts troubled friendship are nothing new Northern Soul does a fine job of creating their world
A revealing if disappointingly thin and histrionic drama about a couple of yearold guys who gamble their dreams and future on riding a wave of youthful passion for soul music
Funny and feisty gritty and sometimes grim this first feature from the photographer Elaine Constantine delivers a sweaty snapshot of a very specific time and place
The first feature from writerdirector Elaine Constantine herself a Northern soul veteran the film follows a fairly familiar innocentledastray storyline
Where this movie comes up short is in bringing any kind of real life to its bildungsroman side
The film has a ferocious shaggy energy that mirrors that of the testosterone amphetamine and adrenalinepumped boys
Its best to not quibble with the details and just enjoy the sights and sounds of this period drama
Every last detail feels authentic from the slightly unfashionable clothes to the cramped rooms in which John and his friends sort through records and piles of pills
The cast is mostly made of newcomers and they are very good indeed
Disaffected youth and music go together like  well theres actually no better simile So how else to describe British director Elaine Constantines first film For one thing its got a welldefined sense of place and time
Rich in period detail its as transportative to the shuffling spins fashion subculture and euphoric amphetaminefuelled allnighters of  Lancashire and the cultfollowing DJs of the Wigan Casino as a documentary turned up to
The movie is carried by the two harddancing leads whose sweatsoaked strutting demands attention
The movie is dull right up until the point that it becomes completely characteristically bonkers with Miike trotting out some of his trademark obscene violence
Over Your Dead Body is a convoluted spectacle that sees Miikes visual prowess wasted on a puzzling story about life imitating art
two parallel narratives bleed into one another in a deeply irrational manner blurring the boundaries between theatre and film antique and postmodern actor and character the living and the dead
Evocatively shot and earns a few winces of discomfort on its way to a lurid finale twist What the picture fails to accomplish however is giving the viewer someone to care about relate to or understand
Instead of relying on frenetic insanity and buckets of viscera this movie sets an uneasy tone where tension slowly crawls to its peak
Over Your Dead Body will certainly appeal to Miikes hardcore fan base for his technical mastery of blood is on display throughout
The Visit seems aware that its core audience considers this kind of movie like a fairground ride Shrieks of audience terror are usually followed by laughter anyway and the two emotions arent always that different
However the movie goes on to explain these alternately alarming and comic phenomena the kids spend some time trying to sort it out and their process is simultaneously predictable and clumsy
The best Shyamalan film since The Village Read full review in Spanish
While its great to see M Night Shyamalan return to the twisty horror genre his use of foundfootage leaves the film feeling like a decent premise with nowhere to go
Effectively modulated balancing character exposition with pointed dialogue while injecting the plots progression with slow but gradual increments of terrifying uncertainty
An unfulfilled promise Full review in Spanish
The Visit marks Shyamalans best horror effort since s The Village and one can only hope that this marks the start of a comeback for the oncereliable filmmaker
an exemplary cast the script is keenly aware of not only our horror expectations but our Shyamalan expectations too using it against us in the best possible way
Stop the presses M Night Shyamalan has made a film that doesnt suck
M Night Shyamalans first foray into foundfootage territory conjures up a few scares but otherwise operates on autopilot
The Visit is a pleasant surprise
A movie that doesnt know if its a horror film or a comedy but its a good hybrid that we can be sure will captivate us and entertain us Full review in Spanish
Its brutally creepy funny and packs a meaningful message about forgiveness
The twist is that The Visit reminds you that M Night Shyamalan has a remarkable mind for what frightens us
With The Visit Shyamalan has delivered a delicious horror gem so intense that you may accidentally rip the armrests off your seat from clenching them so hard
Dont go in expecting some big Sixth Sensestyle twist but The Visit does provide a satisfying blend of thrills and laughs so if youre into spooky movies its worth a watch
Welcome to M Night Shyamaland Whats that The new one Well theres creepy stuff a Hansel and Gretel bit grandma gets loony the ride gets a bit Psycho with a kitchen knife and a basement andwell the last sections a bit rickety really
Though it may not be all that scary it gets brownie points for being strange
With The Visit M Night Shyamalan might well be coming out of his year sophomore slump
There are a lot of both the good and the bad Shyamalanisms in this film to both support the argument that this is a good comeback and to show that he isnt the filmmaker he was  years ago
The films very legitimate concerns were more expertly explored in a  series by Brandon Loomis for the Arizona Republic
Not bad for a brazenly derivative timekilling knockoff
It almost requires belief in magic  and high tolerance for hokum  to buy this whopper of an action picture But heres where good acting builds empathy and defeats cynicism
Heist like the kids game Mouse Trap has plenty of moving parts but a simple set up And like a lot of criminals it thinks and acts like its smarter than it is
Heist goes from lousy to even worse The screenplay is so clunky not a single cast member manages to sound believable Familiar likable actors from Kate Bosworth to Gina Carano to Morris Chestnut are buried under an avalanche of awful
It combines elements of classic heist movies but a poor script leaves it far from being a good addition to the genre Full review in Spanish
Heist hangs together almost entirely on Morgans charm and dogged determination
Caught between genre potboiler and wouldbe human drama Heist doesnt have a mark to hit But it leaves some good impressions
Whatever its flaws Heist is to be commended for repeatedly finding ways to distract us from them
In the race to make the greatest number of crappy movies Nicolas Cage has been De Niros strongest competition Now De Niro pulls way ahead
As vapidly generic as its title British director Scott Manns Heist is a bythenumbers crime thriller that squanders a decent cast including Robert De Niro Jeffrey Dean Morgan and Dave Bautista
As throwbacks go its more bearable than shoulder pads
Everything is dully routine with characters much too composed in volatile situations to be believed
Another dull DTVcaliber actioner with a depressingly good cast considering the material
Applying the brakes to detail worry only reinforces flimsy screenwriting and iffy casting losing the movies appeal as it struggles to build a more dramatically sound offering of complete nonsense
The film sets out to do nothing and I think it does nothing very well either way you read this is fine
The heist in Heist is pretty pedestrian and the film turns into Die Hardonabus with a couple of soso twists and serviceable spasms of action If thats what youre looking for rent Speed instead
Unexpected fast and well acted are the three terms which Id use to describe this adaptation from director Scott Mann Full review in Spanish
Echoes of better movies abound in Stephen Cyrus Sepher and Max Adams screenplay mixed with an illogic entirely their own
Despite a highconcept idea and a good cast this crime thriller feels somewhat halfbaked and rushed It might once have been promising but it seems like something went wrong along the way
more tedious than exciting as it leads to a preposterous climax
An uninspired mixture of slapstick comedy and sentimentality A Walk In The Woods gets by on the charisma of the two stars but there are times when it feels stretched to breaking point
Plunders laughoutloud scenes from the book  getting kitted out with ludicrously expensive gear becoming obsessed with bear attacks  and turns them into deadening Grumpy Old MenVictor Meldrewish moments of bewildered outrage
Robert Redford and Nick Nolte are fine actors yet even they cannot save this one note story of a long journey to nowhere
Its not heavy its not wildly significant But A Walk in the Woods has a redemptive tone a gentle lesson about aging and limitations and never giving up despite any required climb uphill or otherwise
Nolte finds real sympathy for Katz and delivers a compellingly watchable performance even if youre slightly afraid hell keel over at any moment
In the lightest movie ever made by either one Nolte and Redford barely pull this one out of Lake Woebegone territory
It hits its stride now and then and mostly because of Nick Nolte
While you might find yourself musing how much more interesting it might have been to watch Emma Thompson walking in the woods for a couple of hours you might also be distracted by the films presentation of the Appalachian Trail per se
Echoing his witty writing style Bill Brysons memoir of his trek up the Appalachian Trail is adapted as a gently amusing comedy that combines big landscapes with sharp observational humour
Redford and Notle are a great comedic couple because of their respective on screen personas Full review in Spanish
Most of the jokes revolve around the age of the characters and an over dependence on slapstick humor make this a rocky and slippery hike of a movie Full review in Spanish
Unfortunately its shenanigans end up being tiresome instead of charming Full Review in Spanish
Man Im such a sucker for movies about grumpy old men taking trips
A little more pace and purpose might have prevented this being so pedestrian
An enjoyable comedy for mature audiences with some really hillarious moments Full review in Spanish
A fun and simple comedy that works due to Robert Redfords and Nick Noltes charisma Full review in Spanish
The premise of bickering old friends forced together while out of their comfort zones has been done before and far better Whitnail and I City Slickers but the two leads star power and easy charm makes this an enjoyable way to spend a couple of hours
The film contains many incidents constant drops of humor and some tearjerker Full review in Spanish
Here Brysons a wizened cipher wrapped in a grizzled grump inside a pickled jerk Patter gets pat wise oldguy talk vies with lame wisecracks and slapstick turns slapschtick This slogs a waste of time and space
Bear jokes poop jokes bear poop jokesthis movies got it all
That none of it makes any sense is not a problem but the failure to raise the temperature despite endless shootouts punchups car chases et al is a fatal flaw
There are few surprises and noone really to warm to
The film is as soulless as Agent  is conforming to all the mediocrethriller cliches the action zips from Salzburg to Berlin to Singapore for next to no coherent reason with little originality
Someone needs to put out a contract on this franchise
So badly based on a video game that with every new level of bland cool or serious silliness youll wish Mr  would scissorjump barrels to blow Donkey Kongs brains out or cartchase Super Marioanything to make this exciting at some bulletpoint
Weve revisited a franchise that failed to get off the ground with its preceding  endeavour  and one that could struggle to do so yet again
It seems like eight short years ago I reviewed a movie called Hitman that was based on a video game Oh wait a minute It was And I did
A good Hitman film should appeal to fans of the source material engage thrillseeking cineastes and at the very least satisfy theatregoers who want to sit in a dark room and be entertained On all of these counts Agent  fails irredeemably
Its really more of a miss than a hit man
The movie is garbage somehow simultaneously boring and irritating
Works pretty well as unintentional amusement whenever it goes completely insane Unfortunately its mostly just inane and thats not much fun
In Hitman Agent  Bach doesnt really offer anything Full review in Spanish
A slick efficient and utterly banal piece of global cinema product
The seduction is his perfection his ability to function without the clutter of emotion Its why we buy magazines like Real Simple and watch Hoarders so we can imagine for one moment that were capable of overcoming our human flaws and messy house
By the final frame youll be grateful its game over
Where HItman was happy to be derivative of the genre movies that inspired the game Agent  takes those inspirations and pushes them to gleefully insane degrees
Hitman Agent  does deliver sleek production values and a standout performance from Hannah Ware Other than that its a nonsensical story that feels as if it was written by a yearold who plays too many video games
Unfortunately while Hitman Agent  may offer a passable diversion for the action fans among us for everyone else its best avoided
The plot is generic in the extreme and the dialogue and acting barely disguises the fact that its really just a series of not very exciting setpieces strung together
Rupert Friend plays an assassin even deadlier than Homelands Peter Quinn But his very indestructibility means the storys formulaic action never gets particularly gripping
With this film the cowboy genre moves beyond modern and postmodern You could call it postmillennial postapocalyptic postironic
Youll be watching  at least I was  with your hand clamped over a mouth in various states of aghastness But the films bloodiest moments in a real turnup for the books are also its saddest
It has a nice line in wry chatter and a pleasantly oldfashioned lost posse plot with engaging odd characters striving against the wilderness while swapping cynical frontier wisdom
Cult status could beckon for this wellmade macabre and violent westernhorror from cinematographer turned director S Craig Zahler
Make no bones about it this is worth a couple of hours of your time
This delivers humour horror bromance and blood in an exquisitely freaky fashion A weird and wonderful Western
S Craig Zahlers ambitious genre bender is Western black comedy and gory horror movie all at once and entirely successfully
It is mostly just an excuse for the debut director S Craig Zahler to whip the rug out from under you Tarantinostyle
Requiring both patience and a castiron stomach Bone Tomahawk is an acquired taste but it stays with you long after its over
a funny strange and haha western landscape where it is all too easy to get pillaged consumed or lost
Wow What a grueling richly rewarding and meticulously crafted film
A radical fusion of disparate elements which all enhance one another beautifully
If you like flinty offbeat Westerns flavoursome dialogue and expert acting then theres a fair chance you will relish Bone Tomahawk But be warned this slow absorbing tale eventually turns savage and requires a castiron stomach
As entertaining as it is to watch Russell and co it also strains the patience The killcrazy ending does make up for it though
Unlike great westerns that have come before it Bone Tomahawk isnt one that takes its time to develop its character and story
Ultraviolence aside theres plenty to admire about this innovative genrebender
Although not wholly successful Bone Tomahawk is nevertheless the latest example of the quiet renaissance that Westerns have enjoyed over the past few years
A perfect blend of horror and western elements Full review in Spanish
Bone Tomahawk makes its characters shine through a smart script staying away from the traditional and often used formula Full review in Spanish
a sturdy frontier western about strong but decidedly mortal settlers who take the responsibilities of community seriously and its odyssey takes them to the border of horror cinema without leaving its frontier drama landscape or sensibility
Halfway through The Perfect Guy flips from bland romance to a weak womaninperil suspenser
I cant say with absolute authority that The Perfect Guy is the most unimaginative stalkerboyfriend movie in recent years Ive got a lot of Lifetime movies taking up space on my DVR but man it sure feels that way
A reasonably engaging thriller with some topical overtones
Predictable thriller wastes strong cast some sex violence
Derivatively written and has a laughable villain but its success still needs to be celebrated
The unsure tone screams that this is a director embarrassed by the film hes making
Only Lathans earnestness holds the film together  shes so good while being given so little to work with its a crime that she isnt headlining better films than this one
An enragingly stupid and obvious thriller jammed with dull genre clichs wild hypocrisy and just a hint of victim blaming
The Perfect Guy manages to deliver a handful of tawdry thrills and remains solidly watchable thanks to its classy cast but it lacks the ambition to rise above generic thriller boxticking
The beats of the character are as follows lovely lovely lovely satanic satanic satanic
A checklist of s thriller cliches so tired you can almost see the cast yawning get trotted out with a multitude of gloss yet zero creativity in this disappointingly straitlaced Fatal Attraction knockoff
The Perfect Guy is a lazy retread from the Fatal Attraction department of stalker movies
Little more than an NRA promotional video targeted at AfricanAmericans
A superficial yet effective timekiller
Lathan Ealy and Chestnut do their best with the poorlywritten script but even their experienced acting chops fail to save the copycat thriller
The Bmovie riffs are efficiently executed but the biggest mystery is why this supporting feature should be troubling our cinema screens when its target audience is firmly nestled on the sofa
The Perfect Guy might be high melodrama    yet despite its faults it dresses nicely wears good cologne and has excellent table manners
A paintbynumbers stalker thriller a tad too predictable for this critics taste
So very earnest in its incompetence that its kind of sweet
Glossy but blandly directed stalker thriller The Perfect Guy fails to do anything interesting with its stock setup and theres no erotic frisson between the characters Sadly a misted shower door is as steamy as things get here
A shallow selfimportant biopic
You know a movies something special when it succeeds at generating tension and excitement from a game in which two people do little more than stare at a board and move pieces of wood And does it ever
A call to speak out against the neglect of those with untreated mental illness
Its ironic that a player as unconventional as Fischer should inspire such a thoroughly conventional biopic Still plain professionalism has its upside if less spectacularly so than genius
too literal and heavyhanded in its treatment and Bobby emerges as yet another tortured genius who fell prey to his own demons as if there is no other alternative
Landing somewhere between arthouse and solid mainstream entertainment Pawn Sacrifice probably owes its arthouse cred to its cast subject matter and screenwriter Steven Knight
Like all of Zwicks works its perfectly watchable fare but its often infuriating for its refusal to dig deeper into its incredibly compelling subject
Pawn Sacrifice has all the pieces in place ha ha and yet its missing that intangible spark necessary to elevate a biopic beyond the realm of dramatic reenactment
Yawningly dull Cold War chess drama squanders the charms and talents of Tobey Maguire as Bobby Fischer and Liev Schreiber as Boris Spassky
In the end however it seems Fischer is just a onetrick pony Apart from chess Fischer seems stunted as a human being It is an interesting tale and a sad one about the thin line between genius and madness
The most engrossing sports tournament Ive seen onscreen in years
A sports biopic with all the cliches anachronisms and sentimentalisms in the genre but has good acting and directing Full review in Spanish
The visuals and acting create an agile rhythm and compliment one another helped by the dynamic editing done by Steve Rosenblumthel accents work in its favor by giving it some historical context Full Review on Spanish
A movie thats more about the spectacle of chess than the life story of the greatest chess player in history Full review in Spanish
A portrait of the master chess player and world champion Bobby Fischer during his Game of the Century against B Spassky that falls short in its attempt to capture the singular epic feeling of the match Full review in Spanish
A movie about victory loss paranoia and tolerance It also includes one of the best performances ever by Maguire Full review in Spanish
An enjoyable film with great moments of suspense Full review in Spanish
An Impeccably produced film with an interesting story that handles well the suspense Full review in Spanish
The story of an extraordinary triumph and accurate portrayal of the Cold War era remarkably well produced and told Full review in Spanish
A passionate and humanizing film Full review in Spanish
Written by Bobby Lee Darby and Nathan Brookes the new film is a sequel to  Rounds in name only
The  Rounds franchise never used its central idea to the fullest potential but removing it completely wasnt the answer either
While children may be spellbound by the amazing special effects and outrageous events lovers of Peter Pan may want to quickly return to the originals simpler charms
Wright stages some very colourful setpieces but the filmmaking never takes wing It doesnt help that the performances are as broad as any you will find in a pantomime at the Hackney Empire
Although not without problems Pan is worth seeing because of its amazing special effects impressive cinematography and imaginative production design
For the most part this is as flimsy as Tinkerbells wings big on gossamer sparkle low on substance
Pan is fastpaced and visually impressive
While the special effects are as good as anything youll see in a cinema this year some of the performances are endofthepier awful and the storylines a crib sheet of at least half a dozen much better fantasy flicks
A deeply disappointing film from an incredibly talented filmmaker It sometimes works More often it doesnt Its mostly watchable but its a lot of fuss over not much
Pan just peters out
Loud and lumbering its seems like a transparent attempt to launch a franchise based more on financial than creative rationale
Full of amazing effects and riding a fine overthetop performance by Hugh Jackman Pan is sure to be a hit with the  crowd
A crazy colourful adventure anchored by a delightfully devilish turn from Jackman
Joe Wrights Pan is lacking in the fun excitement and magic that made earlier adaptations of Peter Pan so endearing and memorable leaving behind a troubled prequel that even kids will more than likely find to be a tremendous bore
Even with its excellent special effects its a shame to see Wright and co completely turn Peter Pan in to a flat and boring prequel we never asked for in the first place
A soulless special effects extravaganza Quite possibly the least pleasurable Peter Pan story in any format ever
All the key players in this movie are unquestionably talented but theyre part of something that feels like a blatant and somewhat desperate attempt to be something its not a movie that captures the imagination and ignites a franchise
The fantasy adventure invites our senses on an awfully big adventure but lands with a dull thud where it matters most our hearts
A postmodern splishsplash this film ziplines here there everywhere The movies feel skids from circustent carnivalesque to amusementpark giddiness Never boring Pans also never sure what it wants to be
The story is clunky and too many of its details jar with Barries original Didnt Captain Hook go to Eton How come hes suddenly become an American cowboy And given that he still has both hands why on earth is he already called Hook
Wrights airy sensibility regarding the material melds with the artsandcrafts production design to create an earnestly fun brightly colored joyride
Joe Wright creates feelings of pure joy and wonder in the audience as he takes our hand and flies us over the surreal landscape of a world that does not exist
Sebastian Silvas Nasty Baby is a sweet story that becomes enthralling as it takes an unexpected twisted act  turn
Nasty Baby rights itself intriguingly when Silva pushes his characters into unknown territory and lifestyle is imposed upon by life
Nasty Baby takes aim at a fat target gay modernfamily dramedies those wellmeaning selfcongratulatory films that havent actually felt modern in a decade
Silvas screenplay has a sliceoflife feel Sergio Armstrongs cinematography provides the natural lighting and handheld camerawork that adds to that sensation
The end slams the brakes on any goodwill you had for the characters
The real shock is how a lowkey indie drama becomes so broad and overheated hinging partly on a dumb joke and becoming one itself
Sebastin Silvas Nasty Baby declares its buttonpushing intentions right on its inyourface title screen but it actually ends up having a surprising amount of heart
Ultimately Silvas uneven command of tone undoes whatever goodwill his actors have managed to generate They  and we  deserve much better than this
An awkward scene between the artist and a local gallery director highlights Silvas fluency in the rituals of social superiority
Offbeat drama about modern babymaking turns dark and bloody
an engrossing film that is as original as it is thoughtprovoking in how it will challenge your complacency
This unusual wellacted drama disappointed me with its shocking turn to the dark side during the last part of the movie
Its difficult cinema absolutely so but like the most interesting of the difficult movies its an eminently memorable watch
Dramatically undercooked and shot in a way that makes the whole thing feel random and amateurish
Gentrifying neighborhoods are a timely target for satire  pitchperfect at capturing   worst pettiness condescension and isolation  in their brave new PC world
as in his criminally underseen Magic Magic there is a strong sense of foreboding all along Silva littering his story with clues that this just may be an unholy alliance
After the offkilter weirdness of Magic Magic and Crystal Fairy  The Magical Cactus the Chilean director returns to the darkshaded humanity of his breakout hit The Maid Nasty Baby is his most satisfying work to date
Timely social satire of the most scathing and cynical kind
Like many Rivette films Nasty Baby is an enigmatic comedydrama set in a bohemian subculture with a plot involving the creation of a work of art
Its character study and performances are accurate and compelling bringing the desired critique successfully consummate and credible Full Review in Spanish
By far the most slickly produced and insistently evangelical movie yet from the sibling team of Alex and Stephen Kendrick
Occasionally preachy and oversimplified yet captivating tender and heartfelt It can lead to many interesting discussions if you open your mind and heart to it and forgive its preachiness Karen Abercrombie gives an emotionally radiant performance
The message is strong but the movie is weak
Basically the love child of Kirk Cameron and The Room
Its a sermon about the power of prayer
Spirituality can be a beautiful thing to explore in cinema but War Room has no interest in engaging its audience on a personal level
One of the more entertaining and relatable faithbased films to hit the big screen in recent times
Badlyacted poorly written a hackneyed sermon that barely preaches to the choir
Another strong inspirational cinematic experience from the Kendrick brothers
Faithbased drama deals with marital discord infidelity
Extraordinarily powerful and box office success
The faithbased audience will respond to the movies sunny squeakyclean look courtesy of cinematographer Bob M Scott and the fervent sincerity of the performances Let others be warned the sermonizing is nonstop
Its clear that the film though proselytizing only at itself is at least savvy enough to realize that itself doesnt necessarily look like Mike Huckabee or Pat Robertson
War Room is the most slickly made faithbased film Ive seen yet in terms of production values but that doesnt make it quoteunquote good per se
An affluent AfricanAmerican family is going through some domestic issues Husband loses job daughter is being ignored mother has a footodour situation  the usual The answer Submit to a resurrected carpenter
Kendricks view of Christian devotion involves only trivial sacrifices and offers a gospel of selfhelp that masks its wider doctrinal implications
Despite a few fumbles this moving film about the power of prayer still packs quite a punch
This is no mere entertainment Its an instructional video
If faithbased films want to be taken seriously as cinema they deserve to be held to the same standards as any other feature
Slick production values cannot overcome a preachy script full of strained metaphors delivered by wooden actors Like a corporate promo video for God
Perrys movie may not be easy to watch but that doesnt mean you shouldnt
Its that tension  will Catherine really go off the rails and how far will she go when she does  that makes Queen of Earth such a transfixing experience
Perrys gracefully disturbing execution conveys a tangible sense of the emotional violence and attrition that can underlie seemingly placid social surfaces
The film would be nothing but a hollow exercise if its hollowness werent so hauntingly precise
A good try at a difficult story but a lack of character development leaves us in a vacuum
Queen of Earth isnt a laugh riot not by a long shot But it is a good movie and a fascinating study of descent
Well made written acted and crafted its the sort of movie that doesnt go down easy takes no prisoners and leaves you hoping your own life will never resemble theirs
If youre gonna go hard to the closeup youre gonna want to work with a face like Elisabeth Mosss
throws acid on its offputting characters and their hopeless situations and rubs moviegoers faces in it rather than offering any substantial insight or depth
Queen of Earth is one of the least recommendable films of the year This is a film that chooses to ride a tidal wave of agitation and bitterness instead of portraying any sort of amusing or enticing qualities
Its effects will stick with you even if you dont quite understand them
Until the final act the tedium is somewhat overbearing
Its amazing that a movie about such a dark subject can play so thrillingly and vibrantly
Its a horror movie you need to watch through splayed fingers not because of anything frightening on screen but because of its awkwardness
Queen of Earthsurrounds Elisabeth Mosss mesmerizing performance with an actual movie a lowkey homage to Eurohorror films  complete with an appropriately thrumming and discordant score
Moss is a formidable force of twisted nature At once vulnerable and acerbic chaotic and singleminded she renders Catherines pain palpable even as she staunchly refuses to pander to the audiences sympathies
Perrys most impeccably constructed and executed yet
Elizabeth Moss gives a scorchingly downtoearth performance as Catherine a woman on the cusp of a colossal mental breakdown
A sensually photographed and intriguing tart psychological thriller that sets an eerie mood tone
What should be a weekend of bestie comfort becomes fraught with expectations assumptions and resentment
Its all completely overthetop and loaded with cheese and the mix of comedy arch melodrama sweeping CGI vistas and plentiful if improbable action doesnt quite gel
Unfortunately the first casualty is good acting which dies a noisy overly dramatic death
The least interesting question to ask about a movie like Dragon Blade is whether its any good Of course it isnt not especially but questions of quality pale next to the greater headscratcher What is it
People are burned alive crushed like insects hurled from rooftops They may not deserve all this But neither do we
Dragon Blade is a solidly constructed epic that despite its many battle scenes has a strong message of pacifism and unity
Its kind of a mess That more than  minutes have been trimmed for the stateside release may have hurt the films coherence but viewers will be thankful for the shorter sit
The whole plot is incoherent and its depicted almost entirely through montages and crossfades
In Dragon Blade East meets West in a murky mediocre muddle
Just about everyone seems to get slashed carved up or skewered and vengeance rains down upon all Except of course the one villain who truly deserved it The filmmaker
For those who ever wondered what it would be like to see John Cusack and Jackie Chan engage in a swordfight both of you here is Dragon Blade
Not an ideal Chan vehicle but on the other hand it gains a lot from his ecstatic presence
This Chinese battle epic features exciting showstopping fight scenes but the storytelling in between is almost nonexistent as if lazily looking for ways to kill the time
The film barely passes the entertainment mustard
Epic in scope is Dragon Blade  but not the best film Chan has made
Cusack does a good impression of an estate agent having a midlife crisis while Brody adds to his gallery of eyerollingly awful performances by going full Tony Montana
This generic offering simply doesnt transcend the tropes of the genre at hand
There are nice moments but this is pure bombast and Chans natural action flair is not given free rein
Chan brings martial artistry to the fight Cusack his siege skills and the the film overall is a mishmash of languages styles and tones
Handsome production design and sweeping battle vistas lend a touch of class with Chan directing the action scenes with trademark brio
Dragon Blade offers a daftly entertaining throwback to historical epics of the s
An intense drama with a great cast and an impeccable production Full review in Spanish
Johnny Depp gives an impressive performance that proves that when he wants to he can be one of the greatest actors in the business today Full review in Spanish
If it wasnt for Johnny Depps brilliant performance this would be just another forgettable gangster flick Full review in Spanish
Theres plenty here we have seen before and seen done better
A pacey involving thriller that hinges on a disciplined turn from Depp
All praise to Black Mass
An enjoyable if occasionally derivative crime drama thats elevated by a careerbest performance from Johnny Depp
A crude testimony of the life of a criminal that shows us that it doesnt matter what you do what matter is how and when you do it and who sees you doing it Full review in Spanish
Depps performance as Bulger goes far deeper than sweptback thinning hair and leathery reptilian skin and delivers a soulchilling study of sociopathic evil
Packs a nasty gangland punch but lacks the Corleone clout necessary to back up its muchdiscussed bloodandhonour themes
Depp has never been short on menace and he is so unpleasant as Bulger that his usual gangster charm never kicks in leaving the audience cold
Black Mass moves like a heavyweight and when Cooper lands his punches they hurt I cant imagine its going to do much for the South Boston tourist trade though
The tale torn loyalties and brazen betrayals make for a solid thriller
Bulger was a cruel killer and an ally of Irish Republican fascism but its not all bad he gave us Johnny Depp back
as reallife mobster James Whitey Bulger Depp may have found his greatest performance
For a biopic of a reallife person this feels like an oddly standard mob thriller Its the true story of Boston gangster James Whitey Bulger and its told with gritty filmmaking and robust performances
Strip away Depps theatrics and Black Mass is a handsomely crafted if overly familiar tale of crime and punishment that faintly echoes Martin Scorseses Oscarwinning drama The Departed
Here Depp is controlled measured and deeply unsettling
A solid straightforward if not particularly memorable addition to a genre that has seen a number of illustrious contributions in recent years
Black Mass is all about Bulger But thankfully that means its all about Depp And you wont be able to tear your eyes from him  even when you want to
Comic animated robot tale has heavy action crude humor
While the latest vehicle for Tom Cruise is taut and fastpaced the spectacle too often trumps the substance
Its sort of a combination of the Bourne films James Bond and a Road Runner short
If you can stomach every unbelievable conceit that comes standard in a spythrilleraction picture then Rogue Nation is a pretty good time
Taking in luxurious locations scintillating set pieces and nailbiting tension Rogue Nation maintains the Mission form guide  and leaves you wondering what other methods of neardeath risktaking Cruise has left up his sleeve
In Mission Impossible  Rogue Nation the obvious comfort level between star and director pays off nicely
Tom Cruise lends the character an intensity and bug eyed gutsiness that make him a hero you want to root for
No one should come to a Mission Impossible movie for anything but those set pieces and director Christopher McQuarrie delivers some rich ones this time around with strong assistance from cinematographer Robert Elswit
Thankfully watching Tom Cruise do all of this is still great fun
There was probably more spectacular action in the last film but this one is just as much fun and could feasibly make a star of yearold Ferguson who steals scenes while everyone else is trying to steal memory sticks
Rogue Nation is terrific fun the best action movie of the summer without Mad Max in the title Its also arguably the best Mission Impossible movie impressive for the fifth installment in any movie series
Ultimately its another utterly gripping entry into a franchise that has really found its feet dangling from the outside of a plane
Rogue Nation could be the best Mission Impossible film
With Mission Impossible  Rogue Nation were getting the best Bond movie since Casino Royale in
Mission Impossible Rogue Nation lives up to the franchise name presenting us with a successful outcome to the seemingly impossible mission of producing a truly excellent summertime action flick
Could there be a more surprisingly pleasing spy franchise than this action adventure series It doesnt allow James Bond or Jason Bourne to take all the accolades
Perhaps its a shade lighter on the mindwarping trickerywithinchicanery effects seen in previous Missions but on the other hand the interplay between the characters achieves a lightness of touch rare in your average blockbuster
Tightly paced ambitiously conceived superbly executed and greatly enriched by the instincts and wits of its producerstar Tom Cruise
Exciting intelligent and well filmed Full review in Spanish
Sure youll lose count of the well thats not very realistic moments but thats never mattered in a Mission Impossible film and it certainly doesnt in this one
As brisk as a Mission Impossible movie has ever been
Feels like a long trailer for the sequel that may never come Full review in Spanish
This one starts off better than the previous films with a reasonably intelligent script sprightly direction by Trank a great comicbook noir look and pleasant rapport among the stars
An epic fail in Hollywoods comic book movie universe this notsofantastic flop is so bad it could send the whole genre on a downward spiral What a mess
The film drains the spark from four talented young actors with clunky dialogue and a crushingly dull narrative The special effects arent that special either
Ultimately Fantastic Four comes off as a laboured setup to a sequel thats predictably already in the works That of course raises another mammoth question who the hell will want to see it
Creio j ser hora de o Cinema desistir de levar o Quarteto Fantstico para as telonas
An aboveaverage comicbook adaptation
Until the special effects take over in the final act this is an unusually gritty grounded superhero thriller with characters who are so believable that the wacky science almost seems to make sense
This muchreviled reboot represents a legitimate attempt to imagine a superhero origin story as a cautionary sciencefictionhorror yarn of the type The Fly First Man into Space that likely influenced Lee and Kirby in the first place
Maybe its time to just accept that these characters will simply never work on film
There are so many things wrong with Fantastic Four    that you could never squeeze them all into a conventional film review
For now the only big screen on which Fantastic Four deserves to be seen is the one in your living room on a Saturday afternoon on Syfy
I cant help but feel that this was another type of film but someone put his hands on it and it all went to hell Full review in Spanish
features subpar visual effects and onedimensional characters along with a story that lacks emotional depth or any meaningful subtext leading to the obligatory trumpedup finale
Despite the best efforts of its talented cast this joins Terminator Genisys as this summers big disappointments Far from fantastic
Two distinctly different approaches to filmmaking are employed here and neither of them manages to eke out a victory over the other
A jumble of predictable but also incoherent plot turns dreadful dialogue and unfortunate visual choices
Laboriously reworks one of the most wellknown origin stories in superhero comics
Though undeniably ambitious in spots this beloved Marvel property  as produced by Fox  feels stitched together and disengaged
Its a real shame that a likable ensemble has been squandered in this reboot It feels like these four leads couldve been fantastic but in reality theyre a pretty unremarkable bunch
As funny as the first film though a little slow towards the end Ted  is everything you were probably expecting and more
Perde o pouco flego que tinha j na metade da projeo
Some parts are funny but overall its a forgettable movie that doesnt hold a candle to the first one Full review in Spanish
A surprising smart comedy loved its politically incorrect humor and cameos Full review in Spanish
One of the worst movies of  Full review in Spanish
This soft squidgy sequel squanders its comic notquitecrappiness by padding out its length and getting seriously dull in its exploration of a talking bears personhood Paddington versus Descartes anyone I thought not therefore I zzzzzzzz
Ted  is nothing new but its entertaining and a safe bet Full review in Spanish
Ted  is funnier and has a better story than the first one Full review in Spanish
Even for those who enjoyed the first film the novelty has worn off by now
At least theres a joke about how Amanda Seyfried looks a bit like Gollum Take your victories and all that
Ted  is best enjoyed entering into the cinema not really thinking much about what is going to happen allowing you to think only about the possible repercussions when everything is done and dusted
Being awful is still no substitute for being funny  but Seth MacFarlanes talking teddy bear sequel does manage some laughs anyway
Ted  isnt any good but people will go to see it because like a childrens toy says the fword
Ted  works best when it hews closely to Family Guys rhythms of fast dialogue and cutaways to quick selfcontained skits
If you liked the first Ted youre going to really enjoy Ted
Director and Ted voicer Seth MacFarlanes followup to s Ted has plenty of his trademark crude humour but it all feels rather desperate and silly
This is a belowaverage sequel that feels like a watereddown version of the first film
It should go without saying everybody with a heart and soul deserves to be treated equally Sometimes it takes a cursing flatulent potsmoking jokecracking teddy bear to teach us common sense
This will not be for everyone  but if you loved the foulmouthed bear first time round he just got even filthier
The whole devolves into an overlong Family Guy episode chock full of random asides weird cameos and goofy homage
Its an interesting choice but it isnt handled in a compelling way
Documentarylike in execution Fat doesnt hold back as Ken reveals both his body and the most intimate details of his life
A dark film about shame and selfsabotage Fat is not a pretty picture The truthful ones rarely are
Rodriguez just slays this role
There is fearlessness here and uncomfortable raw honesty but theres also little opportunity to care about a man who pushes everyone including us away
Without being innovative or particulary revealing this documentary appeals to be empathic with the audience being very simple in its narrative Full review in Spanish
Guggenheim takes a wholly admiring view of Malala and her family and you wouldnt want it any other way
He Named Me Malala will certainly make you think while making you smile and thats something any film can aspire to
Where the film glows is its depiction of Malalas relationship with her Dad Ziauddin himself an inspiring influential and brave activist He named her Malala after an important folk heroine and together they are something to behold
The best thing about the film is its truly heroic Pakistani teenager heroine and the few unrehearsed glimpses we get of her
Davis turns his documentary a celebration of free thought knowledge integrity and the fight for freedom all of which the world needs so much right now Full Review in Spanish
An interesting and touching story that benefits from a fresh narrative Full review in Spanish
The directors critic eye is a key aspect of this great documentary Full review in Spanish
The film follows an idealized version of Malala and misses the opportunity of exploring lesser known aspects of her already well know and publicized story Full review in Spanish
A rousing profile of the teenage Pakistani girl the Taliban tried to kill for her belief in education and equalityone of the unintended pleasures is registering how Malala has become the worst possible nightmare to her failed murderers
Oscarwinning director Davis Guggenheim An Inconvenient Truth creates a riveting portrait of the youngest ever Nobel Peace Prize winner Malala Yousafzai
He Named Me Malala is a deeply touching story of a proud father and daughter who have drawn strength from each other in times of unimaginable pain and adversity
He Named Me Malala tells an important and engaging story
Its a shame that Guggenheims slickly produced documentary examines such an important and fascinating story with such underwhelming results
The most gratifying takeaway from He Named Me Malala is how ordinary Malala is shown to be when she isnt lobbying the United Nations and visiting beleaguered countries
It ends with a hashtag because it knows its target audience would rather be pandered to with easychair pleasantries
Its as digestible as a book report but not probing
Guggenheims portrait of teenage Nobel Peace Prize laureate Malala Yousafzai is remarkable in terms of access to its subjects life
It has the feel of a handsome classroom study aid but nonetheless one every child from nine to  could learn from
Davis Guggenheims solid and moving documentary He Named Me Malala gives us the background of a story that shocked the world but could have been more rigorously made
We need more than fumbling clichs and Slow Learners doesnt deliver
Only a few scenes fail to draw laughs in a movie thats unexpectedly smart and consistently amusing
A winsomely appealing opus that has enough funny lines courtesy of screenwriter Matt Serword situations and feckless loser characters to divert
Even if this movie isnt fresh its often amusing
Its amusing by any standard  the offbeat story of two nerdy lonelyhearts who decide to spend a summer on the wild side
A satirical romantic comedy that gets mixed grades in the laughter department
Adam Pally The Mindy Project and Sarah Burns Enlightened are good company and wellversed in dual deadpan delivery
The films brisk energy witty visuals and sharp supporting cast including Kevin Dunn and Saturday Night Lives Bobby Moynihan together push an appealing but thin story idea over the finish line
Although it is difficult to believe that the people on screen would actually do the things they do the movie is undeniably funny
While its true that many of the scenes would be hard to perform without breaking into laughter its often difficult to share in the mirth Call it hermetically sealed humor
Cute and funny and certainly worth Netflixing especially on Valentines Day
A very cerebral and complex thriller that succeeds in creating a spooky and very ominous villain who is center stage in a truly scary story
It is a handsomelooking film though it has a promo look to it occasionally like a lavish tourist ad
A fascinating insight into a quite bonkers sporting event
Palio is a propulsive documentary about the horse races held every summer in the picturesque Tuscan town of Siena and the frenzied passions they provoke
Wonderfully vivid
To the uninitiated Sienas Palio is simply a horse race in fancy dress Spenders fascinating documentary gives us an insiders view of the passions that sustain the eightcenturiesold contest and the devious scheming that goes on behind the scenes
Spender assembles a good mix of veterans who fill us in on the changing nature of the Palio particularly as it has become more of a professional event ever the last  years or so
Cosima Spenders documentary plays like a classic sports drama thanks to its memorable central characters
A rare kind of documentary  muscular and refined and a splendour for the eyes
If the film is understandably intoxicated by the multicoloured spectacle on offer its under no illusions as to the insidious corruption and insane brutality unleashed on the path to the prize
While Spender spends enough time with both new and retired jockey legends to collect a gold mine of macho bullheaded rapport you wish she delved deeper into the more sinister behindthescenes wheelings and dealings
Despite its oversights the film  shot and scored beautifully  is an enthusiastic introduction to this delirious event and its peposo of passion style and intrigue
As dramatically satisfying as the most crowdpleasing scripted sports saga
Racing enthusiasts will be satisfied with the time spent in the stables and on the track though I kept waiting for the film to push deeper into its corruption angle
A fascinating documentary
Palio is an exciting documentary though Id have liked it more if it had paid some attention to those who have levelled charges of animal cruelty against the race If it stops short of being a tourist promotion its only by a nose
Seems more like a Lifetime Network movie than something designed for theatrical release
Should be quietly euthanised
Its overly busy and seems to mask that theres something lacking Grenier anchors with a nuanced lead performance but for such a starry cast the cinematic quality doesnt match up to the talent on screen
A richly emotional characterdriven drama
Rather oddly titled for whats basically a sentimental family pic Sex Death and Bowling mixes numerous formulaic overfamiliar elements that never quite gel
Attempting a complex tone combining melodrama and quirk its moderately successful on a few fronts
What starts out as a freak show ends up a poignant tale of tragedy redemption and the boon of taxidermy
You might not think that a documentary about two men fighting over a severed leg would be funny touching and insightful but it is
If youre in the mood for a tragicomic documentary the startlingly original Finders Keepers fills the bill
Carberry and Tweel never condescend to their hellaSouthern subjects And thats why Finders Keepers gives them a leg to stand on
Carberry and Tweel turn this odd story into something surprisingly touching about and compassionate toward these people
All totaled the documentary about inequality entitlement male success and the struggle to feel significant in America is entertaining and insightful
A jawdropping oddity
Finders Keepers is that horrible disastrous and massacreinducing train wreck that you cant look away from Its similar to an episode of Jerry Springer if it had more of a cohesive story stronger characters and a massive dose of heart
Finders Keepers isnt as funny as it sounds but it sure is a twisty tale one that garnered international attention
At times the story seems headed toward an expected conclusion but every time it feels like things should be wrapping up some new hurdle arises to be overcome
Filmmakers Bryan Carberry and Clay Tweel over the films quick  minutes find some humanity and pathos in the story which eventually takes an unexpectedly heartwarming turn
It is a strange tale and I have to admit an original one
Somewhere at the intersection of reality TV and a Coen Brothers picture lies Finders Keepers  as peculiar and entertaining a story as youre likely to find
One of those loopy tales that can be squarely filed in the stranger than fiction category
Finders Keepers has the sort of plot that no screenwriter in his or her right mind would ever dream up
Its a complex portrait of class conflict sad family legacies and the dangerous allure of the spotlight
Its a richer portrait than we might expect in an abysmal era of reality TV
Finders Keepers is an entertaining documentary worth your time
The film is a hodgepodge of archive clips and interview material but it just about gels and a host of smaller scale family stories round it out nicely
A film exploring the undoing of two men the strange circumstances that see their lives interconnect and the qualities weve come to value as a society
Weird and honest about the malaise that can settle into driftless adulthood The Strongest Man suggests that the only way to way forward is to accept where we are first
Another stab at that school of comedy in which adult losers acting like particularly dweeby yearolds in nonsensical situations is assumed to be automatically hilarious
The kind of indie doodle of a movie in which several potentially interesting ideas coexist but never quite come together and where supporters will call the narrative freewheeling while naysayers will insist on rambling
Doesnt piece together as an experience just random points of tonal triumph in picture that seems perfectly comfortable adding up to very little
Although Beef and Conan are far from stereotypical the quirkiness and eccentricities ascribed to them by writerdirector Kenny Riches harp on their otherness all the same
Its uneven goofy and personal in all the ways that independent comedy can be both delightful and occasionally befuddling
Like the waves of the ocean its characters occasionally and semifearfully embrace The Strongest Man continually goes in and out connecting and drifting away
Writerdirector Kenny Riches lays on the whimsy pretty thick and his subJarmusch deadpan style inert camera intentionally flat line readings adds another layer of affect to the story
Its definitely an acquired taste but its most helpful to view the film as a fairy tale
Another painstaking portrait of an American man in crisis from writerdirector Oren Moverman
Director Oren Moverman doesnt seem all that interested in the grimy horror of homelessness except insofar as it gives him an opportunity to show Gere in the midst of an identity crisis who am I and how do I fit into the world
is a fun little chase flick
Clearly the subject matter compels himGeres activism is well knownbut this isnt your usual social drama of our cultures down and out
The film captures the alienated state of homelessness so completely that by the end the average citizens strolling by seem foreign
At the risk of shattering its delicate creation Time Out of Mind maintains its detachment and adheres to the confines of Georges meandering life
Social workers and policy makers talk about people falling between the cracks and Time Out of Mind is the view from that space that the system isnt built to get into
Its clear that this is a filmmaker with an eye for telling powerful stories in nuanced detail  even if Time Out of Mind may be a little too nuanced for its own good
It peters out on the home stretch but theres so much here thats impressive
Movermans screenplay boldly rejects conventional plot structure instead showing us Georges life in breaths and snatches Its all soundtracked by the city itself a great clamour of engines and voices disembodied and indistinct
A compassionate and worthwhile work
Rejoice fans of silver foxes and Eighties lovegods A week after Richard Gere scorched the screen as a billionaire morphine addict in The Benefactor hes back and this time hes on camera for nearly every frame
Its a drama about the struggle for selfrespect in the most parlous of circumstances providing an ultimately convincing and affecting glimpse into a situation that few will ever face by choice
The film eventually reels you into its rhythms and the payoff while slight is surprisingly rewarding
Oren Movermans observational drama about the daytoday life of George Richard Gere a homeless man in New York is made with delicacy and insight
Oren Movermans film is entirely reliant on Geres acting qualities which is rather like relying on the Titanics buoyancy qualities
Richard Gere is a quiet knockout in Time Out of Mind the Oren Moverman film that has for some reason remained as below the radar as its invisible to the rest of society anyway central character
Everyone listed in the final credits  especially writer director Oren Moverman  should feel proud of their involvement
Youll share Georges sense of weariness as he searches for a simple nights sleep but more importantly youll believe in the cold world he inhabits and the complex plight of those around him
This is a tough honourably made drama with a poignant performance from Gere
Scorch at least feels more like a featurelength film than The Maze Runner which played like a sizzle reel for another film that would eventually follow it It doesnt shed all the problems of that prior installment but its a start
dumbeddown science fiction
Maze Runner The Scorch Trials delivers more and bigger scifi actionadventure thrills than the original Maze Runner
The Maze Runner saga is not at the level of other YA adaptations like Harry Potter or Hunger Games but it works as light entertainment Full review in Spanish
Focus is lacking  and it may make you like the Gladers miss the comparatively taut Lord of the Flies intensity of the first installment
As derivative as the first movie
If it sounds like there isnt an original thought in The Scorch Trials scripted head youre right But at least you wont be smacking yourself in the head trying to figure out whats going on
This sequel drops the maze and loses its identity The mystery at the heart of the original is replaced with something far more generic here
It all feels like setup for part three The Death Cure due in February
After the rather lacklustre teendystopia adventure The Maze Runner the action continues in this equally gimmicky sequel
Perhaps the next installment will actually provide the real thrills instead of postponing them
Effective quirky and even downright strange this sequel overcomes a bumpy start to become a minor delight of the fall season
did they run out of money andor hoped no one would notice In the meantime can we please get Dylan OBrien a better movie
The virusinfected zombielike Cranks are old hat but Rosa Salazar as a sassmouthed teen survivalist and Giancarlo Esposito as a wily rebel add freshness
Young adult dystopia for dummies
Overall a much improved movie over the first which I also enjoyed
A bettermade worse movie than its nonetooinvolving predecessor
Boilerplates the YAgloomyfuture fad down to an obstaclecourse at the worstever summer camp Plot points are struck with predictable force line after line battles hopelessly with clich camera shots are fired so routinely they feel preprogrammed
Seen in its own right its a perfectly passable wasteland zombieescape teen horror but as part of a whole that is presumably supposed to add up to an allconquering teen phenomenon its hard to see where this episode fits in
Still theres plenty of fun to be had from the fleetfooted zombies and random nods to Mad Max
Annaud uses its ultrarealistic Imax D images to frame excellent filmed theater a well acted compelling yarn
Wolves horses and sheep are the principal players in the movies set pieces which are powerfully staged and tightly edited if sometimes oversold by James Horners bombastic score
Annaud creates a vivid sensory experience that simply doesnt make much of an emotional impact Its all fur and no fangs
Worth seeing just for a spectacular sequence depicting the wolves chasing a herd of prize horses toward a frozen lake during a blinding snowstorm  which appears to have been shot with minimal computergenerated fakery
Among the outstanding set pieces the most spectacular is a battle between the wolf pack and mounted herders trying to control a horse stampede in the middle of a blizzard
If it strives too hard for weighty import Wolf Totem remains an occasionally stirring portrait of Mongolias wolf population
It becomes difficult to separate the natives from their communist masters in terms of their treatment of their natural surroundings
Epic in scope yet at the same time intensely intimate in its handling of its protagonists inner life its a unique wildlife tale that sets a tribe of humans against a majestic pack of wolves
At the films core is a quirky love story between the student and the orphan wolf cub he finds and raises
Annauds adaptation of Jiang Rongs Chinese novel is a visual wonder but a dramatic dud a cutandpaste mess of plot snippets illuminated by the occasional inspired visual conceit
Characters seem carved from a much larger narrative The landscape and painstakingly trained wolves are the true stars
Theres no denying the beauty of the films imagery violent and tender or the emotional power of the final moment in the boyandhisdog love story
Something ends up lost in translation Its aspirations of looking and feeling recognizable end up uncomfortable and embarrassing
Presented in IMAX D there are scenes in this film that are literally breathtaking when they are not capturing or breaking the heart
Though Wolf Totem is notable for being a Chinesescriptedandshot film that actually offers some kind of critique of the communists it nevertheless falls into a fairly wellworn story of an outsider growing to appreciate a culture hes come to change
Annaud and company make a bold environmental statement cloaked in a personal story of one young mans personal growth change and awareness
The fourlegged creatures are more compelling than their twolegged counterparts in this visually stunning D adventure
The greatest aspect of Wolf Totem is the gorgeous sweeping cinematography that captures the landscape in breathtaking aerial shots and crystalclear color The story has its touching moments but dissolves into disjointed melodrama
A true story that manages to seem both mythical and too fantastic to have really happened JeanJacques Annauds epic D Wolf Totem is ultimately a hymn and a plea for ecological harmony
if the film has a true star it is the environment itself which is enormous and daunting dangerous yet serene
Its a good effort by all even if it does fall short of complete success
Marvels signature gigantism is reversed with agreeable results
As fun as you expect from Marvel but really misses Edgar Wright
No other Marvel installment has felt as weighed down by its obligations to the franchise
Like a superpowered heist movie mixing fantastic invention with a tried tested and alltoofamiliar formula
In these playful moments it feels as if were watching a sly spoof of a typical Marvel film but then the film reverts to more predictable blockbuster fare
It functions well as a comedy and light hearted fantasy while channeling the spirit of classic Marvel tropes of the underdog redemption and the construction of an avenger
The same tepid timeworn routine with a few chocolate sprinkles of fun on top  Marvel has become its own antonym
AntMan is a highlyentertaining romp that boasts outstanding special effects and thrilling action sequences turning this tiny hero into a bigscreen success
Rudds humorously selfdeprecating persona works wonders with the material and he gets terrific support from Michael Pea who takes a stereotypical Latino sidekick role and makes it fizz with goodnatured fun
AntMan has its charms but theyre frequently lost beneath a story that feels like a patchwork job Its a fun watch but also a frustrating one
The result is one of Marvels most purely enjoyable movies to date
The best moments satirise the grand action sequences weve become accustomed to in these tentpoles
Even when it looks as if it might drop the ball Marvel scores another touchdown with this fantastically fun film
AntMan marks the culmination of Marvels Phase Two and theyve brought the chapter to a close in likeable style
AntMan is a largely selfcontained breezy hilarious and gorgeous heist film that manages a feat few recent superhero films do It stands up well on its own
This movie proves that fans should wait until a movie is released before passing judgment Based on the behind the scenes drama this should have been a disaster instead the little movie that could Triumphs
Michael Pea is striking that AntMan and makes it clear that is humor not imposed seriousness of the Avengers that will keep us faithful to the superhero genre Full review in Spanish
Delighting with inventive setpieces and an affable lead in Paul Rudd AntMan is eccentric enough to amuse but can often feel watereddown in execution
So AntMan is an admirable but flawed attempt to expand the Marvel canon The script is far from Marvels best and considering the film aims to be light hearted lacks the laughs that bolster so many of these films
Rudd is indeed the right man for the job for at all times he seems at peace with himself wearing a slightly mischievous grin and speaking in soft tones that make you want to pour your heart out to him as if he were your very old friend
The most impressively flavorless movie in many a long age
The Minions movie is almost complete nonsense but its good natured enjoyable nonsense nonetheless
Minions is despicable or maybe its just me
An idea is only as good as its execution and after spending three sessions with these characters one is left with the speculation that some ideas perhaps would have been better left as part of a rough draft on the printed page
Backing up all the funny Minions antics is one of the best soundtracks ever and some very cute musical numbers
Without the clever plotting of the first the presence of the bumbling brilliance of Gru or a walltowall array of ludicrous jokes the film almost succumbs to its own ego
So much heres fairly uninspired lacking depth and detail or not so zippily zany Mostly these underlings just underperform
Minions is a fun backstory for a popular group of sidekicks but it really shouldnt extend beyond that It does a good job with what it has but it wont leave you asking for more
The Minions crave a strong parental figure but as creatures of pure mischief they dont need hugs  and their antics quickly become tiresome
Lightly Miniony fun throughout this Minionish comedy should please fans of general Minionishness who dont mind the goofiest Mininionesque gags
Forget a never ending stream of superheroes or the dredging up of longdead nostalgia properties this is the real deathknell of modern cinema
The overall effect is mildly diverting and almost instantly forgettable
Like many supporting characters the minions were better in that role idiotic foils for Despicable Mes brooding tormented Gru
An incessant assault of mindless mirthful mayhem with enough cultural allusions mixed in to hold the attention of adults too
This third in the animated Despicable Me series finds the familiar balance between villainy and heroism silliness and relevance but it is a bit thinner As characters the Minions have a lot of limitations This is for kids only
Divorced from the bethankfulforfamily homilies that motivate the Despicable Me movies this first solo outing for the lozengeshaped scenestealers improves on its predecessors Its a Looney loopdeloop of nonstop funny noises and sight gags
Some gags might be saucy in a European sort of way but theyre never crass
As it is Minions is one of the summer film highlights and pokes fun at just about everything
This is a hugely enjoyable ride and with Despicable Me  not due until  provides a welcome fix of Minionshaped hilarity to see us through till then
Theres no denying the minions are adorable but theyre strictly for the kids
Beautiful and soulstirring One  Two is a magical and transcending experience about the strength of sibling love
The silent scenes which hold so much power in the first act feel emptier and emptier the closer the conclusion nears
There are simply too many loose ends to distract us and too much empty air in which audiences cant help but poke holes
Disparate influences percolate but never quite cohere in Andrew Droz Palermos first narrative feature One  Two which while atmospheric and beautifully lensed ends up being a touch too elliptical for its own good
Sally Draper leaps to the big screen in this slight sumptuous drama about teleporting teens
The film tries unsuccessfully to walk the same eerie atmospheric trail as The Village by M Night Shyamalan or any number of Stephen King works
One and Two never feels as momentous or as angsty as a good story about moody teenagers should and thats mostly because the film lacks a menacing parental adversary
Gorgeous and naturalistic shots by cinematographer by Autumn Durald speak volumes and the atonal foreboding score by Nathan Halpern creates a sense of dread though they are ultimately squandered in an underdeveloped story
Strong performances across the board render this a frequently involving and moving experience with the endearing chemistry between the screen siblings making their bond wholly believable
It looks beautiful and there is an interesting denouement but it disappoints the buildup just isnt developed enough for it to matter
Its like Terrence Malick were making a superhero origin story The lack of explanation wouldnt be a problem if the ending didnt feel somehow predictable and lethargic but the cast help make this watchable
Every halfhearted plot twist and supposedly startling jolt underlines just how feeble and derivative this all is The end result is just silly rather than scary
Its a very handsomely made film the performances are very good and Palermos understated vision is definitely intriguing We just cant help wishing that there was a bit more to it
Quiet and contemplative One  Two is confident in the story it wants to tell and achieves so with remarkable poise
Despite a lot of shortcomings and undercooked elements The Transporter Refueled is a mildly entertaining action film
A breezily paced car chase film that does exactly what it says on the label
So many amazingly absurd things occur during the course of The Transporter Refueled that its almost easy to forget that the basic idea driving this fourth installment is itself nonsensical
There is more intrigue in one of Stevensons winks than in the entire plot though to be fair Skrein does a good job of staying unrumpled in even the most violent tussles
What exactly is the point of a Transporter movie without Jason Statham
The Transporter Refueled conclusively proves that this series mild success has been a result of Jason Stathams magnetic turn as the central character
Bad script bad cinematography and bad acting everything is wrong in this movie Full review in Spanish
Talk about your mindless entertainment The acting in this thing is worse than deplorable Had the acting been any better the film wouldnt have worked But its clear that nobody is taking anything seriously and it is somehow strangely enjoyable
Worst movie of the year
It appears that this franchise has hit a dead end running on nothing but fumes
Stupid But not in a good way Just plain insufferably stupid The movie objectifies the women in exactly the same way the villains do In a summer that saw the rise of Imperator Furiosa Im sorry this just wont do
The movie is running on empty with the same sort of insane plot and laughably unbelievable stunts that made the first three movies hardly worth the trouble to begin with
For each deliriously silly action sequence Refueled offers long scenes of tired genre conventions played straight
Dont think too hard about it or you might ruin the ride
An exciting pleasantly mindless way to blow  minutes
In spite of the unexpected plot twists the movie is predictable and barely holds up for its  minute running time Full review in Spanish
Like James Bond wilfully anonymous driver Frank Martin is reborn as a new actor without any fuss shifting the tone of the franchise from Jason Stathams knowing wink to Ed Skreins stonefaced glower
You have believe the unveliebable if you want to ba able to enjoy this one Full review in Spanish
Sticking to its roots the movie shows us an impeccable fighting and driving machine but not much else Full review in Spanish
Ed Skrein takes the wheel from Jason Stathams underworld driver for this relaunch of the action thriller franchise but theres not much gas left in the tank
A film of big ideas and rich themes that nevertheless grabs you by the throat and doesnt let go until the credits have rolled
This is a potentially interesting film that for various reasons does not quite work It is hard to get invested in the woes of Travis a morose and unappealing character
With fine technical credentials on a low budget and impressive playing all round from Winter Matthews movie is certainly worth seeking out
The last gasps of a romantic relationship between two very different men are intimately and delicately charted in the beautifully immersive if decidedly somber Like You Mean It
The creatures  which range from a humansized frog to a sprite with a big metal box for a head  provide a worthy showcase for Murakamis prodigious visual imagination
Takashi Murakami has invested the film with the same sort of primal popart aesthetic that distinguishes much of his art
Watching unmemorable child protagonists repeatedly fight and bond with each other and a bunch of FRIENDs tackylooking Pokmonesque monsters is unproductively exhausting
At once heavily pretentious and suffocatingly derivative
Whatever you might think of Mr Murakamis paintings and sculptures they are invariably polished and eerily perfect but his movie seems thrown together
The film is rich in the artists creative imagery but too thin in its storytelling to captivate a casual audience not already enamored with his work
The story is barely original or interesting enough to sustain a half hour of kids TV
Though it may amuse hardcore devotees of kaiju film on video the film hardly threatens to make Murakami the next Julian Schnabel
Japanese artists fantasy film is disappointing derivative
Whatever theories Murakami has perfected in his visual art hes miscalculated in his featurefilmmaking dabble
Unfortunately a narrative feature film is more than just moving images and once Murakami dives into narrative and character Jellyfish Eyes begins stumbling all over itself
Israeli director Nadav Lapid uses a wellworn concept  a lonely little boy is taken under a teachers wing  to create a slow creepy movie
What is weird and ultimately destructive is the way that The Kindergarten Teacher conceptualizes its child prodigy
Lapids skilled direction and Larrys fascinating performance allow us to find the true art in the unexpected much like Nira does herself
A selfassured remarkably powerful film from the Israeli writerdirector Nadav Lapid
Nadav Lapids astonishingly bold film about a gifted child and his effect on his teacher is a rewarding unsettling and moving experience
The Kindergarten Teacher is too lackadaisical to be as profound as it thinks it is
You may be perplexed or shocked if you take it literally thoughtful if you consider it social criticism or get carried away to consider it allegory You wont forget it
On an immediate level the film is a commentary on what we do with other peoples words when we quote themand in so doing deform them customizing them to our own requirements
Bubbling beneath the troubling educational methods is a complicated conversation about art and the audience
Keeps its audience rapt until the quietly shattering conclusion
There are annoying and deadly dull passages in The Kindergarten Teacher There are also intriguing elements that will stick with you after the final credits roll  if you make it that far
Lapid following his acclaimed  hostage drama Policeman clearly has a lot on his mind here But he tells his overlong story in such a diffuse at times elliptical way that his reach exceeds its grasp
Poetry is dead  but maybe a fiveyearold boy can save it with the help of his kindergarten teacher
Lapids tense psychological drama leaves us with more questions than answers but as with great poetry or art in general maybe everything doesnt have to be spelled out
A perplexing but alltoohuman psychological drama
The Kindergarten Teacher is more than a portrait of the artist as a young man Its almost a horror story about the mysteries of art itself
An interesting and complex film Full review in Spanish
It shows us intriguing and dark chapters including strong sex scenes to denounce a certain sector of Israeli society that puts appearances money and material things above else Full review in Spanish
Its artistic and cinematographic quality and especially being different to what you usually see are worth the effort to go out for a chance to see it Full Review in Spanish
The Kindergarten Teacher is stylish pretentious and cerebral with characters so schematic that they only intermittently resemble actual human beings
Roth who is no Michael Haneke or even Adrian Lyne seems unconcerned with creating genuine tension or digging into an allegory of moral consequence
All foreplay and no well climax
As a piece of social satire Knock Knock winds up being not just toothless but anticlimactic
Alas the intentions of pure comedy dont last for long and the movie becomes a fairly routine series of torture scenes and bursts of violence
As good as Reeves is the potential for Knock Knock is seldom met Roth would rather play to his tiresome habits instead of challenging himself with a vigorous malevolent thriller
Manages to be impossibly conservative while being impossibly sleazy and thats the way were supposed to like it I suppose
Knock Knock is a pretty flimsy erotic thriller but thanks to Reeves oaken obliviousness its also got a few moments of deliciously trashy fun
Theres satiric potential here but Eli Roths sense of humor abandons him when his hero isnt about to get down with the get down
Even fans of Roths past work will probably be disappointed by the relative lack of gore in this one which suggests a mix of horror and thriller but quickly settles into more of the latter
Director Eli Roths Knock Knock a remake of the  exploitation picture Death Game sometimes plays more like a comedy than like the grungy thriller that inspired it but thats often all to the good
The characters are driven by convenience not behavior and their actions seem like theyve been manhandled into place to make the plot work
Knock Knock is a midnight movie that never comes to fruition wasting a stellar concept and cast on forced Bmovie antics that are far too unfunny for Roths talents
Often feels like a tongueincheek episode of Red Shoe Diaries
The characters arent really characters  theyre archetypes  and the movie plays as a soft core lark rather than more substantial entertainment
An erotic homeinvasion thriller which  despite its inconsistencies and lack of depth  is an enjoyable enough romp
The morality here is biblical but also blackly comical it demands punishment without mercy for the infractions of faithlessness and lust
Its ultimately more interested in being lurid than provocative
Knock Knock which is about two women wreaking havoc on a married man aspires to be titillating But more than anything both persistently persuasively angle to make you angry
Another subpar effort from director Eli Roth who finally breaks out of his xenophobia but sadly for a pretty abysmal thriller
Intriguing spin on homeinvasion thriller  Slickly produced energetically played especially by Reeves as the good husband who when home alone falls prey to kinky temptations
Maddeningly vague
Dark chilling impressive film about the training of child assassins in a sequestered commune led by a charismatic figure played to perfection by Vincent Cassel
On one level Alexander is the freedomfighter and Gregori the corrupt old tyrant like Ceaușescu Miloević or Tito On another you wish Partisan were more pointed in its writing more connected to an outside modern world
Ambiguity proves the undoing of a potentially interesting story in Partisan the first feature by Australian filmmaker Ariel Kleiman
Newcomer Jeremy Chabriel commands as yearold Alexander a chosen son whose dawning sense of right and wrong challenges the social order An auspicious beginning for him and Kleiman
Usually partisans stand for something but in this movie which is stripped of a specific time frame and relevant geopolitical context the term becomes hollow
A moody drama that employs a dystopiantype premise that is not too far afield from a typical youngadult book series although with loftier aspirations and a less propulsive pace
Its hard to tell if director and cowriter Ariel Kleiman is being serious or sarcastic with a story this preposterous
Vincent Cassels primal physicality and threatening charm are front and center in this taut dystopian fable
A deeply frustrating headscratcher
Jeremy Chabriel is arresting as the increasingly uneasy youngster and director and cowriter Ariel Kleiman proves adept at both worldbuilding and attentionfocusing
Partisan is an impressive feature debut both expertly condemning its paternalistic society but sadly lacking in a single interesting female character to take a stand to Cassels complex figure
A film trying so hard to upend expectations that it loses itself in the process
Theres some decent technical stuff here but the storys a big ol bust
Oddly underpowered anticlimactic and torpidly acted
Cassels subtle and quietly menacing performance helps compensate for a story that drifts along rather aimlessly at times while Kleimans dreamy visuals mark him out as a talent to watch
Kleiman has an eye for poetic imagery but the storytelling style is so oblique and dour that this is a jarring and difficult film to watch
Kleimans Dogtoothesque dark drama paints a weird and disturbing portrait of stolen innocence
With his feature debut young Australian filmmaker Ariel Kleiman tells a creepy story about a cultlike commune anchored by a riveting performance from French actor Vincent Cassel
A patiently unfolding drama that displays the lengths one will go to provide shelter and community and what happens if you step out of bounds
While not a masterpiece on the order of s Safety Last and s The Freshman Speedy still ranks as gradeA entertainment from the great Harold Lloyd
Highlights include a wild chase scene through New York City traffic a visit to Coney Island by Speedy and Jane and a guest appearance by Babe Ruth
This IMAX spectacular largely does what its supposed to fascinate educate and visually wow the audience in  minutes or less
Laudable for turning armchair tourism into a breathtaking experience  a viewer can truly feel as if he or she has gone inside a number of fantastic ancient places
The film unearths some of this history as well even if at times it feels like a quick course in comparative religion Its message is familiar  lets all try to get along  but the views are remarkable
You dont just go to Jerusalem You experience Jerusalem Those are the words of the director of this phenomenal documentary
The film is at its most moving paradoxically when the camera gets down to street level seeming to squeeze for example into a small shop in the walled Old City where two men play backgammon amid claustrophobically overhung racks of trinkets
Benedict Cumberbatch narrates with effortless authority but its the personal stories of three attractive young women present day inhabitants representing the three faiths which make this a moving and vital production
You can all but feel the heat of blazing candles in the Orthodox preEaster ritual of the Holy Fire in the Church of the Holy Sepulchre the most impressive scene in a dazzling film
The film effectively answers why this one place not even a square kilometre in size is such prime religious real estate but it barely gestures toward the blood that has been paid for it
Magnificently scenic but fatally bland
With its overdrawn characters and hackneyed dialogue its a politicized Billy Elliot
Desert Dancer does manage to movingly convey the chilling ultimately triumphant experience of Ghaffarians struggle for creative expression under a regime that tried to crush it
Raymonds diffuse direction is so oldschool madeforcableTV that it blunts the drama and never turns up the heat beyond tepid
Its all over the place All these are fine places to be when it comes to movies Its just exhausting to try to be in all of them at once
Desert Dancer explores fascinating aspects of presentday Iran but suffers mightily from simplistic and sentimental tendencies
Reece Ritchie brings heart to the lead role particularly in the decisive solo performance that caps the film But that sequence is as expressive and alive as the rest of the supposedly movementloving drama is static
Desert Dancer is the dramatization of a life story that rarely doesnt feel like a dramatization
As impressive as its dance sequences may be Desert Dancer cannot overcome an intellectually lazy script thats as subtle as an eyehigh leg kick
Unsurprisingly dancing is the highlight of Desert Dancer but it hardly receives the attention it deserves
Its depiction of the internal tensions in Iranian politics and culture is not what youd call supernuanced but it is sympathetic to Iran as an entity
Director Richard Raymond making his feature debut doesnt seem to have enough story to fill out a fulllength movie
In his feature film debut director Richard Raymond keeps the story moving despite some dramatic cliches
The story of freedom from oppression and freedom of expression in Iran is a somber and ruthless one but youd never know the true extent of it by seeing this film
The characters are thin and the villains are onenote but one gets worked up anyway much as one gets played by a solid romance  or a dance movie
Desert Dancer strays too frequently into melodrama to have much sticking power
There would seem to be only one thing that the act of dancing cannot free the characters in Desert Dancer from and thats the movie theyre in
By the time Desert Dancer heads for its shadesofArgo climax its hard not to wish there had been a bit less melodrama and a bit more desert dancing
too earnest and cliched  with awkward English dialogue considering the setting  and tends to oversimplify Afshins truelife struggles
The sum total is an interesting truelife story that just doesnt make for a great movie Desert Dancer has its moments but it feels like a story that was better lived than retold
Despite that fact that Pinto is such a vital performer her characters backstory is handled so clumsily that it feels like one more clich thrown onto the pile
Roger Waters The Wall ends up being just another brick in the source materials audacious history of which Waters should be trusted to continue finding success at telling
A concert movie where the considerable pyrotechnics gathered never quite obscure the fully functioning conscience centre stage
This is not merely bravura stadium progrock for Pink Floyd fans but a curious home road movie
Minutes in Heaven stands out from the herd of films designed to spread the good word by telling a story with welldeveloped main characters excellent cinematography and other topnotch production elements
Rendering a miraculous premise dull the film seems relatively uninterested in doing more than preaching to the choir
It fails to create a satisfying narrative with a true arc that pays off its too caught up in explaining its minor details to focus on the big picture
Although this wellmeaning film may appeal to its intended audience on a spiritual level the result is a sluggish clinical largely dreary portrait that tends to mistake trauma for drama
Long and often nearly dramatically inert Perhaps had the film spent more time detailing those heavenly ninety minutes the offering might have worked better or at least been more interesting Full Content Review for Parents also available
Flatlines in its last half hour and barely putters along in the hour and half before that
A staggeringly uneventful movie made for an audience to which I obviously do not belong Im frankly at a loss to convey how little happens during  Minutes in Heaven
While there are fine restrained moments exploring faith as both comfort and struggle theres almost no way to portray a vision of Heaven literally that doesnt feel like somebodys telling us exactly what he thinks we want to hear
The titular experience is buried in favor of a repetitive heavyhanded barrage of bedside hospital prayers
Turns out people can get really irritable after seeing heaven
The movie captures the drama but not the passion
This inspirational indie earns points by being more bluntly realistic than many other faithbased dramas in its depiction of an ordeal that likely would challenge the faith of even the most devout Christians
A better title wouldve likely been  Minutes in Purgatory since thats essentially where audiences will find themselves residing during the entirety of this dreary slog down a familiar road paved with painfully good intentions
May strike a welcome chord with the evangelical groups at whom its obviously aimed But for others the ploddingly preachy picture will seem more like a stint in purgatory if not someplace even more uncomfortable
Although its centered around a fascinating true story  Minutes in Heaven is plagued by an uneventful and padded out two hours
Cynical slow and deathlydull
The screenplay is full of infelicitous dialogue far from a plausible vernacular which wouldnt matter if the movie had an ounce of stylization to justify its fourthgradereadinglevel airportnovel vocabulary
Sincere performances carry the day
Dramatic real life story
So deeply terrible that it will make you question the existence of God The dialogue is the least natural Ive ever seen in a film not made by Ed Wood
Competently made the film doesnt really bring anything truly original to the table other than Purefoys turn as Mr Washington
You can say this much for Momentum  it more than sustains its own
Inserting a political thriller angle that reaches Bournelevel conspiracy proportions in the last three minutes and setting up a sequel the filmmakers unfounded selfworth and confidence in their project makes the whole thing pathetic and misguided
Yet another colorless featureless stunt extravaganza that emphasizes physical feats and convoluted plotting trying to razzledazzle audiences with visuals theyve seen countless times before
Momentum has its moments of fun but overall its a well shot capably acted run of the mill action film
Momentum is caught between being pulpy mindless entertainment and sociallyconscious commentary on government It doesnt do either competently
If youre going to call your movie Momentum it really ought to move
Just as entertaining as the action itself is the back and forth banter between Olga Kurlenko and James Purefoy
One of the worst films you wont see in
The curse of Bourne continues to linger over the lower echelons of the film industry with yet another attempt to replicate its success sputtering out of the gate
Momentum is a strange husk of an action movie one that contains all the right beats of a Hollywood contender including a preposterous closingreel setup for an entire franchise to come without a single moment of dramatic intrigue or credibility
Momentum is in the end silly trashy Bmovie nonsense yet its top quality trash of the highest order as it does what it sets out to do by being enjoyably entertaining and having a stylistic gloss to its visuals
Sure its a kickass Bourne on a budget but it has a strikingly mature sense of style slick walltowall stunts plus wit and suspense
From the Mighty Morphin Power Rangers opening through the Starsky  Hutch gunfight in a warehouse setpieces to the contractualobligation shots of Kurylenkos bum this is utter pants
With a plot so thin that its barely there this sleek South African action thriller is surprisingly entertaining simply because the cast is allowed to chomp merrily on the scenery as they try to torment and kill each other
this action thriller never stops moving except when Olga Kurylenkos kickass heroine Alex has her leg literally caught in a vice racing through its series of genre routines in the hope that speed alone can cover over any cracks
Campanellis main goal seems to have been to incorporate as many action movie clichs as possible in a single film
When Momentum gets going theres an entertaining time to be had trying to catch up
Right down to its generic title this laughable catandmouse thriller is a celebration of vigilante clichs
Stephen Campanelli keeps Momentum moving with brisk assurance making up for his films routine script and modest budget with slick visuals and impressively punchy action
Hoping to revive a classic war film ambiance the endeavor is simply too confined and shiny to feel knucklecrackingly retro
its the music that suddenly feels monumental because somewhere in that dark stream of rolling notes and rumbling minors we can hear the eternal soul of human sadness turned for a brief moment into something undeniably beautiful
Watching Kapadias film it is possible to see how badly she was let down by the male figures closest to her
The films most obvious and honest bet is to show us the artistic strenght of Amy Winehouse Full review in Spanish
The words of Stronger Than Me reverberate through this devastating film we cant help but wonder if things would have turned out very differently had Winehouses family friends lovers collaborators and business partners been a little stronger
Amy is a celebration of all she accomplished as well as a cautionary tale about the grave price to be paid for not getting an addict the help they need
A melancholy deconstruction of the rise and demise of a talented but troubled pop diva
Director Asif Kapadia rounds up revelatory even joyous footage and interviews The film hes assembled from them is a heartbreaking portrait of a major talent with her fair share of demons which only haunted her more once she became a star
As sad as Winehouses story may be Amy is gorgeous and provocative too
The film pays tribute to a great performer leaving little doubt that she possessed one of the great jazz voices of all time
One of the countrys great musical icons gets a fittingly superb documentary
As concerned with perceptions as it is a cautionary tale about the nature of modern fame the profoundly depressing Amy is not the last word on Amy Winehouse but for now it is hard to see how it could be bettered
Amy Winehouses story is a tragic one and a familiar one This film sets her apart from other similar entertainers who died young but not far enough
Captures the spectators attention with Winehouses music and her downfall in which Asif Kapadia denounces how cruel and decadent the media can be Full review in Spanish
Fascinating in a voyeuristic way A demonstration beyond doubt that celebrity is as lifethreatening as the bite of a Black Mamba
Watching this documentary was like listening to an orchestra tune itself After awhile you want the conductor to appear
A devastating infuriating and sometimes breathtaking watch
It doesnt give us any more insight into what drove this phenomenally talented young woman to self destruct at such a young age
Asif Kapadia manages to create an honest portrait of Amy Winehouse without having to use any narration or off voice Full review in Spanish
Even if its told in an entertaining way Winehouses personal life is not enough to justify a two and a half hour film Full review in Spanish
A cinematic shaming
Too many bizzaro elements begin to eat away at ones attention span Even if its the end times this Apocalypse didnt need every idea at once
Yakuza Apocalypse is Miike at the top of his game breaking cinematic rules at every chance while crafting seriously subversive cinema that defangs both the realworld Yakuza the Japanese government and heaven help us Sanrio too
Directed by the inimitable Takashi Miike Apocalypse finds the yearold filmmaker at his silliest which is saying a lot as hes responsible for some of the silliest movies ever made
Lurid and nutso doesnt even begin to describe this opus You have to see it to believe it  if you have the stomach for it
By the end  you might be as punchdrunk as Kageyama in his final showdown and too exhausted to care
For those fans who dont mind enduring some tedium and confusion Yakuza Apocalypse at least offers something memorably bizarre
Yakuza Apocalypse is a movie that is perhaps not meant to be understood so much as witnessed
Real silliness doesnt settle into a routine This movies brand of it does
A very good film but only if youre willing to inevitably submit to its anarchic sensibility
Gonzo laughoutloud funny but with not enough action or laughs to sustain it
Yakuza Apocalypse may not be Miikes best film and lord knows hell try to top it ten times over but its the only one to feature a kungfu frog monster destroyer of worlds
Those looking for a straight actioncrime flick will be disappointed or horrified Probably both
If you want to enjoy Yakuza Apocalypse for its snippets of inspiration we suggest a Baudrillardian form of watching flick it on and off between late night channel hopping
Yakuza Apocalypse is barely comprehensible barminess But its definitely fun
deliriously daft until its final oaterstyle showdown Miike delivers something all at once insane inane and irresistible
It is extremely mad long and often tiresome Yet it is acted with eerie and absolute conviction and has an interestingly surreal quality Miike has a claim to be one of cinemas genuine surrealists
Slow down Takashi
If youre a Miike fan or simply looking for something weird and wonderful then you certainly wont forget this in a hurry
back to the wild and weird Miike with extreme action bloody spectacle goofy humor and abrupt curveballs every time you think you think youve got a handle on it
Its subtle victory though maybe its central weakness as well is its denial of quick easy fanboy pleasures
I highly recommend anyone who liked Console Wars Atari Game Over or other retro gaming celebrations watch this too And if this is your first look back at retro games let it start your nostalgia all over again
Never developing into unabashed antagonism the perpetual pushpull of Louis and Johns relationship ended up being both totally organic and refreshing
Admirably performed and skilled at detailing Texan expanse the feature manages to hit the heart when it counts the most carrying a promising amount of concern for its characters
Duhamel and Wiggins unfortunately dont have the kind of Paper Moon chemistry that could have helped gloss over some of the hardertoswallow plot turns
A monotonous bore Lost in the Sun clearly thinks its saying something profound but what it says has been said before many times in much better pictures
Despite some scenic territory theres just not much to this journey leaving Lost in the Sun feeling like a short story stretched way too thinly toward feature length
Remove the charm and the Depressionera aura from Paper Moon and you have Lost in the Sun a road movie that goes nowhere particularly interesting
Thanks to the excellent if a little on the obviouslypictorialside cinematography by Robert Barocci youve seen some lovely vistas on the way to indifference
Lost in the Sun is a technically proficient but achingly typical gloomy southern crime drama one that audiences can seek out if they exhausted every other similar film
Its a story of redemption and reconciliation that follows a wellworn path and strains credibility as it meanders along
At every minute in this film you know where its going because nothing that happens in any of the  minutes of this film works at all if the movie doesnt get there"""]

let freq_table = getFreqTable ughCorpus 
let ughString = Newtonsoft.Json.JsonConvert.SerializeObject freq_table 
System.IO.File.WriteAllLines ".\ughCorpus.json" json_string