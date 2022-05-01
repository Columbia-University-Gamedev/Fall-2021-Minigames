using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{

    public string[][] dialogues = new string[6][];

    public void Start()
    {
        dialogues[0] = new string[] { "Hi, welcome to the city! Are you new here?", "Our city isn't much, but its home.", "We're very friendly, feel free to ask around to learn more about it!" };
        dialogues[1] = new string[] { "Judging from the outfit, you must be into cosplay.", "What a coincidence!" };
        dialogues[2] = new string[] { "Looks like one of us is going to have to change!" };
        dialogues[3] = new string[] { "A few weeks ago someone came through and broke all the doors on our buildings.", "Luckily the weather's always perfect.", "Still, a bit weird, don't you think?" };
        dialogues[4] = new string[] { "Do you ever wonder why our buildings look kinda blocky?", "I think I need to lay off the pix-ale"};
        dialogues[5] = new string[] { "With no cars I sure am glad that everything's in walking distance."};
        dialogues[6] = new string[] { "One time I was late to work because the building didn't generate in time.", "Do you think you could optimize it some?", "I'm on my last notice!" };
        dialogues[7] = new string[] { "Hmm...", "Huh...", "...", "Oh! Hello, I'm just spacing out." };

    }

}
