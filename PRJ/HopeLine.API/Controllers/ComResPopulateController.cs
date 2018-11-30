using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HopeLine.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComResPopulateController : ControllerBase
    {
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly ICommonResource _commonResource;

        public ComResPopulateController(UserManager<HopeLineUser> userManager, ICommonResource commonResource)
        {
            _userManager = userManager;
            _commonResource = commonResource;

        }

        public async Task<IActionResult> CreateTopics()
        {
            if (_commonResource.GetTopics().Count() == 0)
            {
                var topics = new List<TopicModel> {
                    //Populate topics
                new TopicModel
                {
                    Name = "Mental Disorder",
                    Description = "A mental disorder, also called a mental illness[2] or psychiatric disorder, is a behavioral or mental pattern that causes significant distress or impairment of personal functioning.[3] Such features may be persistent, relapsing and remitting, or occur as a single episode. Many disorders have been described, with signs and symptoms that vary widely between specific disorders.[4][5] Such disorders may be diagnosed by a mental health professional."
                },
                new TopicModel
                {
                    Name = "Eating Disorder",
                    Description = "An eating disorder is a mental disorder defined by abnormal eating habits that negatively affect a person's physical or mental health.[1] They include binge eating disorder where people eat a large amount in a short period of time, anorexia nervosa where people eat very little and thus have a low body weight, bulimia nervosa where people eat a lot and then try to rid themselves of the food, pica where people eat non-food items, rumination disorder where people regurgitate food, avoidant/restrictive food intake disorder where people have a lack of interest in food, and a group of other specified feeding or eating disorders.[1] Anxiety disorders, depression, and substance abuse are common among people with eating disorders.[2] These disorders do not include obesity.[1]"
                },
                 new TopicModel
                {
                    Name = "Anxiety",
                    Description = "Anxiety is an emotion characterized by an unpleasant state of inner turmoil, often accompanied by nervous behaviour such as pacing back and forth, somatic complaints, and rumination.[1] It is the subjectively unpleasant feelings of dread over anticipated events, such as the feeling of imminent death.[2][need quotation to verify] Anxiety is not the same as fear, which is a response to a real or perceived immediate threat,[3] whereas anxiety involves the expectation of future threat.[3] Anxiety is a feeling of uneasiness and worry, usually generalized and unfocused as an overreaction to a situation that is only subjectively seen as menacing.[4] It is often accompanied by muscular tension,[3] restlessness, fatigue and problems in concentration. Anxiety can be appropriate, but when experienced regularly the individual may suffer from an anxiety disorder.[3]"
                },
                    new TopicModel
                {
                    Name = "Schizophrenia",
                    Description = "Schizophrenia is a mental disorder characterized by abnormal behavior and a decreased ability to understand reality.[2] Common symptoms include false beliefs, unclear or confused thinking, hearing voices that others do not, reduced social engagement and emotional expression, and a lack of motivation.[2][3] People with schizophrenia often have additional mental health problems such as anxiety, depressive, or substance-use disorders.[11] Symptoms typically come on gradually, begin in young adulthood, and in many cases never resolve.[3][5]"
                }, new TopicModel
                {
                    Name = "Bipolar Disorder",
                    Description = "Bipolar disorder, previously known as manic depression, is a mental disorder that causes periods of depression and periods of abnormally elevated mood.[3][4][6] The elevated mood is significant and is known as mania or hypomania, depending on its severity, or whether symptoms of psychosis are present.[3] During mania, an individual behaves or feels abnormally energetic, happy, or irritable.[3] Individuals often make poorly thought out decisions with little regard to the consequences.[4] The need for sleep is usually reduced during manic phases.[4] During periods of depression, there may be crying, a negative outlook on life, and poor eye contact with others.[3] The risk of suicide among those with the illness is high at greater than 6 percent over 20 years, while self-harm occurs in 30�C40 percent.[3] Other mental health issues such as anxiety disorders and substance use disorder are commonly associated with bipolar disorder.[3]"
                },new TopicModel
                {
                    Name = "Mood Disorder",
                    Description = "Mood disorder, also known as mood (affective) disorders, is a group of conditions where a disturbance in the person's mood is the main underlying feature.[1] The classification is in the Diagnostic and Statistical Manual of Mental Disorders (DSM) and International Classification of Diseases (ICD)."
                }, new TopicModel
                {
                    Name = "Depression",
                    Description = "Depression, a state of low mood and aversion to activity, can affect a person's thoughts, behavior, tendencies, feelings, and sense of well-being. A depressed mood is a normal temporary reaction to life events - such as loss of a loved one. It is also a symptom of some physical diseases and a side effect of some drugs and medical treatments. Depressed mood may also be a symptom of some mood disorders such as major depressive disorder or dysthymia.[2]"
                }
                };


                foreach (var item in topics)
                {
                    _commonResource.AddTopics(item);
                }
                await _commonResource.SaveTopicAsync();
                return Ok("Data Newly Populated");
            }
            return Ok("Already populated");
        }
        public async Task<IActionResult> CreateCommunities()
        {
            if (_commonResource.GetCommunities().Count() == 0)
            {
                var communities = new List<CommunityModel>
                {
                    new CommunityModel
                    {
                        Name = "Canadian Association for Suicide Prevention",
                        Description = "Referenced is a list of crisis services,centers, and other hotlines that are available 24 hours. General information about these services are available to you and ar elisted in the link referenced. These servies may be more convenient and may provide you with a good alternative. They provide locations of where they are stationed or located if you find that your located somewhere else",
                        URL = "https://suicideprevention.ca/need-help/",
                        ImageURL = "https://suicideprevention.ca/resources/Pictures/CASP.png"
                    },
                    new CommunityModel
                    {
                        Name = "How to deal with suicide on online communities or forums",
                        Description = "Dealing with suicide in online forums can be difficult or challenging.This link can help give you advice in how to dela with these situations online.",
                        URL = "http://www.managingcommunities.com/2009/06/22/dealing-with-suicide-on-your-online-community-or-forums-how-you-can-help-and-protect-everyone/",
                        ImageURL = "http://www.managingcommunities.com/wp-content/themes/LagunaBlue/images/logo.jpg"
                    },
                    new CommunityModel
                    {
                        Name = "We Hear You",
                        Description = "WeHearYou is an alternative website where you can open up and talk to about any issues that are bothering you in life. This website does not require you to sign up as it's optional and when you wish to chat with one of thier agents they will be available. They are open to help clients out in various issues including mental health issues as well.",
                        URL = "http://myvmlab.senecacollege.ca:6284/app",
                        ImageURL = "http://myvmlab.senecacollege.ca:6284/assets/images/ocean-wave.jpg"
                    },
                    new CommunityModel
                    {
                        Name = "The mighty",
                        Description = "Youtube channel dedicated to providing informative videos of both physical and mental illnesses.",
                        URL = "https://www.youtube.com/channel/UCKQaSdgFK01UyVdv6-Up1mg/videos",
                        ImageURL = "data:image/png;base64,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"
                    },
                    new CommunityModel
                    {
                        Name = "The Mental Health & Psychosocial Support Network",
                        Description = "A global platform to connect with other people for support and sharing knowledge about mental health.",
                        URL = "http://www.mhpss.net/",
                        ImageURL = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAQMAAADCCAMAAAB6zFdcAAAA8FBMVEX///8AoNsAn9sAnNoAm9oAodsqodvc8foxqdUXrMUcmdckossTq74Zp8gAmdm/5fUZrszy+/2Gy+sZotEWqd82sdMxrck2s801t8TP7Ph4xukqpt82q+I2sNqZ0+4quci24udexM7s9/xbu+VEs+LZ8POGz9hFvMmd2eDj9Puy3/JsyNLP7PAAoMQAm8dKutqk2PBs0ONSueWLzt51yNgqsMKPz+xRuMcAoLYAo7Ks2OFowOdHssK/5uuD0Ni54fNtvd6NxudexeRqt+Fvyut3vdwAjdFMpdkAkshOstOt1u5JxNtizOBYutEwuc47u+Qxa/0rAAAJ3klEQVR4nO2bDXvaRhLHpV0pEJvIFogaAcG2JAzCJMbKmfSiElrbTXp5OX//b3M7sytpBcRJn+eEcr751W3ICuTZ/87brqhhEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEMTfxI399ad1I6jbjtqIRx8PHOfDB4ez4d0nt25zamD98YNzgDwDLOeuUbdJe8b/+OHgQJNAwPmf/1e+sHQOcgaZCM86naO6Ddsb7h0Xy565wSATYdBsdpZ127Yn3CGDGQ9QBT4YKBEGw6YQ4c+6rdsPHxlOWQCRoDQYDIegQbPTrtu8fQCBoDQYZH+AAlKDZmdRt4HV84k/KzQYREOhwDCNCg2anSdfI4O8FoIEw0jMPknTUNOg2azbxqq5OyhJMIzas1EaSkdQfvDXrG4jqyU+gPQn/kEXCMOZKwiOlstI8BLpdJ64I/xxoGoA+sAiawzddRolUoNm56m3SioMwA+GYaxdaCRJotyg0/m9LvP2gS+zAQRD1I/Ll9Ikk6DTecobh9FBlg+T9mbmG93nGvzl12LdfrhTRSFK0/bmWsf3t1fKDZ50nzSUEgyT+3S7AI5ub6+UBk+5YZYKCNI03rroX91CewD8qwbb9sXrDiowHEajbQ3G1y9lk/TENTg87EgRFtsazN+oXvnw1VPW4BAACcIdsTC+Qj84fPHixdcabNsXqMFhcxilyXYvOHt4aDZfvgD+UYNt++J3KcLhQ5hspX73QYAKvHj1rg7j9sToNUrQ7XbD+01HWDw8dI+VBvNarNsPfq5B9/a+fCl4eDgWSBGecq/s5hJ0r67KzeB99zjX4CmnRMNYvi40uJoVyy16g24uwqtVjRZWT/xaSQAa3E5VTgjefOl2MRSkI9RrY+V8ffWL5ETw5fp6ulqt3nz+fHIMbvAc+N+tCm4Q7Exkm+OxrsHJ9Rchw2cBeMBzxS8VmjlLwzAdlS1chmHYV7t1ty1el9u3ozRMVepaL9uS5Wjhl78w0BglHuOc21F/rU/Yb0e2GGde1C4K4btjTYPP119OT0+lBLkGFRbGpcMETqqPRVwMcUtaGFnitadf9i1x3UHZQodrsKhI6kHKxLtM/GGWnT8x9CMO4wL4FV6+V/6qaXByivxb12BazfTRVBPNMS3tjGbmmKYtBiP4S8zhDfplow1DDGQ5cuCztm2LmcIrZnlqaY8Yh5vY8u6mySPpCiNLzZ/BZU189yTX4ObmBjVotQoNqmyTG0xaxDVHiHBGcpZGA43mevvWzzVANeT0FczClfVZpgpjqIPJQxhfWEoT/Bc0srIwlCKAAi2pwU2r1XquNKi0NWjI+Qrb82D2ufINqQH/rgamigSpAgeX8VADZjHPsy18u8lETolVFFi254mLhdIoAoQDOAGogG4AVO4Fyg9gSXgesala3NwP7E0N8DNSA4gZbzES9D1L+pQIobWFt/BmgesGfp+BPqCB1IwnPowfpTispZo3z1s3klYmAcpQcVUUGtjSmz2VuwOOoZrHAoe8tukHLPMDEChS40cY/8xqGCHLNQRc8QmMBQ9FysMuTrhp6d+vmP/WylTINfjnedXfTwM/YDZMmasUveQyjnM/MLf8gGeRIrNjpoHhixRp2nwkQkG7ITBzPDETF5Mt154gt52wbM/Zb7+2UAelwK9vq98sNrIElU9loDJi4QdCosfyQaGByKbioyK9els5BOtK7OAv0td1+8xkfPEW5i5otd5O9/EdxQYmJihuqv7NYFqeqcfCjpwo0r2KhZIGUp0Qfd7kpZ4DCFAD/t3HBO54/u7d2Xy8p71yAyu1BwsqbYal5AtsbR7JB1osMF0DkERokKCyPPQ3ZqHKZP/n+j6FjAVPrnYMhVGIEokkoGkAMbzwcxop02PBLsUCXElFGyDbA+4lyyNNB6gEEC3cS0eb+tSH7A9ssXImlkcojHzRsMqxYJulltgsYqHkB7709ZEhE4Lsh6GDzqK6kZVdGLeT9Q/LsDqrUDHpB7Z7BDPzsIlhzNU1sEyWTQjNN1ULuK2B70GltEVtNPB2slPGlig7Kh052Z1w3PvBJ4hnvd7lf33qOSonurhy1tFI5oXdGuT9sFn0B7IVEiyWEc97JKGHjXlE7pnEPQcqBSzEPsLO78OybcR3mPZ65xUJYMgeiYEGC7A5GjKxrfXVJkGrC/nisQ0NVHxblgyRrFeGLbdtMaY6cbE7MpUIccosVtyLe/mXTsbwn/lqbOQDq7m8OH4vNJjP8ysVaGCLWDBcdGu1jo0888N+QQZAATQMpf2CbCzxh1m5e7vrFM4P8KZ65gwWyYCpYeEJffXuiXD3s0lPzFZOfAqve+9djARJRSKonOhmtV22d6WciMtVrgulfKCSHyrleOWmpzHrw/kDvMnRrrj+IvUs6VQ8yDTovZdTRbe/VPOeuBAJyE2FGjDwA+X0tnqt9co7941M3zvvOkMp8OVePFvwnJmNGsjNNmrQm1xcwGRXhnEB3u+OzzEZroQg56uzqrrmPCcaUB6FrRxS+GZt/HavjJ8ewcZxsW58I7+5NgZDsjmOm3SmNqygAUTBWGgwNQJ0ADk6xpxYaV3AOIbfN7MgPVpwdFiOBcjfj2ngbd3VTUudoNxGQp4p75BkSy3rJsz2DF6IPy+wGMbj8Ti+xNGqNWCZBkbkcO6gx276waYG2F8bGAtslwaRZeuHbxgMooUOTB5pp7OumbVUhqbBOWiQpYAeesVe/EAWKHc2Gsm5NorTDSwR9qP7xi0N4MSMFd+ukg2T8HnRhDK7SBlLeVQpb72lweRcMd+DBqbMiaVRtC7fM+04R9J65W0/QJG42YfDdrexZKiz6BvkltrDrYLrp/L0jcnPlDUoGkO0rPJYyHKiPppn/h/Iid/wA2idbM/zoDGEdyX5KR2MDwfq/NFaGvPpdFODQFYHw5iLv6AGIkVWtWfQ84E2ahV+YD2aE3fmAzfbMhUtJoNNacxle2Vm7bcoQ5HhYsyXNcCEcHl2Bh2DGFtB3ZycnlWjQSz3wbs0kOsbc/BkS9egXdKAbfuBEXvZKbOaLJc5Ep86mOqsDr1A/OZglwZ5j9TDhyuTKvtEfIxkhZujHjxJaudv2HjO5GTPmRr4xGnX/3XWtrnaQokfi/WzA9vU5NkuAh4/YU1435tADzCRTfJqco5zXV2KiU8uZWPkTs/PLys7WQz6SdLfOrWL0yRZSruDtnjDxvPGMElkevfT7NUm7rrvwSkBZ1640O4fLHAXAeNpdn7wrd5K8Hen8/PhxvHO586BGN+7MQRBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARBEARB/AT8Byrk9mntxs+ZAAAAAElFTkSuQmCC"
                    }

                };
                foreach (var item in communities)
                {
                    _commonResource.AddCommunity(item);
                }
                await _commonResource.SaveCommunityAsync();
                return Ok("Data Newly Populated");
            }
            return Ok("Data Already populated");
        }

        public async Task<IActionResult> CreateResources()
        {
            if (_commonResource.GetResources().Count() == 0)
            {
                var resources = new List<ResourceModel>
                {
                    new ResourceModel
                    {
                        Name = "A Self-Help Guide to Dealing with Depression",
                        Description = "While treating major depressive disorder will generally require professional intervention, there are ways for you to take some control over your condition. Here are eight self - help techniques or alternative therapies that can help you cope with depression, get a handle on your symptoms, and beat the disease altogether.",
                        URL = "https://www.healthline.com/health/depression/self-help-guide-to-depression",
                        ImageURL = "https://i0.wp.com/www.healthline.com/hlcmsresource/images/topic_centers/2018-9/sad_woman-732x549-thumbnail.jpg?w=420"
                    },
                    new ResourceModel
                    {
                        Name = "8 Tips for Living With Depression",
                        Description = "Everything feels more challenging when you're dealing with depression. Going to work, socializing with friends, or even just getting out of bed can feel like a struggle.",
                        URL = "https://www.verywellmind.com/tips-for-living-with-depression-1066834",
                        ImageURL = "https://www.verywellmind.com/thmb/LM__5ShPLF0KDU87LU6d1T5ZtA4=/2121x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/GettyImages-643325030-5aa2c1dea18d9e00382419f9.jpg"
                    },
                    new ResourceModel
                    {
                        Name = "Self-Help Methods For Major Depression",
                        Description = "Thus far in this document, we've described therapies that are generally best prescribed and monitored by clinical professionals. However, it is also possible to take a self-help approach to the treatment of depression under certain circumstances. Self-help approaches emphasize what people can do for themselves rather than what professionals can offer.",
                        URL = "https://www.mentalhelp.net/articles/self-help-methods-for-major-depression/",
                        ImageURL = "https://www.mentalhelp.net/content/uploads/2015/10/xmentalhelp-shutterstock276465491-meditation-feature_image.jpg.pagespeed.ic.dsYV_Coszg.webp"
                    },
                    new ResourceModel
                    {
                        Name = "How to Stop Worrying",
                        Description = "Everyone worries. Worrying can even be helpful when it spurs you to take action and solve a problem. But if you're preoccupied with 'what ifs' and worst-case scenarios, worry becomes a problem. Unrelenting anxious thoughts and fears can be paralyzing. They can sap your emotional energy, send your anxiety levels soaring, and interfere with your daily life. But chronic worrying is a mental habit that can be broken. You can train your brain to stay calm and look at life from a more balanced, less fearful perspective.",
                        URL = "https://www.helpguide.org/articles/anxiety/how-to-stop-worrying.htm",
                        ImageURL = "https://www.helpguide.org/images/anxiety/woman-eyes-closed-hand-on-temple-500.jpg"
                    },
                    new ResourceModel
                    {
                        Name = "How to Improve Self-Esteem With Generalized Anxiety Disorder",
                        Description = "People who struggle with generalized anxiety disorder (GAD) often find themselves struggling with low self-esteem. They may have poor confidence in themselves or think they are worthless.",
                        URL = "https://www.verywellmind.com/anxiety-and-self-esteem-1393168",
                        ImageURL = "https://www.verywellmind.com/thmb/rkcN4BF9ygcZtYySVMM6ol2K0s4=/768x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/GettyImages-530626911-56da37d83df78c5ba03a6777.jpg"
                    },
                    new ResourceModel
                    {
                        Name = "7 tips to help with stress and anxiety",
                        Description = "It��s totally normal to feel stressed or anxious from time to time, but there��s lots of things you can do to feel a bit better. Remember: there��s a difference between feeling stressed every now and then, and experiencing ongoing anxiety.",
                        URL = "https://au.reachout.com/articles/7-tips-to-help-with-stress-and-anxiety",
                        ImageURL = "http://www.daytonreachout.org/images/reachoutlogo2.jpg",
                    },
                    new ResourceModel
                    {
                        Name = "Ways to take care of yourself if you're being bullied",
                        Description = "If you��re experiencing bullying, looking after yourself can help you reduce your stress and feel more positive and hopeful. We chatted to a bunch of young people who��d experienced bullying and they shared their own self-care tips.",
                        URL = "https://au.reachout.com/articles/ways-to-take-care-of-yourself-if-youre-being-bullied",
                        ImageURL = "http://www.daytonreachout.org/images/reachoutlogo2.jpg"
                    },
                    new ResourceModel
                    {
                        Name = "Bullying and Cyberbullying",
                        Description = "How to Deal with a Bully and Overcome Bullying",
                        URL = "https://www.helpguide.org/articles/abuse/bullying-and-cyberbullying.htm",
                        ImageURL = "https://www.helpguide.org/images/abuse/redheaded-girl-chin-to-knee-500.jpg"
                    },
                    new ResourceModel
                    {
                        Name = "THINKING ABOUT SUICIDE?",
                        Description = "There are many crisis centres available 24 hours a day to talk to you.",
                        URL = "https://suicideprevention.ca/need-help/",
                        ImageURL = "https://suicideprevention.ca/resources/Pictures/CASP.png"
                    },
                    new ResourceModel
                    {
                        Name = "The Nature Of Suicide",
                        Description = "This introductory document discusses suicide; the taking of one's own life. It is intended to educate readers about the nature of suicide.",
                        URL = "https://www.mentalhelp.net/articles/about-the-nature-of-suicide/",
                        ImageURL = "https://www.mentalhelp.net/content/uploads/2018/02/mentalhelp-shutterstock1014834241-group-therapy-session.jpg"
                    }
                };
                foreach (var item in resources)
                {
                    _commonResource.AddResources(item);
                }
                await _commonResource.SaveCommunityAsync();

                return Ok("Data Newly Populated");
            }
            return Ok("Data Already Added");
        }

    }
}