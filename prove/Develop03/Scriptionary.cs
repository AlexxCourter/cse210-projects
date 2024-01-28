using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;

public class Scriptionary
{
    //Scriptionary acts as a dictionary of scriptures
    //that can be loaded into the memorization tool
    //based on a user selection.
    private Dictionary<int, Scripture> _scriptures = new Dictionary<int, Scripture>();
    private List<Reference> _references = new();

    public Scriptionary()
    {
        //create scripture objects to load into the dictionary
        //for this, we will use a set of the seminary book of mormon
        //doctrinal mastery scriptures (including the whole verse(s))
        //the list is ordered according to this page:
        //https://www.churchofjesuschrist.org/study/manual/doctrinal-mastery-core-document-2023/doctrinal-mastery-passages-and-key-phrases?lang=eng
        Reference one = new("1 Nephi",3,7);

        Reference two = new("2 Nephi",2,25);
        Reference three = new("2 Nephi",2,27);
        Reference four = new("2 Nephi",26,33);
        Reference five = new("2 Nephi",28,30);
        Reference six = new("2 Nephi",32,3);
        Reference seven = new("2 Nephi",32,8,9);

        Reference eight = new("Mosiah",2,17);
        Reference nine = new("Mosiah",2,41);
        Reference ten = new("Mosiah",3,19);
        Reference eleven = new("Mosiah",4,9);
        Reference twelve = new("Mosiah",18,8,10);

        Reference thirteen = new("Alma",7,11,13);
        Reference fourteen = new("Alma",34,9,10);
        Reference fifteen = new("Alma",39,9);
        Reference sixteen = new("Alma",41,10);

        Reference seventeen = new("Helaman",5,12);

        Reference eighteen = new("3 Nephi",11,10,11);
        Reference nineteen = new("3 Nephi",12,48);
        Reference twenty = new("3 Nephi",27,20);

        Reference twentyone = new("Ether",12,6);
        Reference twentytwo = new("Ether",12,27);

        Reference twentythree = new("Moroni",7,45,48);
        Reference twentyfour = new("Moroni",10,4,5);

        _references = new List<Reference>{
            one, two, three, four, five, six,
            seven, eight, nine, ten, eleven, twelve, 
            thirteen, fourteen, fifteen, sixteen, seventeen, eighteen,
            nineteen, twenty, twentyone, twentytwo, twentythree, twentyfour
        };
        //1 nephi 3:7
        Scripture scrip1 = new(one, "And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them.");
        // 2 Nephi 2:25
        Scripture scrip2 = new(two, "Adam fell that men might be; and men are, that they might have joy.");
        // 2 Nephi 2:27
        Scripture scrip3 = new(three, "Wherefore, men are free according to the flesh; and all things are given them which are expedient unto man. And they are free to choose liberty and eternal life, through the great Mediator of all men, or to choose captivity and death, according to the captivity and power of the devil; for he seeketh that all men might be miserable like unto himself.");
        // 2 Nephi 26:33
        Scripture scrip4 = new(four, "For none of these iniquities come of the Lord; for he doeth that which is good among the children of men; and he doeth nothing save it be plain unto the children of men; and he inviteth them all to come unto him and partake of his goodness; and he denieth none that come unto him, black and white, bond and free, male and female; and he remembereth the heathen; and all are alike unto God, both Jew and Gentile.");
        // 2 Nephi 28:30
        Scripture scrip5 = new(five, "For behold, thus saith the Lord God: I will give unto the children of men line upon line, precept upon precept, here a little and there a little; and blessed are those who hearken unto my precepts, and lend an ear unto my counsel, for they shall learn wisdom; for unto him that receiveth I will give more; and from them that shall say, We have enough, from them shall be taken away even that which they have.");
        // 2 Nephi 32:3
        Scripture scrip6 = new(six, "Angels speak by the power of the Holy Ghost; wherefore, they speak the words of Christ. Wherefore, I said unto you, feast upon the words of Christ; for behold, the words of Christ will tell you all things what ye should do.");
        // 2 Nephi 32:8-9
        Scripture scrip7 = new(seven, "And now, my beloved brethren, I perceive that ye ponder still in your hearts; and it grieveth me that I must speak concerning this thing. For if ye would hearken unto the Spirit which teacheth a man to pray, ye would know that ye must pray; for the evil spirit teacheth not a man to pray, but teacheth him that he must not pray. But behold, I say unto you that ye must pray always, and not faint; that ye must not perform any thing unto the Lord save in the first place ye shall pray unto the Father in the name of Christ, that he will consecrate thy performance unto thee, that thy performance may be for the welfare of thy soul.");
        // Mosiah 2:17
        Scripture scrip8 = new(eight, "And behold, I tell you these things that ye may learn wisdom; that ye may learn that when ye are in the service of your fellow beings ye are only in the service of your God.");
        // Mosiah 2:41
        Scripture scrip9 = new(nine, "And moreover, I would desire that ye should consider on the blessed and happy state of those that keep the commandments of God. For behold, they are blessed in all things, both temporal and spiritual; and if they hold out faithful to the end they are received into heaven, that thereby they may dwell with God in a state of never-ending happiness. O remember, remember that these things are true; for the Lord God hath spoken it.");
        // Mosiah 3:19
        Scripture scrip10 = new(ten, "For the natural man is an enemy to God, and has been from the fall of Adam, and will be, forever and ever, unless he yields to the enticings of the Holy Spirit, and putteth off the natural man and becometh a saint through the atonement of Christ the Lord, and becometh as a child, submissive, meek, humble, patient, full of love, willing to submit to all things which the Lord seeth fit to inflict upon him, even as a child doth submit to his father.");
        // Mosiah 4:9
        Scripture scrip11 = new(eleven, "Believe in God; believe that he is, and that he created all things, both in heaven and in earth; believe that he has all wisdom, and all power, both in heaven and in earth; believe that man doth not comprehend all the things which the Lord can comprehend.");
        // Mosiah 18:8-10
        Scripture scrip12 = new(twelve, "And it came to pass that he said unto them: Behold, here are the waters of Mormon (for thus were they called) and now, as ye are desirous to come into the fold of God, and to be called his people, and are willing to bear one another's burdens, that they may be light; Yea, and are willing to mourn with those that mourn; yea, and comfort those that stand in need of comfort, and to stand as witnesses of God at all times and in all things, and in all places that ye may be in, even until death, that ye may be redeemed of God, and be numbered with those of the first resurrection, that ye may have eternal life— Now I say unto you, if this be the desire of your hearts, what have you against being baptized in the name of the Lord, as a witness before him that ye have entered into a covenant with him, that ye will serve him and keep his commandments, that he may pour out his Spirit more abundantly upon you?");
        // Alma 7:11-13
        Scripture scrip13 = new(thirteen, "And he shall go forth, suffering pains and afflictions and temptations of every kind; and this that the word might be fulfilled which saith he will take upon him the pains and the sicknesses of his people. And he will take upon him death, that he may loose the bands of death which bind his people; and he will take upon him their infirmities, that his bowels may be filled with mercy, according to the flesh, that he may know according to the flesh how to succor his people according to their infirmities. Now the Spirit knoweth all things; nevertheless the Son of God suffereth according to the flesh that he might take upon him the sins of his people, that he might blot out their transgressions according to the power of his deliverance; and now behold, this is the testimony which is in me.");
        // Alma 34:9-10
        Scripture scrip14 = new(fourteen, "For it is expedient that an atonement should be made; for according to the great plan of the Eternal God there must be an atonement made, or else all mankind must unavoidably perish; yea, all are hardened; yea, all are fallen and are lost, and must perish except it be through the atonement which it is expedient should be made. For it is expedient that there should be a great and last sacrifice; yea, not a sacrifice of man, neither of beast, neither of any manner of fowl; for it shall not be a human sacrifice; but it must be an infinite and eternal sacrifice.");
        // Alma 39:9
        Scripture scrip15 = new(fifteen, "Now my son, I would that ye should repent and forsake your sins, and go no more after the lusts of your eyes, but cross yourself in all these things; for except ye do this ye can in nowise inherit the kingdom of God. Oh, remember, and take it upon you, and cross yourself in these things.");
        // Alma 41:10
        Scripture scrip16 = new(sixteen, "Do not suppose, because it has been spoken concerning restoration, that ye shall be restored from sin to happiness. Behold, I say unto you, wickedness never was happiness.");
        // Helaman 5:12
        Scripture scrip17 = new(seventeen, "And now, my sons, remember, remember that it is upon the rock of our Redeemer, who is Christ, the Son of God, that ye must build your foundation; that when the devil shall send forth his mighty winds, yea, his shafts in the whirlwind, yea, when all his hail and his mighty storm shall beat upon you, it shall have no power over you to drag you down to the gulf of misery and endless wo, because of the rock upon which ye are built, which is a sure foundation, a foundation whereon if men build they cannot fall.");
        // 3 Nephi 11:10-11
        Scripture scrip18 = new(eighteen, "Behold, I am Jesus Christ, whom the prophets testified shall come into the world. And behold, I am the light and the life of the world; and I have drunk out of that bitter cup which the Father hath given me, and have glorified the Father in taking upon me the sins of the world, in the which I have suffered the will of the Father in all things from the beginning.");
        // 3 Nephi 12:48
        Scripture scrip19 = new(nineteen, "Therefore I would that ye should be perfect even as I, or your Father who is in heaven is perfect.");
        // 3 Nephi 27:20
        Scripture scrip20 = new(twenty, "Now this is the commandment: Repent, all ye ends of the earth, and come unto me and be baptized in my name, that ye may be sanctified by the reception of the Holy Ghost, that ye may stand spotless before me at the last day.");
        // Ether 12:6
        Scripture scrip21 = new(twentyone, "And now, I, Moroni, would speak somewhat concerning these things; I would show unto the world that faith is things which are hoped for and not seen; wherefore, dispute not because ye see not, for ye receive no witness until after the trial of your faith.");
        // Ether 12:27
        Scripture scrip22 = new(twentytwo, "And if men come unto me I will show unto them their weakness. I give unto men weakness that they may be humble; and my grace is sufficient for all men that humble themselves before me; for if they humble themselves before me, and have faith in me, then will I make weak things become strong unto them.");
        // Moroni 7:45-48
        Scripture scrip23 = new(twentythree, "And charity suffereth long, and is kind, and envieth not, and is not puffed up, seeketh not her own, is not easily provoked, thinketh no evil, and rejoiceth not in iniquity but rejoiceth in the truth, beareth all things, believeth all things, hopeth all things, endureth all things. Wherefore, my beloved brethren, if ye have not charity, ye are nothing, for charity never faileth. Wherefore, cleave unto charity, which is the greatest of all, for all things must fail— But charity is the pure love of Christ, and it endureth forever; and whoso is found possessed of it at the last day, it shall be well with him. Wherefore, my beloved brethren, pray unto the Father with all the energy of heart, that ye may be filled with this love, which he hath bestowed upon all who are true followers of his Son, Jesus Christ; that ye may become the sons of God; that when he shall appear we shall be like him, for we shall see him as he is; that we may have this hope; that we may be purified even as he is pure. Amen.");
        // Moroni 10:4-5
        Scripture scrip24 = new(twentyfour, "And when ye shall receive these things, I would exhort you that ye would ask God, the Eternal Father, in the name of Christ, if these things are not true; and if ye shall ask with a sincere heart, with real intent, having faith in Christ, he will manifest the truth of it unto you, by the power of the Holy Ghost. And by the power of the Holy Ghost ye may know the truth of all things.");

        List<Scripture> scripList = new List<Scripture>()
        {scrip1, scrip2, scrip3, scrip4, scrip5, scrip6, 
        scrip7, scrip8, scrip9, scrip10, scrip11, scrip12, 
        scrip13, scrip14, scrip15, scrip16, scrip17, scrip18, 
        scrip19, scrip20, scrip21, scrip22, scrip23, scrip24};

        int counter = 1;
        foreach (Scripture scrip in scripList)
        {
            _scriptures.Add(counter, scrip);
            counter++;
        }

    
    }
    public string GetDictionaryReferences()
    {
        string result = "";
        int counter = 1;
        foreach ( Reference refer in _references)
        {
            result += $"{counter} - {refer.GetDisplayText()}\n";
            counter++;
        }
        return result;
    }

    public Scripture GetScriptureByNumber(int scriptureNumber)
    {
        Scripture result;
        if (_scriptures.ContainsKey(scriptureNumber))
        {
            result = _scriptures[scriptureNumber];
        }
        else
        {
            // if somehow the scriptureNumber is not one of the specified numbers,
            //need to throw an exception to alert an error.
            throw new Exception("Scripture not found. Please try again.");
        }
        return result;
    }
}